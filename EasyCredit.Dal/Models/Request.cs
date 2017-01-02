using EasyCredit.DAL.Constants;
using System;

namespace EasyCredit.DAL.Models
{
    public class Request : EasyCreditEntity
    {
        public BankAccount BankAccount { get; set; }
        public CreditPlan CreditPlan { get; set; }
        public DateTime StartDateOfRequest { get; set; }
        public StatusRequestDictionary StatusOfRequest { get; set; }
    }
}
