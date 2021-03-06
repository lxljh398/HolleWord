﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class RNGCryptoServiceProviderDemo
    {
        static void Main(string[] args)
        {
            //方法一：
            //byte[] randomBytes = new byte[4];
            //RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            //rngCrypto.GetBytes(randomBytes);
            //Int32 rngNum = BitConverter.ToInt32(randomBytes, 0);



            //方法二：
            //for (int i = 0; i < 10000; i++)
            //{
            //    int rngNum = GetRnd();
            //    Console.WriteLine(rngNum + "----" + rngNum.ToString().Length);
            //}


            //方法三：
            //int iSeed = 10;
            //long tick = DateTime.Now.Ticks;
            //Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            //for (int i = 0; i < 1000; i++)
            //{
            //    iSeed = ran.Next();
            //    Console.WriteLine(iSeed + "----" + iSeed.ToString().Length);
            //}
            //方法四：
            //Console.WriteLine(Generate("1234567890",1));
            //Random random = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    int color = random.Next(0, 4) + 1;
            //    Console.WriteLine(color);
            //}
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();
        }

        private static int GetRnd()
        {
            //这样产生0 ~ 1000000的强随机数（含1000000）
            int max = 1000000;
            int rnd = int.MinValue;
            decimal _base = (decimal)long.MaxValue;
            byte[] rndSeries = new byte[8];
            //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //rng.Dispose();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(rndSeries);
                //不含1000000需去掉+1
                rnd = (int)(Math.Abs(BitConverter.ToInt64(rndSeries, 0)) / _base * (max + 1));
                //这个rnd就是你要的随机数，
                //但是要注意别扔到循环里去，实例化RNG对象可是很消耗资源的
                return rnd;                
            }
        }

        public static string GetUniqueIdentifier(int length)
        {
            int maxSize = length;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            if (result[0] >= '0' && result[0] <= '9')
            {
                return GetUniqueIdentifier(length);
            }
            return result.ToString();
        }
        public static string Generate(string dictionary, int length)
        {
            string result = "";
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[8];
            int max = dictionary.Length - 1;
            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(data);
                int rnd = (int)Math.Round(Math.Abs(BitConverter.ToInt64(data, 0)) / (decimal)long.MaxValue * max, 0);
                result += dictionary.Substring(rnd, 1);
            }
            return result;
        }
        ///<summary>
        ///得到随机字符.
        ///</summary>
        ///<param name="intLength">Length of the int.</param>
        ///<param name="booNumber">if set to <c>true</c> [boo number].</param>
        ///<param name="booSign">if set to <c>true</c> [boo sign].</param>
        ///<param name="booSmallword">if set to <c>true</c> [boo smallword].</param>
        ///<param name="booBigword">if set to <c>true</c> [boo bigword].</param>
        ///<returns></returns>
        public string getRandomizer(int intLength, bool booNumber, bool booSign, bool booSmallword, bool booBigword)
        {
            //定义
            Random ranA = new Random();
            int intResultRound = 0;
            int intA = 0;
            string strB = "";
            while (intResultRound < intLength)
            {
                //生成随机数A，表示生成类型
                //1=数字，2=符号，3=小写字母，4=大写字母
                intA = ranA.Next(1, 5);
                //如果随机数A=1，则运行生成数字
                //生成随机数A，范围在0-10
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 1 && booNumber)
                {
                    intA = ranA.Next(0, 10);
                    strB = intA.ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
                //如果随机数A=2，则运行生成符号
                //生成随机数A，表示生成值域
                //1：33-47值域，2：58-64值域，3：91-96值域，4：123-126值域
                if (intA == 2 && booSign == true)
                {
                    intA = ranA.Next(1, 5);
                    //如果A=1
                    //生成随机数A，33-47的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 1)
                    {
                        intA = ranA.Next(33, 48);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=2
                    //生成随机数A，58-64的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 2)
                    {
                        intA = ranA.Next(58, 65);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=3
                    //生成随机数A，91-96的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 3)
                    {
                        intA = ranA.Next(91, 97);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=4
                    //生成随机数A，123-126的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 4)
                    {
                        intA = ranA.Next(123, 127);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                }

                //如果随机数A=3，则运行生成小写字母
                //生成随机数A，范围在97-122
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 3 && booSmallword == true)
                {
                    intA = ranA.Next(97, 123);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=4，则运行生成大写字母
                //生成随机数A，范围在65-90
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 4 && booBigword == true)
                {
                    intA = ranA.Next(65, 89);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
            }
            return strB;
        }
        ///<summary>
        ///生成随机字符串
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        ///随机字符串生成器的主要功能如下： 
        ///1、支持自定义字符串长度
        ///2、支持自定义是否包含数字
        ///3、支持自定义是否包含小写字母
        ///4、支持自定义是否包含大写字母
        ///5、支持自定义是否包含特殊符号
        ///6、支持自定义字符集
        public string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
    }
}
