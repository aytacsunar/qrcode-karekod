using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagingToolkit.QRCode.Codec.Reader;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit;
using MessagingToolkit.QRCode.ExceptionHandler;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.IO;
using System.Text.RegularExpressions;

namespace KareKodDLL
{
    public class KareKod
    {
        //düzeltme seviyesi L-M-Q-H
        string duzeltmeSeviyesi;
        public string DuzeltmeSeviyesi
        {
            get { return duzeltmeSeviyesi; }
            set
            {
                if (value == "h" || value == "H")
                {
                    duzeltmeSeviyesi = "H";
                }
                else if (value == "l" || value == "L")
                {
                    duzeltmeSeviyesi = "L";
                }
                else if (value == "m" || value == "M")
                {
                    duzeltmeSeviyesi = "M";
                }
                else if (value == "q" || value == "Q")
                {
                    duzeltmeSeviyesi = "Q";
                }
                else
                {
                    duzeltmeSeviyesi = "L";
                }
            }
        }

        //boyutu scale 1-10
        int boyutu;
        public int Boyutu
        {
            get { return boyutu; }
            set
            {
                if (value > 10 & value < 1) boyutu = 2;
                else boyutu = value;
            }
        
        }
        //sürümü versiyon 1-40
        int surumu;
        public int Surumu
        {
            get { return surumu; }
            set
            {
                if (value > 40 & value < 1) surumu = 1;
                else surumu = value;
            }
        }
        //Arkaplan rengi #000000, 
        string arkaRenk;
        public string ArkaRenk
        {
            get { return arkaRenk; }
            set { arkaRenk = value; }
        }
        //Kodlanmamis HAM KALIP
        string hamKalip;
        public string HamKalip
        {
            get { return hamKalip; }
            set { hamKalip = value; }
        }
        
        //KodlanmisYazi
        string kodlanmisYazi;
        public string KodlanmisYazi
        {
            get { return kodlanmisYazi; }
            set { kodlanmisYazi = value; }
        }
        //Kodrengi #FFFFFF
        string renk;
        public string Renk
        {
            get { return renk; }
            set { renk = value; }
        }
        //Karekod başlangıcı
        public string HTMLKodu(System.Drawing.Color SistemRengi)
        {
            string renkkodu = HexColor.ColorToHex(SistemRengi);
            return renkkodu;

        }
        public string EpostaGonder(string kime, string konu, string mesaj)
        {
            string gid = kime;
            string gsu = konu;
            string gme  = mesaj;
            Eposta postaci = new Eposta();
            string postasonuc = postaci.MailGonder(gid, gsu, gme, true);
            return postasonuc;
        }
        public string SifreleMD5Hash(string SifrelenecekYazi)
        {
            string sifrelenmisyazi = Sifreci.MD5Hash(SifrelenecekYazi);
            return sifrelenmisyazi;
        }
        public string SifreleMD5(string SifrelenecekYazi)
        {
            string sifrelenmisyazi = Sifreci.MD5Cevir(SifrelenecekYazi);
            return sifrelenmisyazi;
        }
        public string SifreleSHA1(string SifrelenecekYazi)
        {
            string sifrelenmisyazi = Sifreci.SHA1Cevir(SifrelenecekYazi);
            return sifrelenmisyazi;
        }
        public string SifreleSHA512(string SifrelenecekYazi)
        {
            string sifrelenmisyazi = Sifreci.SHA512Cevir(SifrelenecekYazi);
            return sifrelenmisyazi;
        }
        public KareKod()
        {
            DuzeltmeSeviyesi = "L";
            ArkaRenk = "#FFFFFF";
            Renk = "#000000";
            Surumu = 10;
            Boyutu = 5;
        }
        public KareKod(int Surum, int Boyut)
        {
            DuzeltmeSeviyesi = "L";
            ArkaRenk = "#FFFFFF";
            Renk = "#000000";
            Surumu = Surum;
            Boyutu = Boyut;
        }
        public KareKod(int Surum)
        {
            DuzeltmeSeviyesi = "L";
            ArkaRenk = "#FFFFFF";
            Renk = "#000000";
            Surumu = Surum;
            Boyutu = 2;
        }
        public KareKod(string Seviyesi, string ArkaPlanRengi, string KodRengi, int Boyut)
        {
            DuzeltmeSeviyesi = Seviyesi;
            ArkaRenk = ArkaPlanRengi;
            Renk = KodRengi;
            Surumu = 10;
            Boyutu = Boyut;
        }
        public KareKod(string Seviyesi, string ArkaPlanRengi, string KodRengi, int Surum, int Boyut)
        {
            DuzeltmeSeviyesi = Seviyesi;
            ArkaRenk = ArkaPlanRengi;
            Renk = KodRengi;
            Surumu = Surum;
            Boyutu = Boyut;
        }
   /*     public partial string KareKodYazi(string KodlanacakYazi)
        {
            string sifrelenmis = SifreleMD5Hash(KodlanacakYazi);
            return sifrelenmis;
        }*/
        public Bitmap KareKodYazi(string KodlanacakYazi)
        {
            //karekod baslangici
            QRCodeEncoder encoder = new QRCodeEncoder();
            //karekod düzeltme seviyesi
            if (DuzeltmeSeviyesi == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            else if (DuzeltmeSeviyesi == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (DuzeltmeSeviyesi == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (DuzeltmeSeviyesi == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                DuzeltmeSeviyesi = "L";
            }
            //karekod boyutu
            encoder.QRCodeScale = Boyutu;
            //Kodlama tipi
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //karekod arkaplan rengi
            //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#8F00FF");
            encoder.QRCodeBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ArkaRenk);
            //encoder.QRCodeBackgroundColor = mycol;//System.Drawing.Color.FromName(comboBox1.SelectedValue.ToString());
            //karekod kod rengi
            encoder.QRCodeForegroundColor = System.Drawing.ColorTranslator.FromHtml(Renk);
            //Qrcode sürümü versiyon seviyesi
            //Gelen yazı karekod stringine dönüstü. Trim ile boşluklarını aldık.
            string karekod = KodlanacakYazi.Trim();
            HamKalip = karekod;
            KodlanmisYazi = SifreleMD5Hash(karekod);
            //gelen karekodyazisinin karakter sayısını hesaplıyoruz.
            int karaktersayisi = 0;
            for (int i = 0; i < karekod.Length; i++)
            {
                karaktersayisi++;
            }
            int karaktersonuc = karaktersayisi;
            //atanmış sürümü içeri alıyoruz.
            int atanansurum = Surumu;
            //belirlenen sürümün mevcut karaktere uygun olup olmadığını kontrol ediyoruz. eğer doğru ise sorun yok. yanlışsa
            //düzeltilmis sürüm nosu geliyor...
            encoder.QRCodeVersion = SurumKontrol(karaktersonuc, atanansurum);
            //karekod için veriler alınıyor
            Bitmap img = encoder.Encode(karekod);
            return img;
        }
        public Bitmap KareKodTelefon(string TelefonNumarasi)
        {
            //karekod baslangici
            QRCodeEncoder encoder = new QRCodeEncoder();
            //karekod düzeltme seviyesi
            if (DuzeltmeSeviyesi == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            else if (DuzeltmeSeviyesi == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (DuzeltmeSeviyesi == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (DuzeltmeSeviyesi == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            //karekod boyutu
            encoder.QRCodeScale = Boyutu;
            //Kodlama tipi
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //karekod arkaplan rengi
            //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#8F00FF");
            encoder.QRCodeBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ArkaRenk);
            //encoder.QRCodeBackgroundColor = mycol;//System.Drawing.Color.FromName(comboBox1.SelectedValue.ToString());
            //karekod kod rengi
            encoder.QRCodeForegroundColor = System.Drawing.ColorTranslator.FromHtml(Renk);
            //Qrcode sürümü versiyon seviyesi
            //Gelen yazı karekod stringine dönüstü. Trim ile boşluklarını aldık.
            string karekod = TelefonNumarasi.Trim();
            karekod = string.Format("TEL:{0}",karekod);
            HamKalip = karekod;
            KodlanmisYazi = SifreleMD5Hash(karekod);
            //gelen karekodyazisinin karakter sayısını hesaplıyoruz.
            int karaktersayisi = 0;
            for (int i = 0; i < karekod.Length; i++)
            {
                karaktersayisi++;
            }
            int karaktersonuc = karaktersayisi;
            //atanmış sürümü içeri alıyoruz.
            int atanansurum = Surumu;
            //belirlenen sürümün mevcut karaktere uygun olup olmadığını kontrol ediyoruz. eğer doğru ise sorun yok. yanlışsa
            //düzeltilmis sürüm nosu geliyor...
            encoder.QRCodeVersion = SurumKontrol(karaktersonuc, atanansurum);
            //karekod için veriler alınıyor
            Bitmap img = encoder.Encode(karekod);
            return img;
        }
        public Bitmap KareKodWebAdresi(string WebAdresiHTTP)
        {
            //karekod baslangici
            QRCodeEncoder encoder = new QRCodeEncoder();
            //karekod düzeltme seviyesi
            if (DuzeltmeSeviyesi == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            else if (DuzeltmeSeviyesi == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (DuzeltmeSeviyesi == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (DuzeltmeSeviyesi == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            //karekod boyutu
            encoder.QRCodeScale = Boyutu;
            //Kodlama tipi
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //karekod arkaplan rengi
            //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#8F00FF");
            encoder.QRCodeBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ArkaRenk);
            //encoder.QRCodeBackgroundColor = mycol;//System.Drawing.Color.FromName(comboBox1.SelectedValue.ToString());
            //karekod kod rengi
            encoder.QRCodeForegroundColor = System.Drawing.ColorTranslator.FromHtml(Renk);
            //Qrcode sürümü versiyon seviyesi
            //Gelen yazı karekod stringine dönüstü. Trim ile boşluklarını aldık.
            string karekod = WebAdresiHTTP.Trim();
            if (Regex.IsMatch(karekod, "http://") && !Regex.IsMatch(karekod, "https://"))
            {

            }
            else
            {
                karekod = string.Format("http://{0}", karekod);
            }
            HamKalip = karekod;
            KodlanmisYazi = SifreleMD5Hash(karekod);
            //gelen karekodyazisinin karakter sayısını hesaplıyoruz.
            int karaktersayisi = 0;
            for (int i = 0; i < karekod.Length; i++)
            {
                karaktersayisi++;
            }
            int karaktersonuc = karaktersayisi;
            //atanmış sürümü içeri alıyoruz.
            int atanansurum = Surumu;
            //belirlenen sürümün mevcut karaktere uygun olup olmadığını kontrol ediyoruz. eğer doğru ise sorun yok. yanlışsa
            //düzeltilmis sürüm nosu geliyor...
            encoder.QRCodeVersion = SurumKontrol(karaktersonuc, atanansurum);
            //karekod için veriler alınıyor
            Bitmap img = encoder.Encode(karekod);
            return img;
        }
        public Bitmap KareKodKisaMesajSMS(string TelefonNumarasi, string KisaMesaj)
        {
            //karekod baslangici
            QRCodeEncoder encoder = new QRCodeEncoder();
            //karekod düzeltme seviyesi
            if (DuzeltmeSeviyesi == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            else if (DuzeltmeSeviyesi == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (DuzeltmeSeviyesi == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (DuzeltmeSeviyesi == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            //karekod boyutu
            encoder.QRCodeScale = Boyutu;
            //Kodlama tipi
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //karekod arkaplan rengi
            //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#8F00FF");
            encoder.QRCodeBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ArkaRenk);
            //encoder.QRCodeBackgroundColor = mycol;//System.Drawing.Color.FromName(comboBox1.SelectedValue.ToString());
            //karekod kod rengi
            encoder.QRCodeForegroundColor = System.Drawing.ColorTranslator.FromHtml(Renk);
            //Qrcode sürümü versiyon seviyesi
            //Gelen değerler karekod stringine dönüstü. Trim ile boşluklarını aldık.
            string telefonno = TelefonNumarasi.Trim();
            string kisamesaji = KisaMesaj.Trim();
            string karekod = string.Format("SMSTO:{0}:{1}", telefonno, kisamesaji);
            HamKalip = karekod;
            KodlanmisYazi = SifreleMD5Hash(karekod);
            //gelen karekodyazisinin karakter sayısını hesaplıyoruz.
            int karaktersayisi = 0;
            for (int i = 0; i < karekod.Length; i++)
            {
                karaktersayisi++;
            }
            int karaktersonuc = karaktersayisi;
            //atanmış sürümü içeri alıyoruz.
            int atanansurum = Surumu;
            //belirlenen sürümün mevcut karaktere uygun olup olmadığını kontrol ediyoruz. eğer doğru ise sorun yok. yanlışsa
            //düzeltilmis sürüm nosu geliyor...
            encoder.QRCodeVersion = SurumKontrol(karaktersonuc, atanansurum);
            //karekod için veriler alınıyor
            Bitmap img = encoder.Encode(karekod);
            return img;
        }
        public Bitmap KareKodBookMark(string SikKullanilanlarAdi, string WebAdresiHttpURL)
        {
            //karekod baslangici
            QRCodeEncoder encoder = new QRCodeEncoder();
            //karekod düzeltme seviyesi
            if (DuzeltmeSeviyesi == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            else if (DuzeltmeSeviyesi == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (DuzeltmeSeviyesi == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (DuzeltmeSeviyesi == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            //karekod boyutu
            encoder.QRCodeScale = Boyutu;
            //Kodlama tipi
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //karekod arkaplan rengi
            //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#8F00FF");
            encoder.QRCodeBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ArkaRenk);
            //encoder.QRCodeBackgroundColor = mycol;//System.Drawing.Color.FromName(comboBox1.SelectedValue.ToString());
            //karekod kod rengi
            encoder.QRCodeForegroundColor = System.Drawing.ColorTranslator.FromHtml(Renk);
            //Qrcode sürümü versiyon seviyesi
            //Gelen değerler karekod stringine dönüstü. Trim ile boşluklarını aldık.
            string bookmarkadi = SikKullanilanlarAdi.Trim();
            string weburl = WebAdresiHttpURL.Trim();
            if (Regex.IsMatch(weburl, "http://") && !Regex.IsMatch(weburl, "https://"))
            {

            }
            else
            {
                weburl = "http://" + weburl;
            }
            string karekod = string.Format("MEBKM:TITLE:{0};URL:{1};;", bookmarkadi, weburl);
            HamKalip = karekod;
            KodlanmisYazi = SifreleMD5Hash(karekod);
            //gelen karekodyazisinin karakter sayısını hesaplıyoruz.
            int karaktersayisi = 0;
            for (int i = 0; i < karekod.Length; i++)
            {
                karaktersayisi++;
            }
            int karaktersonuc = karaktersayisi;
            //atanmış sürümü içeri alıyoruz.
            int atanansurum = Surumu;
            //belirlenen sürümün mevcut karaktere uygun olup olmadığını kontrol ediyoruz. eğer doğru ise sorun yok. yanlışsa
            //düzeltilmis sürüm nosu geliyor...
            encoder.QRCodeVersion = SurumKontrol(karaktersonuc, atanansurum);
            //karekod için veriler alınıyor
            Bitmap img = encoder.Encode(karekod);
            return img;
        }
        public Bitmap KareKodMECARD(string KisiAdi, string TelefonNo, string Adresi, string EpostaAdresi, string WebAdresi)
        {
            //karekod baslangici
            QRCodeEncoder encoder = new QRCodeEncoder();
            //karekod düzeltme seviyesi
            if (DuzeltmeSeviyesi == "H")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            else if (DuzeltmeSeviyesi == "L")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (DuzeltmeSeviyesi == "M")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (DuzeltmeSeviyesi == "Q")
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else
            {
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                DuzeltmeSeviyesi = "L";
            }
            //karekod boyutu
            encoder.QRCodeScale = Boyutu;
            //Kodlama tipi
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //karekod arkaplan rengi
            //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#8F00FF");
            encoder.QRCodeBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ArkaRenk);
            //encoder.QRCodeBackgroundColor = mycol;//System.Drawing.Color.FromName(comboBox1.SelectedValue.ToString());
            //karekod kod rengi
            encoder.QRCodeForegroundColor = System.Drawing.ColorTranslator.FromHtml(Renk);
            //Qrcode sürümü versiyon seviyesi
            //Gelen değerler karekod stringine dönüstü. Trim ile boşluklarını aldık.
            string adi = KisiAdi.Trim();
         //   string takmaad = TakmaAdi.Trim();
            string tel1 = TelefonNo.Trim();
            string adres = Adresi.Trim();
         //   string not = NotPad.Trim();
            string mail = EpostaAdresi.Trim();
            string weburl = WebAdresi.Trim();
            if (Regex.IsMatch(weburl, "http://") && !Regex.IsMatch(weburl, "https://"))
            {

            }
            else
            {
                weburl = "http://" + weburl;
            }
            string karekod = string.Empty;
            karekod = "MECARD:N:" + adi + ";ADR:" + adres + ";TEL:" + tel1 + ";EMAIL:" + mail + ";URL:" + weburl + ";;";
       //     string karekod = string.Format("MECARD:N:{0};NICKNAME:{1};ADR:{2};TEL:{3};EMAIL:{4};URL:{5};NOTE:{6};;", adi, takmaad, adres, tel1, mail, weburl, not);
            HamKalip = karekod;
            KodlanmisYazi = SifreleMD5Hash(karekod);
            //gelen karekodyazisinin karakter sayısını hesaplıyoruz.
            int karaktersayisi = 0;
            for (int i = 0; i < karekod.Length; i++)
            {
                karaktersayisi++;
            }
            int karaktersonuc = karaktersayisi;
            //atanmış sürümü içeri alıyoruz.
            int atanansurum = Surumu;
            //belirlenen sürümün mevcut karaktere uygun olup olmadığını kontrol ediyoruz. eğer doğru ise sorun yok. yanlışsa
            //düzeltilmis sürüm nosu geliyor...
            encoder.QRCodeVersion = SurumKontrol(karaktersonuc, atanansurum);
            //karekod için veriler alınıyor
            Bitmap img = encoder.Encode(karekod);
            return img;
        }
        //Sürüm kontrol methodu
        public int SurumKontrol(int karaktersayisi, int surumno)
        {

            int sayi = karaktersayisi;
            int sno = surumno;
            int sonuc = 0;
            string duzeltseviye = string.Empty;
            duzeltseviye = DuzeltmeSeviyesi;
            if (duzeltseviye == "L")
            {
                //L seviyesi için
                if (sayi <= 17 & sayi > 1)
                {
                    if (sno < 1) sonuc = 1;
                    else sonuc = sno;
                }
                else if (sayi <= 32 & sayi > 17)
                {
                    if (sno < 2) sonuc = 2;
                    else sonuc = sno;
                }
                else if (sayi <= 53 & sayi > 32)
                {
                    if (sno < 3) sonuc = 3;
                    else sonuc = sno;
                }
                else if (sayi <= 78 & sayi > 53)
                {
                    if (sno < 4) sonuc = 4;
                    else sonuc = sno;
                }
                else if (sayi <= 106 & sayi > 78)
                {
                    if (sno < 5) sonuc = 5;
                    else sonuc = sno;
                }
                else if (sayi <= 134 & sayi > 106)
                {
                    if (sno < 6) sonuc = 6;
                    else sonuc = sno;
                }
                else if (sayi <= 154 & sayi > 134)
                {
                    if (sno < 7) sonuc = 7;
                    else sonuc = sno;
                }
                else if (sayi <= 192 & sayi > 154)
                {
                    if (sno < 8) sonuc = 8;
                    else sonuc = sno;
                }
                else if (sayi <= 230 & sayi > 192)
                {
                    if (sno < 9) sonuc = 9;
                    else sonuc = sno;
                }
                else if (sayi <= 271 & sayi > 230)
                {
                    if (sno < 10) sonuc = 10;
                    else sonuc = sno;
                }
                else if (sayi <= 321 & sayi > 271)
                {
                    if (sno < 11) sonuc = 11;
                    else sonuc = sno;
                }
                else if (sayi <= 367 & sayi > 321)
                {
                    if (sno < 12) sonuc = 12;
                    else sonuc = sno;
                }
                else if (sayi <= 425 & sayi > 367)
                {
                    if (sno < 13) sonuc = 13;
                    else sonuc = sno;
                }
                else if (sayi <= 458 & sayi > 425)
                {
                    if (sno < 14) sonuc = 14;
                    else sonuc = sno;
                }
                else if (sayi <= 520 & sayi > 458)
                {
                    if (sno < 15) sonuc = 15;
                    else sonuc = sno;
                }
                else if (sayi <= 586 & sayi > 520)
                {
                    if (sno < 16) sonuc = 16;
                    else sonuc = sno;
                }
                else if (sayi <= 644 & sayi > 586)
                {
                    if (sno < 17) sonuc = 17;
                    else sonuc = sno;
                }
                else if (sayi <= 718 & sayi > 644)
                {
                    if (sno < 18) sonuc = 18;
                    else sonuc = sno;
                }
                else if (sayi <= 792 & sayi > 718)
                {
                    if (sno < 19) sonuc = 19;
                    else sonuc = sno;
                }
                else if (sayi <= 858 & sayi > 792)
                {
                    if (sno < 20) sonuc = 20;
                    else sonuc = sno;
                }
                else if (sayi <= 929 & sayi > 858)
                {
                    if (sno < 21) sonuc = 21;
                    else sonuc = sno;
                }
                else if (sayi <= 1003 & sayi > 929)
                {
                    if (sno < 22) sonuc = 22;
                    else sonuc = sno;
                }
                else if (sayi <= 1091 & sayi > 1003)
                {
                    if (sno < 23) sonuc = 23;
                    else sonuc = sno;
                }
                else if (sayi <= 1171 & sayi > 1091)
                {
                    if (sno < 24) sonuc = 24;
                    else sonuc = sno;
                }
                else if (sayi <= 1273 & sayi > 1171)
                {
                    if (sno < 25) sonuc = 25;
                    else sonuc = sno;
                }
                else if (sayi <= 1367 & sayi > 1273)
                {
                    if (sno < 26) sonuc = 26;
                    else sonuc = sno;
                }
                else if (sayi <= 1465 & sayi > 1367)
                {
                    if (sno < 27) sonuc = 27;
                    else sonuc = sno;
                }
                else if (sayi <= 1528 & sayi > 1465)
                {
                    if (sno < 28) sonuc = 28;
                    else sonuc = sno;
                }
                else if (sayi <= 1628 & sayi > 1528)
                {
                    if (sno < 29) sonuc = 29;
                    else sonuc = sno;
                }
                else if (sayi <= 1732 & sayi > 1628)
                {
                    if (sno < 30) sonuc = 30;
                    else sonuc = sno;
                }
                else if (sayi <= 1840 & sayi > 1732)
                {
                    if (sno < 31) sonuc = 31;
                    else sonuc = sno;
                }
                else if (sayi <= 1952 & sayi > 1840)
                {
                    if (sno < 32) sonuc = 32;
                    else sonuc = sno;
                }
                else if (sayi <= 2068 & sayi > 1952)
                {
                    if (sno < 33) sonuc = 33;
                    else sonuc = sno;
                }
                else if (sayi <= 2188 & sayi > 2068)
                {
                    if (sno < 34) sonuc = 34;
                    else sonuc = sno;
                }
                else if (sayi <= 2303 & sayi > 2188)
                {
                    if (sno < 35) sonuc = 35;
                    else sonuc = sno;
                }
                else if (sayi <= 2431 & sayi > 2303)
                {
                    if (sno < 36) sonuc = 36;
                    else sonuc = sno;
                }
                else if (sayi <= 2563 & sayi > 2431)
                {
                    if (sno < 37) sonuc = 37;
                    else sonuc = sno;
                }
                else if (sayi <= 2699 & sayi > 2563)
                {
                    if (sno < 38) sonuc = 38;
                    else sonuc = sno;
                }
                else if (sayi <= 2809 & sayi > 2699)
                {
                    if (sno < 39) sonuc = 39;
                    else sonuc = sno;
                }
                else if (sayi <= 2953 & sayi > 2809)
                {
                    if (sno < 40) sonuc = 40;
                    else sonuc = sno;
                }
                else if (sayi > 2953 | sno > 40)
                {
                    sonuc = 0;
                }
                else
                {
                    sonuc = sno;
                }
                //L seviyesi bitis
            }
            else if (duzeltseviye == "H")
            {
                //H seviyesi için
                if (sayi <= 7 & sayi > 1)
                {
                    if (sno < 1) sonuc = 1;
                    else sonuc = sno;
                }
                else if (sayi <= 14 & sayi > 7)
                {
                    if (sno < 2) sonuc = 2;
                    else sonuc = sno;
                }
                else if (sayi <= 24 & sayi > 14)
                {
                    if (sno < 3) sonuc = 3;
                    else sonuc = sno;
                }
                else if (sayi <= 34 & sayi > 24)
                {
                    if (sno < 4) sonuc = 4;
                    else sonuc = sno;
                }
                else if (sayi <= 44 & sayi > 34)
                {
                    if (sno < 5) sonuc = 5;
                    else sonuc = sno;
                }
                else if (sayi <= 58 & sayi > 44)
                {
                    if (sno < 6) sonuc = 6;
                    else sonuc = sno;
                }
                else if (sayi <= 64 & sayi > 58)
                {
                    if (sno < 7) sonuc = 7;
                    else sonuc = sno;
                }
                else if (sayi <= 84 & sayi > 64)
                {
                    if (sno < 8) sonuc = 8;
                    else sonuc = sno;
                }
                else if (sayi <= 98 & sayi > 84)
                {
                    if (sno < 9) sonuc = 9;
                    else sonuc = sno;
                }
                else if (sayi <= 119 & sayi > 98)
                {
                    if (sno < 10) sonuc = 10;
                    else sonuc = sno;
                }
                else if (sayi <= 137 & sayi > 119)
                {
                    if (sno < 11) sonuc = 11;
                    else sonuc = sno;
                }
                else if (sayi <= 155 & sayi > 137)
                {
                    if (sno < 12) sonuc = 12;
                    else sonuc = sno;
                }
                else if (sayi <= 177 & sayi > 155)
                {
                    if (sno < 13) sonuc = 13;
                    else sonuc = sno;
                }
                else if (sayi <= 194 & sayi > 177)
                {
                    if (sno < 14) sonuc = 14;
                    else sonuc = sno;
                }
                else if (sayi <= 220 & sayi > 194)
                {
                    if (sno < 15) sonuc = 15;
                    else sonuc = sno;
                }
                else if (sayi <= 250 & sayi > 220)
                {
                    if (sno < 16) sonuc = 16;
                    else sonuc = sno;
                }
                else if (sayi <= 280 & sayi > 250)
                {
                    if (sno < 17) sonuc = 17;
                    else sonuc = sno;
                }
                else if (sayi <= 310 & sayi > 280)
                {
                    if (sno < 18) sonuc = 18;
                    else sonuc = sno;
                }
                else if (sayi <= 338 & sayi > 310)
                {
                    if (sno < 19) sonuc = 19;
                    else sonuc = sno;
                }
                else if (sayi <= 382 & sayi > 338)
                {
                    if (sno < 20) sonuc = 20;
                    else sonuc = sno;
                }
                else if (sayi <= 403 & sayi > 382)
                {
                    if (sno < 21) sonuc = 21;
                    else sonuc = sno;
                }
                else if (sayi <= 439 & sayi > 403)
                {
                    if (sno < 22) sonuc = 22;
                    else sonuc = sno;
                }
                else if (sayi <= 461 & sayi > 439)
                {
                    if (sno < 23) sonuc = 23;
                    else sonuc = sno;
                }
                else if (sayi <= 511 & sayi > 461)
                {
                    if (sno < 24) sonuc = 24;
                    else sonuc = sno;
                }
                else if (sayi <= 535 & sayi > 511)
                {
                    if (sno < 25) sonuc = 25;
                    else sonuc = sno;
                }
                else if (sayi <= 593 & sayi > 535)
                {
                    if (sno < 26) sonuc = 26;
                    else sonuc = sno;
                }
                else if (sayi <= 625 & sayi > 593)
                {
                    if (sno < 27) sonuc = 27;
                    else sonuc = sno;
                }
                else if (sayi <= 658 & sayi > 625)
                {
                    if (sno < 28) sonuc = 28;
                    else sonuc = sno;
                }
                else if (sayi <= 698 & sayi > 658)
                {
                    if (sno < 29) sonuc = 29;
                    else sonuc = sno;
                }
                else if (sayi <= 742 & sayi > 698)
                {
                    if (sno < 30) sonuc = 30;
                    else sonuc = sno;
                }
                else if (sayi <= 790 & sayi > 742)
                {
                    if (sno < 31) sonuc = 31;
                    else sonuc = sno;
                }
                else if (sayi <= 842 & sayi > 790)
                {
                    if (sno < 32) sonuc = 32;
                    else sonuc = sno;
                }
                else if (sayi <= 898 & sayi > 842)
                {
                    if (sno < 33) sonuc = 33;
                    else sonuc = sno;
                }
                else if (sayi <= 958 & sayi > 898)
                {
                    if (sno < 34) sonuc = 34;
                    else sonuc = sno;
                }
                else if (sayi <= 983 & sayi > 958)
                {
                    if (sno < 35) sonuc = 35;
                    else sonuc = sno;
                }
                else if (sayi <= 1051 & sayi > 983)
                {
                    if (sno < 36) sonuc = 36;
                    else sonuc = sno;
                }
                else if (sayi <= 1093 & sayi > 1051)
                {
                    if (sno < 37) sonuc = 37;
                    else sonuc = sno;
                }
                else if (sayi <= 1139 & sayi > 1093)
                {
                    if (sno < 38) sonuc = 38;
                    else sonuc = sno;
                }
                else if (sayi <= 1219 & sayi > 1139)
                {
                    if (sno < 39) sonuc = 39;
                    else sonuc = sno;
                }
                else if (sayi <= 1273 & sayi > 1219)
                {
                    if (sno < 40) sonuc = 40;
                    else sonuc = sno;
                }
                else if (sayi > 1273 | sno > 40)
                {
                    sonuc = 0;
                }
                else
                {
                    sonuc = sno;
                }
                //H seviyesi bitis
            }
            else if (duzeltseviye == "Q")
            {
                //Q seviyesi için
                if (sayi <= 11 & sayi > 1)
                {
                    if (sno < 1) sonuc = 1;
                    else sonuc = sno;
                }
                else if (sayi <= 20 & sayi > 11)
                {
                    if (sno < 2) sonuc = 2;
                    else sonuc = sno;
                }
                else if (sayi <= 32 & sayi > 20)
                {
                    if (sno < 3) sonuc = 3;
                    else sonuc = sno;
                }
                else if (sayi <= 46 & sayi > 32)
                {
                    if (sno < 4) sonuc = 4;
                    else sonuc = sno;
                }
                else if (sayi <= 60 & sayi > 46)
                {
                    if (sno < 5) sonuc = 5;
                    else sonuc = sno;
                }
                else if (sayi <= 74 & sayi > 60)
                {
                    if (sno < 6) sonuc = 6;
                    else sonuc = sno;
                }
                else if (sayi <= 86 & sayi > 74)
                {
                    if (sno < 7) sonuc = 7;
                    else sonuc = sno;
                }
                else if (sayi <= 108 & sayi > 86)
                {
                    if (sno < 8) sonuc = 8;
                    else sonuc = sno;
                }
                else if (sayi <= 130 & sayi > 108)
                {
                    if (sno < 9) sonuc = 9;
                    else sonuc = sno;
                }
                else if (sayi <= 151 & sayi > 130)
                {
                    if (sno < 10) sonuc = 10;
                    else sonuc = sno;
                }
                else if (sayi <= 177 & sayi > 151)
                {
                    if (sno < 11) sonuc = 11;
                    else sonuc = sno;
                }
                else if (sayi <= 203 & sayi > 177)
                {
                    if (sno < 12) sonuc = 12;
                    else sonuc = sno;
                }
                else if (sayi <= 241 & sayi > 203)
                {
                    if (sno < 13) sonuc = 13;
                    else sonuc = sno;
                }
                else if (sayi <= 258 & sayi > 241)
                {
                    if (sno < 14) sonuc = 14;
                    else sonuc = sno;
                }
                else if (sayi <= 292 & sayi > 258)
                {
                    if (sno < 15) sonuc = 15;
                    else sonuc = sno;
                }
                else if (sayi <= 322 & sayi > 292)
                {
                    if (sno < 16) sonuc = 16;
                    else sonuc = sno;
                }
                else if (sayi <= 364 & sayi > 322)
                {
                    if (sno < 17) sonuc = 17;
                    else sonuc = sno;
                }
                else if (sayi <= 394 & sayi > 364)
                {
                    if (sno < 18) sonuc = 18;
                    else sonuc = sno;
                }
                else if (sayi <= 442 & sayi > 394)
                {
                    if (sno < 19) sonuc = 19;
                    else sonuc = sno;
                }
                else if (sayi <= 482 & sayi > 442)
                {
                    if (sno < 20) sonuc = 20;
                    else sonuc = sno;
                }
                else if (sayi <= 509 & sayi > 482)
                {
                    if (sno < 21) sonuc = 21;
                    else sonuc = sno;
                }
                else if (sayi <= 565 & sayi > 509)
                {
                    if (sno < 22) sonuc = 22;
                    else sonuc = sno;
                }
                else if (sayi <= 611 & sayi > 565)
                {
                    if (sno < 23) sonuc = 23;
                    else sonuc = sno;
                }
                else if (sayi <= 661 & sayi > 611)
                {
                    if (sno < 24) sonuc = 24;
                    else sonuc = sno;
                }
                else if (sayi <= 715 & sayi > 661)
                {
                    if (sno < 25) sonuc = 25;
                    else sonuc = sno;
                }
                else if (sayi <= 751 & sayi > 715)
                {
                    if (sno < 26) sonuc = 26;
                    else sonuc = sno;
                }
                else if (sayi <= 805 & sayi > 751)
                {
                    if (sno < 27) sonuc = 27;
                    else sonuc = sno;
                }
                else if (sayi <= 868 & sayi > 805)
                {
                    if (sno < 28) sonuc = 28;
                    else sonuc = sno;
                }
                else if (sayi <= 908 & sayi > 868)
                {
                    if (sno < 29) sonuc = 29;
                    else sonuc = sno;
                }
                else if (sayi <= 982 & sayi > 908)
                {
                    if (sno < 30) sonuc = 30;
                    else sonuc = sno;
                }
                else if (sayi <= 1030 & sayi > 982)
                {
                    if (sno < 31) sonuc = 31;
                    else sonuc = sno;
                }
                else if (sayi <= 1112 & sayi > 1030)
                {
                    if (sno < 32) sonuc = 32;
                    else sonuc = sno;
                }
                else if (sayi <= 1168 & sayi > 1112)
                {
                    if (sno < 33) sonuc = 33;
                    else sonuc = sno;
                }
                else if (sayi <= 1228 & sayi > 1168)
                {
                    if (sno < 34) sonuc = 34;
                    else sonuc = sno;
                }
                else if (sayi <= 1283 & sayi > 1228)
                {
                    if (sno < 35) sonuc = 35;
                    else sonuc = sno;
                }
                else if (sayi <= 1351 & sayi > 1283)
                {
                    if (sno < 36) sonuc = 36;
                    else sonuc = sno;
                }
                else if (sayi <= 1423 & sayi > 1351)
                {
                    if (sno < 37) sonuc = 37;
                    else sonuc = sno;
                }
                else if (sayi <= 1499 & sayi > 1423)
                {
                    if (sno < 38) sonuc = 38;
                    else sonuc = sno;
                }
                else if (sayi <= 1579 & sayi > 1499)
                {
                    if (sno < 39) sonuc = 39;
                    else sonuc = sno;
                }
                else if (sayi <= 1663 & sayi > 1579)
                {
                    if (sno < 40) sonuc = 40;
                    else sonuc = sno;
                }
                else if (sayi > 1663 | sno > 40)
                {
                    sonuc = 0;
                }
                else
                {
                    sonuc = sno;
                }
                //Q seviyesi bitis
            }
            else if (duzeltseviye == "M")
            {
                //M seviyesi için
                if (sayi <= 14 & sayi > 1)
                {
                    if (sno < 1) sonuc = 1;
                    else sonuc = sno;
                }
                else if (sayi <= 26 & sayi > 14)
                {
                    if (sno < 2) sonuc = 2;
                    else sonuc = sno;
                }
                else if (sayi <= 42 & sayi > 26)
                {
                    if (sno < 3) sonuc = 3;
                    else sonuc = sno;
                }
                else if (sayi <= 62 & sayi > 42)
                {
                    if (sno < 4) sonuc = 4;
                    else sonuc = sno;
                }
                else if (sayi <= 84 & sayi > 62)
                {
                    if (sno < 5) sonuc = 5;
                    else sonuc = sno;
                }
                else if (sayi <= 106 & sayi > 84)
                {
                    if (sno < 6) sonuc = 6;
                    else sonuc = sno;
                }
                else if (sayi <= 122 & sayi > 106)
                {
                    if (sno < 7) sonuc = 7;
                    else sonuc = sno;
                }
                else if (sayi <= 152 & sayi > 122)
                {
                    if (sno < 8) sonuc = 8;
                    else sonuc = sno;
                }
                else if (sayi <= 180 & sayi > 152)
                {
                    if (sno < 9) sonuc = 9;
                    else sonuc = sno;
                }
                else if (sayi <= 213 & sayi > 180)
                {
                    if (sno < 10) sonuc = 10;
                    else sonuc = sno;
                }
                else if (sayi <= 251 & sayi > 213)
                {
                    if (sno < 11) sonuc = 11;
                    else sonuc = sno;
                }
                else if (sayi <= 287 & sayi > 251)
                {
                    if (sno < 12) sonuc = 12;
                    else sonuc = sno;
                }
                else if (sayi <= 331 & sayi > 287)
                {
                    if (sno < 13) sonuc = 13;
                    else sonuc = sno;
                }
                else if (sayi <= 362 & sayi > 331)
                {
                    if (sno < 14) sonuc = 14;
                    else sonuc = sno;
                }
                else if (sayi <= 412 & sayi > 362)
                {
                    if (sno < 15) sonuc = 15;
                    else sonuc = sno;
                }
                else if (sayi <= 450 & sayi > 412)
                {
                    if (sno < 16) sonuc = 16;
                    else sonuc = sno;
                }
                else if (sayi <= 504 & sayi > 450)
                {
                    if (sno < 17) sonuc = 17;
                    else sonuc = sno;
                }
                else if (sayi <= 560 & sayi > 504)
                {
                    if (sno < 18) sonuc = 18;
                    else sonuc = sno;
                }
                else if (sayi <= 624 & sayi > 560)
                {
                    if (sno < 19) sonuc = 19;
                    else sonuc = sno;
                }
                else if (sayi <= 666 & sayi > 624)
                {
                    if (sno < 20) sonuc = 20;
                    else sonuc = sno;
                }
                else if (sayi <= 711 & sayi > 666)
                {
                    if (sno < 21) sonuc = 21;
                    else sonuc = sno;
                }
                else if (sayi <= 779 & sayi > 711)
                {
                    if (sno < 22) sonuc = 22;
                    else sonuc = sno;
                }
                else if (sayi <= 857 & sayi > 779)
                {
                    if (sno < 23) sonuc = 23;
                    else sonuc = sno;
                }
                else if (sayi <= 911 & sayi > 857)
                {
                    if (sno < 24) sonuc = 24;
                    else sonuc = sno;
                }
                else if (sayi <= 997 & sayi > 911)
                {
                    if (sno < 25) sonuc = 25;
                    else sonuc = sno;
                }
                else if (sayi <= 1059 & sayi > 997)
                {
                    if (sno < 26) sonuc = 26;
                    else sonuc = sno;
                }
                else if (sayi <= 1125 & sayi > 1059)
                {
                    if (sno < 27) sonuc = 27;
                    else sonuc = sno;
                }
                else if (sayi <= 1190 & sayi > 1125)
                {
                    if (sno < 28) sonuc = 28;
                    else sonuc = sno;
                }
                else if (sayi <= 1264 & sayi > 1190)
                {
                    if (sno < 29) sonuc = 29;
                    else sonuc = sno;
                }
                else if (sayi <= 1370 & sayi > 1264)
                {
                    if (sno < 30) sonuc = 30;
                    else sonuc = sno;
                }
                else if (sayi <= 1452 & sayi > 1370)
                {
                    if (sno < 31) sonuc = 31;
                    else sonuc = sno;
                }
                else if (sayi <= 1538 & sayi > 1452)
                {
                    if (sno < 32) sonuc = 32;
                    else sonuc = sno;
                }
                else if (sayi <= 1628 & sayi > 1538)
                {
                    if (sno < 33) sonuc = 33;
                    else sonuc = sno;
                }
                else if (sayi <= 1722 & sayi > 1628)
                {
                    if (sno < 34) sonuc = 34;
                    else sonuc = sno;
                }
                else if (sayi <= 1809 & sayi > 1722)
                {
                    if (sno < 35) sonuc = 35;
                    else sonuc = sno;
                }
                else if (sayi <= 1911 & sayi > 1809)
                {
                    if (sno < 36) sonuc = 36;
                    else sonuc = sno;
                }
                else if (sayi <= 1989 & sayi > 1911)
                {
                    if (sno < 37) sonuc = 37;
                    else sonuc = sno;
                }
                else if (sayi <= 2099 & sayi > 1989)
                {
                    if (sno < 38) sonuc = 38;
                    else sonuc = sno;
                }
                else if (sayi <= 2213 & sayi > 2099)
                {
                    if (sno < 39) sonuc = 39;
                    else sonuc = sno;
                }
                else if (sayi <= 2331 & sayi > 2213)
                {
                    if (sno < 40) sonuc = 40;
                    else sonuc = sno;
                }
                else if (sayi > 2331 | sno > 40)
                {
                    sonuc = 0;
                }
                else
                {
                    sonuc = sno;
                }
                //M seviyesi bitis
            }
            else
            {
                sonuc = 0;
            }
            //Bu kod ile sürümün en güncel olanını değiştirmiş oluruz!
            Surumu = sonuc;
            return sonuc;
        }
    }
}
