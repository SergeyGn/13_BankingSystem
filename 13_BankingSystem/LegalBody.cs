using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_BankingSystem
{
    class LegalBody:Client
    {
      
        public LegalBody (string nameCompany, double startDeposit, DateTime dateOfStartDeposit, DateTime dateOfEndDeposit, double percent, bool isCapitalization, bool isVIP) 
            :base(nameCompany,startDeposit,dateOfStartDeposit,dateOfEndDeposit,percent,isCapitalization, isVIP)
        {
            if(nameCompany!="")
           NameCompany= nameCompany;
            else 
            { 
                throw new IndexOutOfRangeException(); 
            }
        }

        /// <summary>
        /// Название компании
        /// </summary>
        public string NameCompany { get; set; }
    }
}
