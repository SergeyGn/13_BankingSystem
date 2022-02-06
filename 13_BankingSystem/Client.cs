using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_BankingSystem
{
    public abstract class Client
    {
        //public Client(DateTime dateBirth, double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization)
        //{

        //    DateBirth = dateBirth;
        //    StartDeposit = startDeposit;
        //    DateOfStartDeposit = dateOfStartDeposit;
        //    DateOfEndDeposit = dateOfEndDeposit;
        //    AmountNow = GetAmonth(GetCountMonth(dateOfStartDeposit, DateTime.Now), startDeposit, percent);
        //    AmountEnd = GetAmonth(GetCountMonth(dateOfStartDeposit, dateOfEndDeposit), startDeposit, percent);
        //    Percent = percent;
        //    IsCapitalization = isCapitalization;
        //}

        public Client(string nameClient, double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization, bool isVIP)
        {
            NameClient = nameClient;
            if (startDeposit >= 0)
            {
                StartDeposit = startDeposit;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
            IsCapitalization = isCapitalization;

            DateOfStartDeposit = dateOfStartDeposit;
            DateOfEndDeposit = dateOfEndDeposit;
            AmountNow = GetAmonth(GetCountMonth(dateOfStartDeposit, DateTime.Now), startDeposit, percent);
            AmountEnd = GetAmonth(GetCountMonth(dateOfStartDeposit, dateOfEndDeposit), startDeposit, percent);
            CountMonth = GetCountMonth(dateOfStartDeposit, dateOfEndDeposit);
            IsVIP = isVIP;
            if (isVIP == true)
            {
                Percent = GetHigherPercent(percent,1.2);
            }
            else
            {
                Percent = percent;
            }
            
            
        }
       
        /// <summary>
        /// Имя/Название клиента
        /// </summary>
        public string NameClient { get; set; }

        /// <summary>
        /// Начальный вклад
        /// </summary>
        public double StartDeposit { get; set; }

        /// <summary>
        /// Дата вклада
        /// </summary>
        public DateTime DateOfStartDeposit { get; set; }

        /// <summary>
        /// Сумма вклада на текущий момент
        /// </summary>
        public double AmountNow { get; set; }

        /// <summary>
        /// Уровень начисления процентов без капитализации или с капитализацией
        /// </summary>
        public bool IsCapitalization { get; set; }

        /// <summary>
        /// Процент по вкладу
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// Дата завершения срока
        /// </summary>
        public DateTime DateOfEndDeposit { get; set; }

        /// <summary>
        /// Сумма после окончания срока вклада
        /// </summary>
        public double AmountEnd { get; set; }

        /// <summary>
        /// Кол-во месяцев на сколько открыт счёт
        /// </summary>
        public int CountMonth { get; set; }

        /// <summary>
        /// Id клиента
        /// </summary>
        public bool IsVIP { get; set; }

        private int GetCountMonth(DateTime dateStartDeposit, DateTime dateEndDeposit)
        {
            int countYear = dateEndDeposit.Year - dateStartDeposit.Year;
            int countMonth = dateEndDeposit.Month - dateStartDeposit.Month;
            int countDay = dateEndDeposit.Day - dateStartDeposit.Day;
            if(countYear>0)
            {
                countMonth += countYear * 12;
            }
            if (countDay < 0)
            {
                countMonth -= 1;
            }

            return countMonth;
        }

        private double GetAmonth(int countMonth, double deposit, double percent)
        {
            double monthPercent = percent / 12;
            if (countMonth > 0)
            {
                if (IsCapitalization == true)
                {
                    for (int i = 0; i < countMonth; i++)
                    {
                        deposit += deposit * monthPercent / 100;
                    }
                }
                else if (IsCapitalization == false)
                {
                    deposit += deposit * monthPercent * countMonth/100;
                }
            }
            return deposit;
        }

        private double GetHigherPercent(double percent, double coefficient)
        {
            percent *= coefficient;
            return percent;
        }
    }
}
