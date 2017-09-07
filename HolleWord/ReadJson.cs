using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace HolleWord
{
    class ReadJson
    {

        static string strConn = @"User Id=root;host=127.0.0.1;Database=Address1;password=123123;port=3306;persist security info=True;character set=utf8;";
        //static string strConn = @"User Id = root; host=139.196.191.193;Database=AlphaWallet_hu8064;password=6K9PH6k9pd95s21z56dwjslq4;port=3306;persist security info=True;character set = utf8;";
        static void Main(string[] args)
        {
            //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ////执行反序列化
            //Address obj = jsonSerializer.Deserialize<Address>(GetFileJson(@"E:\bj.json"));
            //var ret = obj;


            Address obj = JsonConvert.DeserializeObject<Address>(GetFileJson(@"E:\ChinaAddress.json"));
            foreach (var item in obj.districts)
            {
                string strCmd = $"insert into province (Citycode,Name,Center,Adcode) values ('{item.citycode}','{item.name}','{item.center}','{item.adcode}')";
                long provinceid = InsertData(strCmd);
                foreach (var item1 in item.districts)
                {
                    strCmd = $"insert into cities (provinceid,Citycode,Name,Center,Adcode) values ('{provinceid}','{item1.citycode}','{item1.name}','{item1.center}','{item1.adcode}')";
                    long cityid = InsertData(strCmd);
                    foreach (var item2 in item1.districts)
                    {
                        strCmd = $"insert into districts (provinceid,cityid,Citycode,Name,Center,Adcode) values ('{provinceid}','{cityid}','{item2.citycode}','{item2.name}','{item2.center}','{item2.adcode}')";
                        long districtid = InsertData(strCmd);
                        foreach (var item3 in item2.districts)
                        {
                            strCmd = $"insert into streets (provinceid,cityid,districtid,Citycode,Name,Center,Adcode) values ('{provinceid}','{cityid}','{districtid}','{item3.citycode}','{item3.name}','{item3.center}','{item3.adcode}')";
                            InsertData(strCmd);
                        }
                    }
                }
            }


            var ret = obj;
        }

        public static string GetFileJson(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }


        private static long InsertData(string strCmd)
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(strCmd, conn))
                {
                    //using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    //{
                    //    while (dataReader.Read())
                    //    {
                    //        string obj = (string)dataReader[1];
                    //        Console.WriteLine(obj);
                    //    }
                    //}
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return cmd.LastInsertedId;
                    }
                    else
                    {
                        Console.WriteLine("插入失败");
                        return 0;
                    }
                }
            }
        }

        public class Address
        {
            public string citycode { get; set; }
            public string adcode { get; set; }
            public string name { get; set; }
            public string center { get; set; }
            public string level { get; set; }
            public List<Address> districts { get; set; }
        }
    }
}
