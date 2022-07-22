using System;

namespace Convertering
{
    public class ConverteringInformation
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
        //в библиотеку
        private double ConvertingStringInDouble(string errorText, string text)
        {
            if (double.TryParse(text, out double result))
            {
                return result;
            }
            else
            {
                errorText = "Некорректное значение";
                return -1;
            }
        }
    }
}
