using System;
using System.Linq;

using EasyCredit.Models;


namespace EasyCredit.Repositories
{
    public interface IRepository<T> where T : IEasyCreditEntity
    {
        IQueryable<T> GetAll();

        T Get(Guid id);

        void Create(T entity);

        bool Exists(T entity);

        void InsertOrUpdate(T entity);

        void DeleteById(Guid id);
    }
}
