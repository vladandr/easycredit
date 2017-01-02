using System;

namespace EasyCredit.DAL.Models
{
    public class BankCard: EasyCreditEntity
    {
        public string CardNumber { get; set; }
        public Guid? UserId { get; set; }
    }
}
