using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_BankingSystem
{
    class LegalBody:Client
    {

        //public LegalBody(string lastName, 
        //    string firstName, 
        //    string patronymic, 
        //    DateTime dateBirth, 
        //    double startDeposit, 
        //    DateTime dateOfStartDeposit, 
        //    DateTime dateOfEndDeposit, 
        //    double percent, 
        //    bool isCapitalization,
        //    bool isVIP) : base(lastName,firstName,
        //        patronymic,dateBirth,startDeposit,dateOfStartDeposit,dateOfEndDeposit,percent,isCapitalization,isVIP)
        //{

        //}
        
        public LegalBody (string nameCompany, double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization, bool isVIP) 
            :base(nameCompany,startDeposit,dateOfStartDeposit,dateOfEndDeposit,percent,isCapitalization, isVIP)
        {
           NameCompany= nameCompany;
        }
        /// <summary>
        /// Название компании
        /// </summary>
        public string NameCompany { get; set; }
    }
}
