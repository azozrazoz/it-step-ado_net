using System;
using System.Collections.Generic;
using System.Text;

namespace Practice
{
    public static class ConvertBoolean
    {
        public static string GetStrWithDot(double a)
        {
            string str = a.ToString();
            string str1 = "";
            string str2 = "";
            int temp = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ',') { temp = i; break; }
                else { str1 += str[i]; }
                if(i == str.Length - 1) { return str; }
            }
            for (temp += 1; temp < str.Length; temp++)
            {
                str2 += str[temp];
            }
            return str1 + '.' + str2;
        }
        public static string Convert(bool x) => x ? "True" : "False";
        public static bool Convert(string x) => x == "False" ? false : true;        
    }
}
