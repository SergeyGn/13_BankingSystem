using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_BankingSystem
{
    public class Person:Client
    {
        private static int idNumber=0; 
       public Person(string name,double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization, bool isVIP) 
            :base(name,startDeposit,dateOfStartDeposit,dateOfEndDeposit,percent,isCapitalization,isVIP)
        {
            name = "";
        }

        public Person(string lastName,
            string firstName, 
            string patronymic,
            DateTime birthday,
            double startDeposit, 
            DateTime dateOfStartDeposit, 
            DateTime dateOfEndDeposit, 
            double percent, bool 
            isCapitalization, bool 
            isVIP)
    : base($"{lastName} {firstName} {patronymic}" ,startDeposit, dateOfStartDeposit, dateOfEndDeposit, percent, isCapitalization, isVIP)
        {
            if (lastName != "")
            {
                LastName = lastName;
            }
            if (firstName != "") FirstName = firstName;
            if (patronymic != "") Patronymic = patronymic;
            else
            {
                throw new IndexOutOfRangeException();
            }
            


            DateBirth = birthday;

        }

        /// <summary>
        /// Фамилия
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
    }
}
