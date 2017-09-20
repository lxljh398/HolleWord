using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class Math
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(System.Math.Pow(2, 0));
            //Console.WriteLine(System.Math.Pow(2, 1));
            //Console.WriteLine(System.Math.Pow(2, 2));
            //Console.WriteLine(System.Math.Pow(2, 3));
            //Console.WriteLine(System.Math.Pow(2, 4));
            //Console.WriteLine(System.Math.Pow(2, 5));
            //Console.ReadLine();

            FunctionOperate permission = FunctionOperate.Add | FunctionOperate.Delete | FunctionOperate.Update | FunctionOperate.Query;
            Console.WriteLine((int)permission);
            Console.ReadLine();
        }



        /// <summary>
        /// 功能可操作的功能
        /// http://www.cnblogs.com/youring2/archive/2011/12/16/2289832.html
        /// </summary>
        [Flags]
        public enum FunctionOperate
        {
            /// <summary>
            /// 无操作权限
            /// </summary>
            None = 0,
            /// <summary>
            /// 增
            /// </summary>
            Add = 1,
            /// <summary>
            /// 删
            /// </summary>
            Delete = 2,
            /// <summary>
            /// 改
            /// </summary>
            Update = 4,
            /// <summary>
            /// 查
            /// </summary>
            Query = 8
        }
    }
}
