using System.Collections.Generic;

namespace Forum.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void Delete(TEntity entity);
        bool SaveChanges();
    }   
}