using System;

namespace EasyCredit.Models
{//ready
    public class Credit: EasyCreditEntity
    {
        public CreditPlan CreditPlan { get; set; }
        public BankAccount BankAccount { get; set; }
        public DateTime StartDateOfCredit { get; set; }
        public DateTime? EndDateOfCredit { get; set; }
        public decimal NeededCash { get; set; }
        public decimal TotalCash { get; set; }
    }
}
