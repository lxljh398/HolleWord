using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class BankCard
    {
        /// <summary>
        /// 判断银行卡
        /// http://blog.csdn.net/archer119/article/details/52832499
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string cardNumber = "6214835893420065";//银行卡号
            string url = $"https://ccdcapi.alipay.com/validateAndCacheCardInfo.json?_input_charset=utf-8&cardNo={cardNumber}&cardBinCheck=true";
            ResponseRet re = RequestUrl4Get(url);
            Console.WriteLine();
            Console.ReadLine();
        }

        private static ResponseRet RequestUrl4Get(string url)
        {
            ResponseRet re = new ResponseRet();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.Timeout = 5000;//5秒
                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream());
                    string srReturn = sr.ReadToEnd().Trim();
                    wr.Close();
                    sr.Close();
                    re = JsonConvert.DeserializeObject<ResponseRet>(srReturn);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
            return re;
        }

        /// <summary>
        /// 请求回调api接口返回状态
        /// </summary>
        private class ResponseRet
        {
            /// <summary>
            /// 银行PID
            /// </summary>
            public string Bank { get; set; }
            /// <summary>
            /// 是否可用
            /// </summary>
            public bool validated { get; set; }
            /// <summary>
            /// 卡类型
            /// </summary>
            public string cardType { get; set; }
            /// <summary>
            /// 银行key（不清楚）
            /// </summary>
            public string Key { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public string Stat { get; set; }
        }
    }
}
