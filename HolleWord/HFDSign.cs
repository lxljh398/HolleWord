using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class HFDSign
    {
        private static SortedDictionary<string, string> txtParams = null;
        static void Main(string[] args)
        {
            //txtParams = new SortedDictionary<string, string>();
            //txtParams.Add("phone_number", "13735471883");
            //txtParams.Add("card_worth", "50");
            //txtParams.Add("sp_order_id", "17072715434056344017");
            //txtParams.Add("api_key", "wiC0xDTdKdMfdpnPyuc2BgpS33BMdTAA34Apvis7m6bqjo6HI9y0dNS7EiHI289S");
            //Console.WriteLine(MakeSign());//915980b1c1df007dcc1dbf042d6d77bd


            Console.WriteLine(GetUnixTime());
            Console.ReadLine();
        }

        private static string GetUnixTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime validTime = DateTime.Now;
            Random random = new Random();
            return ((int)(validTime - startTime).TotalSeconds).ToString() + random.Next(0, 100000).ToString().PadLeft(5, '0'); //稍微做了处理
        }

        public static int GetUnixTime(int mins)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime validTime = DateTime.Now.AddMinutes(mins);
            return (int)(validTime - startTime).TotalSeconds;
        }

        public static DateTime ConverUnixTimeToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        public static string MakeSign()
        {
            //转url格式
            string str = ChangeUrlParam(true).Replace("&", "").Replace("=", "");
            //在string后加入SECRET_KEY
            str += "1y9kHqfTkg9tv5242o463K1wSx6Ju4rjrotYEzHOGF92zYUqcMk5laq5ylCSdT2v";
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        public static string ChangeUrlParam(bool makeSignFlag)
        {
            string buff = "";
            if (makeSignFlag)
                txtParams.Remove("sign");
            foreach (System.Collections.Generic.KeyValuePair<string, string> pair in txtParams)
                buff += pair.Key + "=" + pair.Value + "&";
            buff = buff.Trim('&');
            return buff;
        }
    }
}
