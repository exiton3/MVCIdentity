using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mvc5IdentityExample.Domain.Repositories;

namespace Mvc5IdentityExample.Data.EntityFramework.Repositories
{

    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> GetExpression();
    }

    class MultiSpecification<T> : ISpecification<T> where T : class
    {
        string value;

        public string Property { get; set; }
        public MultiSpecification(string value)
        {
            this.value = value;
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            return x => Property.Contains(value);
        }
    }
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context;
        private DbSet<TEntity> _set;

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public List<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public List<TEntity> PageAll(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToList();
        }

        public TEntity FindById(object id)
        {
            return Set.Find(id);
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}