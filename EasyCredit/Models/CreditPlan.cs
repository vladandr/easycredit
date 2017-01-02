using EasyCredit.Constants;

namespace EasyCredit.Models
{//ready
    public class CreditPlan : EasyCreditEntity
    {
        public string Description { get; set; }
        public string NameOfPlan { get; set; }
        public int MinValueToCredit { get; set; }
        public int MaxValueToCredit { get; set; }
        public int LimitDays { get; set; }
        public int Surcharge { get; set; }
        public int Percentage { get; set; }
        public CreditPlanStatusDictionary Status { get; set; }
    }
}
