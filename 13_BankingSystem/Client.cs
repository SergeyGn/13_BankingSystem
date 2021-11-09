using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_BankingSystem
{
    public class Client
    {
        public Client(string lastName, string firstName, string patronymic, DateTime dateBirth, double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization)
        {
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            DateBirth = dateBirth;
            StartDeposit = startDeposit;
            DateOfStartDeposit = dateOfStartDeposit;
            DateOfEndDeposit = dateOfEndDeposit;
            AmountNow = GetAmonth(GetCountMonth(dateOfStartDeposit, DateTime.Now), startDeposit, percent);
            AmountEnd = GetAmonth(GetCountMonth(dateOfStartDeposit, dateOfEndDeposit), startDeposit, percent);
            Percent = percent;
            IsCapitalization = isCapitalization;
        }

        public Client(double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization)
        {

            StartDeposit = startDeposit;
            DateOfStartDeposit = dateOfStartDeposit;
            DateOfEndDeposit = dateOfEndDeposit;
            AmountNow = GetAmonth(GetCountMonth(dateOfStartDeposit, DateTime.Now), startDeposit, percent);
            AmountEnd = GetAmonth(GetCountMonth(dateOfStartDeposit, dateOfEndDeposit), startDeposit, percent);
            Percent = percent;
            IsCapitalization = isCapitalization;
        }
        /// <summary>
        /// Фмилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateBirth { get; set; }

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
        private DateTime DateOfEndDeposit { get; set; }

        /// <summary>
        /// Сумма после окончанияS срока вклада
        /// </summary>
        private double AmountEnd { get; set; }

        private int GetCountMonth(DateTime dateStartDeposit, DateTime dateEndDeposit)
        {
            int countYear = dateEndDeposit.Year - dateStartDeposit.Year;
            int countMonth = dateEndDeposit.Month - dateStartDeposit.Month;
            int countDay = dateEndDeposit.Day - dateStartDeposit.Day;

            if (countDay < 0)
            {
                countMonth -= 1;
            }

            return countMonth + countYear * 12;
        }

        private double GetAmonth(int countMonth, double deposit, double percent)
        {
            if (IsCapitalization == true)
            {
                for (int i = 0; i <= countMonth; i++)
                {
                    deposit += deposit * percent/100;
                }
            }
            else if (IsCapitalization == false)
            {
                double value = deposit;
                for (int i = 0; i <= countMonth; i++)
                {
                    deposit += value * percent/100;
                }
            }
            return deposit;
        }
    }
}
