using EasyCredit.Models;
using EasyCredit.Models.Identity;
using EasyCredit.Repositories;
using System.Data.Entity;

namespace EasyCredit.UnitOfWork
{
    public abstract class EasyCreditRepositoriesUnitOfWork
    {
        private DbContext context;
        protected Repository<CreditPlan> creditPlanRepository;
        protected Repository<ApplicationUser> applicationUserRepositiry;
        protected Repository<Request> requestRepositiry;


        public Repository<CreditPlan> CreditPlansRepository
        {
            get
            {
                if (creditPlanRepository == null)
                    creditPlanRepository = new Repository<CreditPlan>(context);
                return creditPlanRepository;
            }
        }

        public Repository<Request> RequestRepositiry
        {
            get
            {
                if (requestRepositiry == null)
                    requestRepositiry = new Repository<Request>(context);
                return requestRepositiry;
            }
        }

        public Repository<ApplicationUser> ApplicationUserRepositiry
        {
            get
            {
                if (applicationUserRepositiry == null)
                    applicationUserRepositiry = new Repository<ApplicationUser>(context);
                return applicationUserRepositiry;
            }
        }

        protected void RegisterRepositories(DbContext context)
        {
            this.context = context;
        }
    }
}
