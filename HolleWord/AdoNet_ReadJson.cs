using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace HolleWord
{
    /// <summary>
    /// 此处命名不规范，为了更好说明类的作用而为之
    /// </summary>
    class AdoNet_ReadJson
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
                string strCmd = $"insert into provinces (Citycode,Name,Center,Adcode) values ('{item.citycode}','{item.name}','{item.center}','{item.adcode}')";
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







        #region ADO.NET帮助方法
        #region 1.1插入新数据
        private static void InsertData()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand sqlcmd = connection.CreateCommand())
                {
                    string strCmd = "insert into Customer (region,Name,Department) values ('jinan','li','shandong')";
                    sqlcmd.CommandText = strCmd;
                    if (sqlcmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("插入成功");
                    }
                    else
                    {
                        Console.WriteLine("插入失败");
                    }
                }
            }
        }
        #endregion

        #region 1.2插入新数据,Parameters方法
        private static void InsertDataParameters()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    string sqlStr = "insert into Customer (region,Name,Department) values (@region,@name,@depart)";
                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.Parameters.Add("@region", SqlDbType.NVarChar);
                    sqlCmd.Parameters["@region"].Value = "Sichuan";
                    sqlCmd.Parameters.AddWithValue("@name", "Chong");
                    sqlCmd.Parameters.AddWithValue("@depart", "qing");
                    if (sqlCmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("插入成功");
                    }
                    else
                    {
                        Console.WriteLine("插入失败");
                    }
                }
            }
        }
        #endregion

        #region 2 修改数据练习
        private static void updataData()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    string cmdStr = "update Customer set Name='@name' where id='2845'";
                    sqlCmd.CommandText = cmdStr;
                    if (sqlCmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("插入成功");
                    }
                    else
                    {
                        Console.WriteLine("插入失败");
                    }
                }
            }
        }
        #endregion

        #region 3 删除数据 练习
        private static void DelData()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand sqlcmd = connection.CreateCommand())
                {
                    string cmdStr = "delete from Customer where id >='2840'";
                    sqlcmd.CommandText = cmdStr;
                    if (sqlcmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("删除成功");
                    }
                    else
                    {
                        Console.WriteLine("删除失败");
                    }
                }
            }
        }
        #endregion

        #region 4.1 使用dataadapter,dataset查询数据
        private static void QueryDataByDs()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                string cmdStr = "select top 20 * from Customer";
                SqlDataAdapter sqlda = new SqlDataAdapter(cmdStr, strConn);
                DataSet ds = new DataSet();
                sqlda.Fill(ds, "customer");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Console.WriteLine("ID={0},region={1},department={2},name={3},phone1={4}", dr[0], dr[1], dr[2], dr[3], dr[4]);
                }
            }
        }
        #endregion

        #region 4.2 使用datareader查询数据
        private static void QueryByDr()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    string cmdStr = "select top 10 * from Customer";
                    sqlCmd.CommandText = cmdStr;
                    SqlDataReader sqlDr = sqlCmd.ExecuteReader();
                    while (sqlDr.HasRows)//是否返回数据
                    {
                        while (sqlDr.Read())//从第一行开始顺序读取数据集到最后一行
                        {
                            Console.WriteLine("ID={0},region={1},department={2},name={3},phone1={4}", sqlDr[0].ToString(), sqlDr[1].ToString(), sqlDr[2].ToString(), sqlDr[3].ToString(), sqlDr[4].ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #region 4.3 使用datatable查询数据
        private static void querybyDt()
        {
            SqlConnection connection = new SqlConnection(strConn);
            connection.Open();
            string cmdStr = "select top 10 * from Customer";
            SqlDataAdapter sqlda = new SqlDataAdapter(cmdStr, connection);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine("{0},{1},{2}", item[0].ToString(), item[1].ToString(), item[2].ToString());
            }
            connection.Close();
        }
        #endregion
        #endregion
    }
}
