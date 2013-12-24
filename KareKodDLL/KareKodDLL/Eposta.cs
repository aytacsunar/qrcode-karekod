using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace KareKodDLL
{
    class Eposta
    {
        int sunucuPort;
        public int SunucuPort
        {
            get { return sunucuPort; }
            set
            {
                if (value > 65535 | value < 1) sunucuPort = 587;
                else sunucuPort = value;
            }
        }
        string sunucuAdi;
        public string SunucuAdi
        {
            get { return sunucuAdi; }
            set { sunucuAdi = value; }

        }
        string gonderenAdi;
        public string GonderenAdi
        {
            get { return gonderenAdi; }
            set { gonderenAdi = value; }

        }
        string gonderenSifre;
        public string GonderenSifre
        {
            get { return gonderenSifre; }
            set { gonderenSifre = value; }

        }
        string gonderenMail;
        public string GonderenMail
        {
            get { return gonderenMail; }
            set { gonderenMail = value; }

        }
        bool sunucuGuvenlikDurumu;
        public bool SunucuGuvenlikDurumu
        {
            get { return sunucuGuvenlikDurumu; }
            set { sunucuGuvenlikDurumu = value; }
        }
        public Eposta()
        {
            GonderenMail = "emailadresi.com";
            GonderenSifre = "sifre";
            SunucuPort = 587;
            SunucuGuvenlikDurumu = true;
            SunucuAdi = "smtp.gmail.com";
            GonderenAdi = "KONU BASLIGI";
        }
        public Eposta(string mailadres, string sifre, string hostname, int portno, bool sslDurum, string gorunenAd)
        {
            GonderenMail = mailadres;
            GonderenSifre = sifre;
            SunucuPort = portno;
            SunucuGuvenlikDurumu = sslDurum;
            SunucuAdi = hostname;
            GonderenAdi = gorunenAd;
        }
        public string MailGonder(string kime, string konu, string mesaj)
        {
            string sonuc = string.Empty;
            SmtpClient gonderici = new SmtpClient();
            gonderici.Port = SunucuPort;
            gonderici.Host = SunucuAdi;
            gonderici.EnableSsl = SunucuGuvenlikDurumu;
            gonderici.Credentials = new NetworkCredential(GonderenMail, GonderenSifre);
            MailMessage eposta = new MailMessage();
            eposta.Body = mesaj;
            eposta.Subject = konu;
            eposta.To.Add(kime);
            eposta.From = new MailAddress(GonderenMail, GonderenAdi);
            try
            {
                gonderici.Send(eposta);
                sonuc = kime + " adresine başarıyla gönderildi.";
            }
            catch (Exception hata)
            {
                sonuc = kime + " adresine posta iletilmedi. \n Hata kaynağı: " + hata.Message;
            }
            return sonuc;
        }

        public string MailGonder(string kime, string konu, string mesaj, bool html)
        {
            string sonuc = string.Empty;
            SmtpClient gonderici = new SmtpClient();
            gonderici.Port = SunucuPort;
            gonderici.Host = SunucuAdi;
            gonderici.EnableSsl = SunucuGuvenlikDurumu;
            gonderici.Credentials = new NetworkCredential(GonderenMail, GonderenSifre);
            MailMessage eposta = new MailMessage();
            eposta.Body = mesaj;
            eposta.IsBodyHtml = html;
            eposta.Subject = konu;
            eposta.To.Add(kime);
            eposta.From = new MailAddress(GonderenMail, GonderenAdi);
            try
            {
                gonderici.Send(eposta);
                sonuc = kime + " adresine başarıyla gönderildi.";
            }
            catch (Exception hata)
            {
                sonuc = kime + " adresine posta iletilmedi. \n Hata kaynağı: " + hata.Message;
            }
            return sonuc;
        }

        public string[] TopluMailGonder(string[] kimlere, string konu, string mesaj, bool html)
        {
            string[] sonuclar = new string[kimlere.Length];
            SmtpClient gonderici = new SmtpClient();
            gonderici.Port = SunucuPort;
            gonderici.Host = SunucuAdi;
            gonderici.EnableSsl = SunucuGuvenlikDurumu;
            gonderici.Credentials = new NetworkCredential(GonderenMail, GonderenSifre);
            for (int i = 0; i < kimlere.Length; i++)
            {
                MailMessage eposta = new MailMessage();
                eposta.Body = mesaj;
                eposta.IsBodyHtml = html;
                eposta.Subject = konu;
                eposta.To.Add(kimlere[i]);
                eposta.From = new MailAddress(GonderenMail, GonderenAdi);
                try
                {
                    gonderici.Send(eposta);
                    sonuclar[i] = kimlere[i] + " adresine başarıyla gönderildi.";
                    Thread.Sleep(10000);
                }
                catch (Exception hata)
                {
                    sonuclar[i] = kimlere[i] + " adresine posta iletilmedi. \n Hata kaynağı: " + hata.Message;
                }
            }
            return sonuclar;
        }
    }
}