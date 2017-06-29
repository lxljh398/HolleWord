using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Demo
{
    class Json
    {
        static void Main(string[] args)
        {
        }


        public string GetJsonString()
        {
            List<Product> products = new List<Product>(){
    new Product(){Name="苹果",Price=5.5},
    new Product(){Name="橘子",Price=2.5},
    new Product(){Name="干柿子",Price=16.00}
    };
            ProductList productlist = new ProductList();
            productlist.GetProducts = products;
            return new JavaScriptSerializer().Serialize(productlist));
        }

        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        public class ProductList
        {
            public List<Product> GetProducts { get; set; }
        }



        public static List<T> JSONStringToList<T>(this string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        string JsonStr = "[{Name:'苹果',Price:5.5},{Name:'橘子',Price:2.5},{Name:'柿子',Price:16}]";
        List<Product> products = new List<Product>();
        products = JSONStringToList<Product>(JsonStr);  
 
foreach (var item in products)  
{  
    Response.Write(item.Name + ":" + item.Price + "<br />");  
}
}
}
