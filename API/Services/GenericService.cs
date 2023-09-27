using API.Models;
using API.Models.Validations;
using API.Port.Repositories;
using API.Utility;
using FluentValidation;

namespace API.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : IEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TEntity> _validator;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IValidator<TEntity> validator)
        {
            _repository = unitOfWork.GetGenericRepository<TEntity>();
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result<TEntity, ValidationFailed> Create(TEntity entity)
        {
            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return new ValidationFailed(validationResult.Errors);
            }

            _repository.Insert(entity);
            _unitOfWork.Save();

            return entity;
        }

        public Result<TEntity?, ValidationFailed> Update(TEntity entity)
        {
            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return new ValidationFailed(validationResult.Errors);
            }

            var entityExists = _repository.GetByID(entity.Id) is not null;
            if (!entityExists)
            {
                return default(TEntity?);
            }

            _repository.Update(entity);
            _unitOfWork.Save();

            return entity;
        }

        public TEntity? GetById(object id)
        {
            return _unitOfWork.GetGenericRepository<TEntity>().GetByID(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.GetGenericRepository<TEntity>().Get();
        }

        public void DeleteById(object id)
        {
            _unitOfWork.GetGenericRepository<TEntity>().Delete(id);
            _unitOfWork.Save();
        }
    }
}