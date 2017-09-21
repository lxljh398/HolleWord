using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            // Start OWIN host   
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("OWIN SERVICE OPEN!");
                Console.Read();
            }
            Console.ReadLine();
        }
    }
}
