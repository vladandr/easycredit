using System.Collections.Generic;

using EasyCredit.Models.Identity;


namespace EasyCredit.Models
{
    public class BankAccount : EasyCreditEntity
    {
        public ApplicationUser User { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }

        public virtual ICollection<BankCard> BankCards { get; set; }

        public decimal Cash
        {
            get { return GetСash(); }
        }

        private decimal GetСash()
        {
            decimal availableCash = 0;
            foreach (var card in BankCards)
                availableCash = availableCash + card.Money;
            return availableCash;
        }
    }
}
