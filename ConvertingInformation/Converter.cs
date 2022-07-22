using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertingInformation
{
    public class Converter
    {
        public static DateTime GetDate(string[] array, string year, string month, string day)
        {
            int countMonth = 1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == month)
                {
                    countMonth = i + 1;
                }
            }
            DateTime date = new DateTime(int.Parse(year), countMonth, int.Parse(day));
            return date;
        }
        
        public static double ConvertingStringInDouble( string text)
        {
            if (double.TryParse(text, out double result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }
    }
}

