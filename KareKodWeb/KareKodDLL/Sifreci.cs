using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace KareKodDLL
{
    class Sifreci
    {
        public static string MD5Hash(string DuzYazi)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(DuzYazi);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }
        public static string MD5Cevir(string DuzYazi)
        {
            MD5CryptoServiceProvider sifrele = new MD5CryptoServiceProvider();
            byte[] bdizi = Encoding.UTF8.GetBytes(DuzYazi);
            byte[] sifreliDizi = sifrele.ComputeHash(bdizi);
            string sifreliYazi = Convert.ToBase64String(sifreliDizi);
            return sifreliYazi;
        }
        public static string SHA1Cevir(string DuzYazi)
        {
            SHA1CryptoServiceProvider sifrele = new SHA1CryptoServiceProvider();
            byte[] bdizi = Encoding.UTF8.GetBytes(DuzYazi);
            byte[] sifreliDizi = sifrele.ComputeHash(bdizi);
            string sifreliYazi = Convert.ToBase64String(sifreliDizi);
            return sifreliYazi;
        }
        public static string SHA512Cevir(string DuzYazi)
        {
            SHA512CryptoServiceProvider sifrele = new SHA512CryptoServiceProvider();
            byte[] bdizi = Encoding.UTF8.GetBytes(DuzYazi);
            byte[] sifreliDizi = sifrele.ComputeHash(bdizi);
            string sifreliYazi = Convert.ToBase64String(sifreliDizi);
            return sifreliYazi;
        }
    }
}