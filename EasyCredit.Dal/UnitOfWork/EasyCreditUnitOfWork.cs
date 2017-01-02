using EasyCredit.DAL.Context;
using System;

namespace EasyCredit.DAL.UnitOfWork
{
    public class EasyCreditUnitOfWork : EasyCreditRepositoriesUnitOfWork, IDisposable
    {
        private readonly MainContext context;
        private bool disposed;

        public EasyCreditUnitOfWork(MainContext context)
        {
            this.context = context;
            RegisterRepositories(context);
        }

        public EasyCreditUnitOfWork()
        {
            context = new MainContext();
            RegisterRepositories(context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
