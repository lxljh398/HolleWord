using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class Reflection
    {
        static void Main(string[] args)
        {
            User user1 = new User() { Name = "111", Age = 30 };
            Type type = typeof(User);
            User user2 = new User();
            foreach (System.Reflection.PropertyInfo info in type.GetProperties())
            {
                info.SetValue(user2, info.GetValue(user1, null), null);
            }
            Console.WriteLine(user2.Name);
            Console.WriteLine(user2.Age);
            Console.ReadLine();
        }

        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; } = 25;
        }
    }
}
