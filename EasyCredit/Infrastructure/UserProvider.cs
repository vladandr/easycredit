using EasyCredit.Models;
using EasyCredit.Models.Identity;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyCredit.Infrastructure
{
    public class UserProvider
    {
        private EasyCreditUnitOfWork unitOfWork;

        public UserProvider(EasyCreditUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void EditUser(ApplicationUser user)
        {
            unitOfWork.ApplicationUserRepositiry.InsertOrUpdate(user);
            unitOfWork.Commit();
        }

        public List<Request> ShowUserRequests(Guid userId)
        {
            var userRequests = unitOfWork.RequestRepositiry.GetAll().Where(x => x.User.Id == userId).ToList();
            return userRequests;
        }
    }
}
