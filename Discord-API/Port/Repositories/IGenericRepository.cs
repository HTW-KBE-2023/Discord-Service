using API.Models;
using System.Linq.Expressions;

namespace API.Port.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        TEntity? GetByID(object id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       string includeProperties = "");

        void Delete(object id);

        void Delete(TEntity? entityToDelete);
    }
}