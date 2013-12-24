using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KareKodDLL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace KareKodWeb
{
    public partial class KareKodYap : System.Web.UI.Page
    {
        KareKod karekoddll = new KareKod();
        protected void Page_Load(object sender, EventArgs e)
        {
       //     karekoddll.Renk = "#FFFFFF";   
       //     karekoddll.ArkaRenk = "#000000";
            Bitmap karekodyaziornek = karekoddll.KareKodYazi("Bu bir KareKodWeb denemesidir!");
            Bitmap karekodmecardornek = karekoddll.KareKodMECARD("KareKodWeb", "000000000", "AAAAA", "karekod@web.com", "http://kimzeki.com");
            Bitmap karekodsmsornek = karekoddll.KareKodKisaMesajSMS("0000000", "KareKodWeb ornek sms");
            Bitmap karekodurlornek = karekoddll.KareKodWebAdresi("http://kimzeki.com");
            Bitmap karekodbookmark = karekoddll.KareKodBookMark("KareKodSık", "http://kimzeki.com");
            Bitmap karekodtelnoornek = karekoddll.KareKodTelefon("0000000000");
            string path = HttpContext.Current.Server.MapPath("/");
            karekodyaziornek.Save(path + "karekodweb.png", ImageFormat.Png);
            karekodmecardornek.Save(path + "karekodme.png", ImageFormat.Png);
            karekodsmsornek.Save(path + "karekodsms.png", ImageFormat.Png);
            karekodurlornek.Save(path + "karekodurl.png", ImageFormat.Png);
            karekodbookmark.Save(path + "karekodbook.png", ImageFormat.Png);
            karekodtelnoornek.Save(path + "karekodtelno.png", ImageFormat.Png);
            Image1.ImageUrl = "~/karekodweb.png";
            Image2.ImageUrl = "~/karekodme.png";
            Image3.ImageUrl = "~/karekodsms.png";
            Image4.ImageUrl = "~/karekodurl.png";
            Image5.ImageUrl = "~/karekodbook.png";
            Image6.ImageUrl = "~/karekodtelno.png";
        }
    }
}