using EasyCredit.Models.Identity;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EasyCredit.Infrastructure
{
    public class AdminProvider
    {
        private EasyCreditUnitOfWork unitOfWork;
        private const string password = "1qaz@WSX";
        public AdminProvider(EasyCreditUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Ban(Guid id)
        {
            var user = unitOfWork.ApplicationUserRepositiry.Get(id);
            user.LockoutEndDateUtc = DateTime.Now.AddYears(1);
            unitOfWork.ApplicationUserRepositiry.InsertOrUpdate(user);
            unitOfWork.Commit();
        }

        public void Unban(Guid id)
        {
            var user = unitOfWork.ApplicationUserRepositiry.Get(id);
            user.LockoutEndDateUtc = null;
            unitOfWork.ApplicationUserRepositiry.InsertOrUpdate(user);
            unitOfWork.Commit();
        }

        public void ResetToDefaultPassword(Guid id)
        {
            var userManager = new ApplicationUserManager(new ApplicationUserStore(new Contexts.ApplicationDbContext()));
            var user = unitOfWork.ApplicationUserRepositiry.Get(id);
            user.PasswordHash = userManager.PasswordHasher.HashPassword(password);
            unitOfWork.ApplicationUserRepositiry.InsertOrUpdate(user);
            unitOfWork.Commit();
        }
    }
}