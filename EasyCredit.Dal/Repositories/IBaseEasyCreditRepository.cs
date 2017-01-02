using System;
using System.Collections.Generic;

namespace EasyCredit.DAL.Repositories
{
    public interface IBaseEasyCreditRepository<T> 
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T entity);
        bool Exists(T entity);
        void InsertOrUpdate(T entity);
        void DeleteById(Guid id);
    }
}
