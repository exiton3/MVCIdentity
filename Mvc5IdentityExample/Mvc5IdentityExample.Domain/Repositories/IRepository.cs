using System.Collections.Generic;

namespace Mvc5IdentityExample.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        List<TEntity> PageAll(int skip, int take);

        TEntity FindById(object id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
