using EasyCredit.Constants;
using EasyCredit.Models;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyCredit.Infrastructure
{
    public class RequestProvider
    {
        private EasyCreditUnitOfWork unitOfWork;

        public RequestProvider(EasyCreditUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void SendRequest(Guid userId, Guid creditPlanId)
        {
            var creditPlan = unitOfWork.CreditPlansRepository.Get(creditPlanId);
            var user = unitOfWork.ApplicationUserRepositiry.Get(userId);
            var request = new Request
            {
                CreditPlan = creditPlan,
                StartDateOfRequest = DateTime.Now,
                StatusOfRequest = StatusRequestDictionary.Waiting,
                User = user
            };
            unitOfWork.RequestRepositiry.InsertOrUpdate(request);
            unitOfWork.Commit();
        }

        public void DenyRequest(Guid requestId)
        {
            var request = unitOfWork.RequestRepositiry.Get(requestId);
            request.StatusOfRequest = StatusRequestDictionary.Denied;
            unitOfWork.RequestRepositiry.InsertOrUpdate(request);
            unitOfWork.Commit();
        }

        public void AcceptRequest(Guid requestId)
        {
            var request = unitOfWork.RequestRepositiry.Get(requestId);
            request.StatusOfRequest = StatusRequestDictionary.Accepted;
            unitOfWork.RequestRepositiry.InsertOrUpdate(request);
            unitOfWork.Commit();
        }
    }
}