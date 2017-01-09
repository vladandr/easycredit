using System.ComponentModel.DataAnnotations;
using EasyCredit.Constants;

namespace EasyCredit.Models
{//ready
    public class CreditPlan : EasyCreditEntity
    {
        public string Description { get; set; }

        [Display(Name = "Name of plan")]
        public string NameOfPlan { get; set; }

        [Display(Name = "Minimum value to credit (BYR)")]
        public int MinValueToCredit { get; set; }

        [Display(Name = "Maximum value to credit (BYR)")]
        public int MaxValueToCredit { get; set; }

        [Display(Name = "Limit Days")]
        public int LimitDays { get; set; }

        [Display(Name = "Surcharge")]
        public int Surcharge { get; set; }

        [Display(Name = "Percentage")]
        public int Percentage { get; set; }

        public CreditPlanStatusDictionary Status { get; set; }
    }
}
