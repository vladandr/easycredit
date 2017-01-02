using EasyCredit.Contexts;
using System;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace EasyCredit.UnitOfWork
{
    public class EasyCreditUnitOfWork : EasyCreditRepositoriesUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private bool _disposed;        

        public EasyCreditUnitOfWork()
        {
            RegisterRepositories(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
