using EasyCredit.Models.Identity;


namespace EasyCredit.Models
{
    //ready
    public class BankCard : EasyCreditEntity
    {
        public decimal Money { get; set; }

        public string CardNumber { get; set; }

        public ApplicationUser User { get; set; }

        public BankAccount Account { get; set; }
    }
}
