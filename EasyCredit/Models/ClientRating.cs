using EasyCredit.Models.Identity;


namespace EasyCredit.Models
{//ready
    public class ClientRating: EasyCreditEntity
    {
        public ApplicationUser User;
        public double Rating;
    }
}
