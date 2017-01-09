using EasyCredit.Constants;
using EasyCredit.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace EasyCredit.Models
{
    public class Request : EasyCreditEntity
    {
        public virtual ApplicationUser User { get; set; }

        public virtual CreditPlan CreditPlan { get; set; }

        [Display(Name = "Start Date of Request")]
        public DateTime StartDateOfRequest { get; set; }

        public StatusRequestDictionary StatusOfRequest { get; set; }
    }
}
