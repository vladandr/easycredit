using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCredit.Models.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, 
    // please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, 
        ApplicationUserRole, ApplicationUserClaim>, IEasyCreditEntity
    {

        #region Card Code Second Factor Auth

        // todo to separate table
        // format 1111;2222;...9999
        public string CardCodes { get; set; }

        public int? SelectedCardCode { get; set; }

        public DateTime? CardCodeIssued { get; set; }

        #endregion
       
        public Gender? Gender { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Second name")]
        public string SecondName { get; set; }
        public uint Age { get; set; }

        [Display(Name = "Amount of children")]
        public uint AmountOfChildren { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        //public virtual ICollection<BankAccount> BankAccounts { get; set; }

        //public virtual ICollection<Children> Childrens { get; set; }

        //public virtual ICollection<BankCard> BankCards { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser, Guid> manager)
        {
            // Note the authenticationType must match the one defined in 
            // CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity 
                = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
