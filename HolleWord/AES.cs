using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class AES
    {
        //http://www.cnblogs.com/lzrabbit/p/3639503.html   Java、C#双语版配套AES加解密示例

        //这里采用的加解密使用base64转码方法，ECB模式，PKCS5Padding填充，密码必须是16位，否则会报错哈
        //模式：Java的ECB对应C#的System.Security.Cryptography.CipherMode.ECB
        //填充方法：Java的PKCS5Padding对应C#System.Security.Cryptography.PaddingMode.PKCS7

        static void Main(string[] args)
        {
            Console.WriteLine(AesEncrypt("123123", "euNirWrWgUnbTR/t2ITQ6OZSTejAKDT2"));
            Console.ReadKey();
            //AESFlag();//公司内部
        }



        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">要加密的内容</param>
        /// <param name="key">两种方式</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            //方法一：只使用key加密
            //System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            //{
            //    Key = Encoding.UTF8.GetBytes(key),
            //    Mode = System.Security.Cryptography.CipherMode.CBC,//Mode = System.Security.Cryptography.CipherMode.ECB,
            //    Padding = System.Security.Cryptography.PaddingMode.PKCS7
            //};
            //System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            //Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);


            //方法二：使用key和向量IV加密
            using (RijndaelManaged rm = new RijndaelManaged())
            {
                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor(rgbKey, rgbIv);
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }
        /// <summary>
        ///  AES 解密(对应加密方法)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            //System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            //{
            //    Key = Encoding.UTF8.GetBytes(key),
            //    Mode = System.Security.Cryptography.CipherMode.ECB,
            //    Padding = System.Security.Cryptography.PaddingMode.PKCS7
            //};
            //System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor(rgbKey, rgbIv);
            //Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //return Encoding.UTF8.GetString(resultArray);


            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                ICryptoTransform cTransform = rDel.CreateDecryptor(rgbKey, rgbIv);
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
        }




        private static void AESFlag()
        {
            if (MD5Encrypt(Decrypt("juUiWKUMGm45QOGHDCqQcA=="), "U#fd7j*pK=3.").Equals("f8ef29a41b92a03cac510bb5b4afb11f", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private static byte[] rgbKey = Convert.FromBase64String("euNirWrWgUnbTR/t2ITQ6OZSTejAKDT2");
        private static byte[] rgbIv = Convert.FromBase64String("viAeTPfv0smQjeY2xiiW0g==");
        private static string Decrypt(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                ICryptoTransform cTransform = rDel.CreateDecryptor(rgbKey, rgbIv);
                byte[] resultArray = cTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
        }

        private static string MD5Encrypt(string str, string encrypt)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str + encrypt));
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
