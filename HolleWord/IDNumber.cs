using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    /// <summary>
    /// 第一步：将前 17 位依次填入下面的方框[#*7+#*9+#*10+#*5+#*8+#*4+#*2+#*1+#*6+#*3+#*7+#*9+#*10+#*5+#*8+#*4+#*2 （#为身份证号码)]，完成算式：
    /// （为便于观察，算式分为四行：地址、出生年、生日、序列性别）
    /// 第二步：上式计算得出的数值，除以 11，取其余数；
    /// 第三步：用 12 减去该余数，如果结果在 2～10 之间，则该结果即为校验码（注意 10 用罗马数字 X 表示）；如果结果为 11、12，则再减去 11，最终得到的 0、1 即为校验码。
    /// </summary>
    class IDNumber
    {
        readonly static int[] validate = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        static void Main(string[] args)
        {
            string iDNumber = "xxxxxxxxxxxxxxxxxx";//身份证号码
            int stringIndexValue = 18 - 1;//字符串下标值
            int reckonValue = 0;
            string formula = null;
            //第一步
            for (int i = 0; i < stringIndexValue; i++)
            {
                reckonValue += int.Parse(iDNumber[i].ToString()) * validate[i];
                formula += $"{iDNumber[i]}*{validate[i]}+";
            }
            Console.WriteLine(formula.TrimEnd('+'));
            //第二步
            int remainder = reckonValue % 11;
            //第三步
            int iDNumberLastNum = 12 - remainder;
            if (iDNumberLastNum > 10)
                iDNumberLastNum = iDNumberLastNum - 11;
            Console.WriteLine("最后一位正确值为："+iDNumberLastNum);
            
            string lastChar = iDNumberLastNum == 10 ? "X" : iDNumberLastNum.ToString();
            if (lastChar.Equals(iDNumber[stringIndexValue].ToString().ToUpper()))
                Console.WriteLine("当前的身份证ID：正确");
            else
                Console.WriteLine("当前的身份证ID：错误");
            Console.ReadLine();
        }
    }
}
