using System.ComponentModel.DataAnnotations;
using EasyCredit.Constants;

namespace EasyCredit.Models
{//ready
    public class CreditPlan : EasyCreditEntity
    {
        public string Description { get; set; }

        [Display(Name = "Name of plan")]
        public string NameOfPlan { get; set; }

        [Display(Name = "Minimum value to credit")]
        public int MinValueToCredit { get; set; }

        [Display(Name = "Maximum value to credit")]
        public int MaxValueToCredit { get; set; }

        public int LimitDays { get; set; }

        public int Surcharge { get; set; }

        public int Percentage { get; set; }

        public CreditPlanStatusDictionary Status { get; set; }
    }
}
