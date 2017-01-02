using EasyCredit.Constants;
using EasyCredit.Models.Identity;
using System;

namespace EasyCredit.Models
{
    public class Request : EasyCreditEntity
    {
        public virtual ApplicationUser User { get; set; }
        public virtual CreditPlan CreditPlan { get; set; }
        public DateTime StartDateOfRequest { get; set; }
        public StatusRequestDictionary StatusOfRequest { get; set; }
    }
}
