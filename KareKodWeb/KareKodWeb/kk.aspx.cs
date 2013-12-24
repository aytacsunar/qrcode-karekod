using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KareKodDLL;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Security;

namespace KareKodWeb
{
    public partial class kk : System.Web.UI.Page
    {
        KareKod karekoddll = new KareKod();
        string path = HttpContext.Current.Server.MapPath("/");
        protected void Page_Load(object sender, EventArgs e)
        {
            //            Bitmap objImage = new Bitmap(path + "karekoduret.png");
            //    objImage.Save(Response.OutputStream,ImageFormat.Png);
            //    objImage.Dispose();

            //       karekoddll.KareKodYazi(TextBox1.Text.Trim());
            if (!IsPostBack)
            {
                string kodu = "KareKod API";
                int boyutu = 5;
                string arkarenk = "#FFFFFF";
                string renk = "#000000";
                if (Request.QueryString["kod"] != null)
                {
                    kodu = Request.QueryString["kod"].ToString();
                }
                if (Request.QueryString["boyut"] != null)
                {
                    boyutu = Convert.ToInt32(Request.QueryString["boyut"]);
                }
                if (Request.QueryString["renk"] != null)
                {
                    renk = Request.QueryString["renk"].ToString();
                }
                if (Request.QueryString["arkarenk"] != null)
                {
                    arkarenk = Request.QueryString["arkarenk"].ToString();
                }


                //FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox2.Text, "MD5");
                //Request.QueryString["Modul"].ToString()
                karekoddll.Renk = "#" + renk;
                karekoddll.ArkaRenk = "#" + arkarenk;
                karekoddll.Boyutu = boyutu;
                string yenikod = kodu.Substring(1, kodu.Length - 2);
                yenikod = Duzeltici(yenikod);
                Bitmap karekodolustur = karekoddll.KareKodYazi(yenikod);
                karekodolustur.Save(Response.OutputStream, ImageFormat.Jpeg);
                karekodolustur.Dispose();
            }

        }
        public static string Duzeltici(string text)
        {
            string sonuc = text.Replace("ç", "c").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("\"", "").Replace("ö", "o").Replace("ı", "i").Replace("I", "i").Replace("İ", "i").Replace("Ü", "u").Replace("Ç", "c").Replace("Ğ", "g");

            return sonuc;
        }
    }
}