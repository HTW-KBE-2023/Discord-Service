using API.Models;

namespace API.Port.Repositories
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : IEntity;

        public void Save();
    }
}