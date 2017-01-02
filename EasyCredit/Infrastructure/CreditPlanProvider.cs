using EasyCredit.EasyCreditAssertionGroup;
using EasyCredit.Models;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyCredit.Infrastructure
{
    public class CreditPlanProvider
    {
        private EasyCreditUnitOfWork unitOfWork;

        public CreditPlanProvider(EasyCreditUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void InsertOrUpdateCreditPlan(CreditPlan planToInsertOrUpdate)
        {
            unitOfWork.CreditPlansRepository.InsertOrUpdate(planToInsertOrUpdate);
            unitOfWork.Commit();
        }

        public void DeleteCreditPlan(Guid id)
        {
            unitOfWork.CreditPlansRepository.DeleteById(id);
            unitOfWork.Commit();
        }

        public void SendToArchiveCreditPlan(Guid id)
        {
            var creditPlan = unitOfWork.CreditPlansRepository.Get(id);
            creditPlan.ThrowIfArgumentIsNull(nameof(creditPlan));
            creditPlan.Status = Constants.CreditPlanStatusDictionary.InHistory;
            unitOfWork.CreditPlansRepository.InsertOrUpdate(creditPlan);
            unitOfWork.Commit();
        }

        public List<CreditPlan> ShowRecentlyCreatedCreditPlans()
        {
            var recentlyCreated = unitOfWork.CreditPlansRepository.GetAll().Reverse().Take(5).ToList();
            return recentlyCreated;
        }
    }
}