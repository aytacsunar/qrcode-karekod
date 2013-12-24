using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KareKodDLL;
using System.Drawing;
using System.Drawing.Imaging;

namespace KareKodWeb
{
    public partial class KareKodUret : System.Web.UI.Page
    {
        KareKod karekoddll = new KareKod();
        string path = HttpContext.Current.Server.MapPath("/");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bitmap karekodyaziornek = karekoddll.KareKodYazi("Bu bir KareKodWeb denemesidir!");
                karekodyaziornek.Save(path + "karekoduret.png", ImageFormat.Png);
                Image1.ImageUrl = "~/karekoduret.png";
            }
            else
            {
                karekoddll.Boyutu = Convert.ToInt32(DropDownList1.SelectedValue);
                if (TextBox3.Text.Trim() == "")
                {
                    karekoddll.Renk = "#000000";
                }
                else
                {
                karekoddll.Renk = "#" + TextBox3.Text.Trim();
                }
                if (TextBox2.Text.Trim() == "")
                {
                    karekoddll.ArkaRenk = "#FFFFFF";
                }
                else
                {
                karekoddll.ArkaRenk = "#" + TextBox2.Text.Trim();
                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Bitmap karekoduretiyor = karekoddll.KareKodYazi(TextBox1.Text.Trim());
            karekoddll.Boyutu = Convert.ToInt32(DropDownList1.SelectedValue);
            karekoddll.ArkaRenk = "#"+TextBox2.Text.Trim();
            karekoddll.Renk = "#" + TextBox3.Text.Trim();
            karekoduretiyor.Save(path + "karekoduretilmis.png", ImageFormat.Png);
            Image1.ImageUrl = "~/karekoduretilmis.png";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KareKod karekoddll2 = new KareKod();
            karekoddll2.Boyutu = Convert.ToInt32( DropDownList1.SelectedValue);
            Bitmap karekoduretiyor = karekoddll2.KareKodYazi(TextBox1.Text.Trim());
            karekoduretiyor.Save(path + "karekoduretilmis.png", ImageFormat.Png);
            Image1.ImageUrl = "~/karekoduretilmis.png";
        }
    }
}