using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyCredit.Infrastructure
{
    public class AdminProvider
    {
        private EasyCreditUnitOfWork unitOfWork;
        private const string passord = "1qaz@WSX";
        public AdminProvider(EasyCreditUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Bun(Guid id)
        {
            var userToBan = unitOfWork.ApplicationUserRepositiry.Get(id);
            userToBan.LockoutEndDateUtc = DateTime.Now.AddMonths(1);
            unitOfWork.ApplicationUserRepositiry.InsertOrUpdate(userToBan);
            unitOfWork.Commit();
        }

        public void UnBun(Guid id)
        {
            var userToUnBan = unitOfWork.ApplicationUserRepositiry.Get(id);
            userToUnBan.LockoutEndDateUtc = null;
            unitOfWork.ApplicationUserRepositiry.InsertOrUpdate(userToUnBan);
            unitOfWork.Commit();
        }
    }
}