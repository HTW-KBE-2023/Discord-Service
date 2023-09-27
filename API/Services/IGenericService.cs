using API.Models;
using API.Models.Validations;
using API.Utility;

namespace API.Services
{
    public interface IGenericService<TEntity> where TEntity : IEntity
    {
        Result<TEntity, ValidationFailed> Create(TEntity entity);

        Result<TEntity?, ValidationFailed> Update(TEntity entity);

        TEntity? GetById(object id);

        IEnumerable<TEntity> GetAll();

        void DeleteById(object id);
    }
}