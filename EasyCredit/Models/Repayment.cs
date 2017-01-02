using System;

namespace EasyCredit.Models
{//ready
    public class Repayment : EasyCreditEntity
    {
        public BankAccount Account { get; set; }
        public Credit Credit { get; set; }
        public decimal AmountOfRepayment { get; set; }

        public DateTime DateOfRepayment
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
