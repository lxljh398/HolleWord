﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class BaseMethod
    {
        static void Main(string[] args)
        {
            string val = @"来来来";
            var a = EncodeBase64(val);


            string val2 = @"5ZWm5ZKv5ZWm5ZKv5ZWm5ZKv8J+Yg\/CfmIPwn5iD8J+Yg\/CfmIM=";
            var b = DecodeBase64(val2);
        }

        public static string EncodeBase64(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }
        public static string DecodeBase64(string result)
        {
            byte[] outputb = Convert.FromBase64String(result);
            return Encoding.UTF8.GetString(outputb);
        }

        ///// <summary>
        ///// Base64加密
        ///// </summary>
        ///// <param name="codeName">加密采用的编码方式</param>
        ///// <param name="source">待加密的明文</param>
        ///// <returns></returns>
        //public static string EncodeBase64(Encoding encode, string source)
        //{
        //    byte[] bytes = encode.GetBytes(source);
        //    try
        //    {
        //        encode = Convert.ToBase64String(bytes);
        //    }
        //    catch
        //    {
        //        encode = source;
        //    }
        //    return encode;
        //}

        ///// <summary>
        ///// Base64加密，采用utf8编码方式加密
        ///// </summary>
        ///// <param name="source">待加密的明文</param>
        ///// <returns>加密后的字符串</returns>
        //public static string EncodeBase64(string source)
        //{
        //    return EncodeBase64(Encoding.UTF8, source);
        //}

        ///// <summary>
        ///// Base64解密
        ///// </summary>
        ///// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        ///// <param name="result">待解密的密文</param>
        ///// <returns>解密后的字符串</returns>
        //public static string DecodeBase64(Encoding encode, string result)
        //{
        //    string decode = "";
        //    byte[] bytes = Convert.FromBase64String(result);
        //    try
        //    {
        //        decode = encode.GetString(bytes);
        //    }
        //    catch
        //    {
        //        decode = result;
        //    }
        //    return decode;
        //}

        ///// <summary>
        ///// Base64解密，采用utf8编码方式解密
        ///// </summary>
        ///// <param name="result">待解密的密文</param>
        ///// <returns>解密后的字符串</returns>
        //public static string DecodeBase64(string result)
        //{
        //    return DecodeBase64(Encoding.UTF8, result);
        //}
    }
}
