using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class CSharp6
    {
        static void Main(string[] args)
        {
            //string name = "lixin";
            ////Console.WriteLine("我的名字："+ name);
            //Console.WriteLine(string.Format("我的名字：{0}", name));
            //Console.WriteLine($"我的名字：{name}");
            //Console.WriteLine($"我的名字：{Say(name)}");
            //Console.ReadLine();



            //Double remain = 2000.5;
            //var results = ChineseText($"your money is {remain:C}");
            //Console.WriteLine(results);
            //Console.Read();


            //User user = null;
            //List<User> list = null;
            //Console.WriteLine(list?[0].Name);
            //Console.WriteLine(list?[0].Age);
            //user?.SayHello();


            Console.WriteLine(SetString());
            Console.WriteLine(Print());
            Console.ReadLine();

        }

        private static string SetString() => "表达式方法体";
        private static string Print() => $"调用方法 { Say("say方法")}";

        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; } = 25;
            public void SayHello()
            {
                Console.WriteLine("Ha Ha");
            }
        }


        public static string ChineseText(IFormattable formattable)
        {
            return formattable.ToString(null, new CultureInfo("zh-cn"));
        }


        private static string Say(string name)
        {
            return name;
        }
    }
}
