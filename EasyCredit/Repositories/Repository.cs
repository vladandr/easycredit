using System;
using System.Data.Entity;
using System.Linq;
using EasyCredit.EasyCreditAssertionGroup;
using EasyCredit.Models;

namespace EasyCredit.Repositories
{
    public class Repository<T> : IRepository<T> 
        where T : class, IEasyCreditEntity
    {
        private readonly IDbSet<T> _entities;
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            context.ThrowIfArgumentIsNull(nameof(context));

            _entities = context.Set<T>();
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public T Get(Guid id)
        {
            return _entities.Find(id);
        }

        public void Create(T entity)
        {
            entity.ThrowIfArgumentIsNull(nameof(entity));

            _entities.Add(entity);
        }

        public bool Exists(T entity)
        {
            return _entities.Any(item => item.Id == entity.Id);
        }

        public void InsertOrUpdate(T entity)
        {
            entity.ThrowIfArgumentIsNull(nameof(entity));

            if (Exists(entity))
            {
                _entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                Create(entity);
            }
        }

        public void DeleteById(Guid id)
        {
            T entity = Get(id);

            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }
    }
}
