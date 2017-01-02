using EasyCredit.DAL.Context;
using EasyCredit.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCredit.DAL.UnitOfWork
{
    public abstract class EasyCreditRepositoriesUnitOfWork
    {
        public CreditRepository CreditRepository
        {
            get;
            set;
        }

        protected void RegisterRepositories(MainContext context)
        {
            CreditRepository = new CreditRepository(context);
        }
    }
}
