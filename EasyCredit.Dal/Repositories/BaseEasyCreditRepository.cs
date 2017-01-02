using AutoMapper;
using EasyCredit.DAL.Context;
using EasyCredit.DAL.EasyCreditAssertionGroup;
using EasyCredit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EasyCredit.DAL.Repositories
{
    public abstract class BaseEasyCreditRepository<T>: IBaseEasyCreditRepository<T> where T : class, IEasyCreditEntity
    {
        private readonly MainContext context;
        private IDbSet<T> entities;
        public BaseEasyCreditRepository(MainContext context)
        {
            this.context = context;
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }

        public IEnumerable<T> GetAll() 
        {
            return Entities;
        }

        public T Get(Guid id)
        {
            return Entities.Find(id);
        }

        public void Create(T entity)
        {
            GeneralAssertionGroup<T>.ValueCanNotBeNull(entity);
            Entities.Add(entity);
        }        

        public bool Exists(T entity)
        {
            return Entities.Any(item => item.Id == entity.Id);
        }

        public void InsertOrUpdate(T entity)
        {
            GeneralAssertionGroup<T>.ValueCanNotBeNull(entity);
            if (Exists(entity))
            {
                T item = Get(entity.Id);
                Mapper.Map(entity, item);
            }

            else Create(entity);
        }

        public void DeleteById(Guid id)
        {
            T entity = Entities.Find(id);
            if (entity != null)
                Entities.Remove(entity);
        }
    }
}
