using API.Models;
using API.Models.DiscordUsers;
using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Port.Database;
using Models.Fights;

namespace API.Port.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed = false;

        private readonly DiscordContext _context;

        private readonly ICollection<object> _genericRepositories = new List<object>();

        private IGenericRepository<Survey>? _surveyRepository;
        private IGenericRepository<SurveyOption> _surveyOptionRepository;

        public UnitOfWork(DiscordContext context)
        {
            _context = context;

            _genericRepositories.Add(new GenericRepository<Survey>(_context));
            _genericRepositories.Add(new GenericRepository<SurveyOption>(_context));
            _genericRepositories.Add(new GenericRepository<DiscordUser>(_context));
            _genericRepositories.Add(new GenericRepository<Fight>(_context));
        }

        public IGenericRepository<Survey> SurveyRepository
        {
            get
            {
                _surveyRepository ??= GetGenericRepository<Survey>();
                return _surveyRepository;
            }
        }

        public IGenericRepository<SurveyOption> SurveyOptionRepository
        {
            get
            {
                _surveyOptionRepository ??= GetGenericRepository<SurveyOption>();
                return _surveyOptionRepository;
            }
        }

        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : IEntity
        {
            var genericRepository = _genericRepositories.OfType<IGenericRepository<TEntity>>().SingleOrDefault();
            if (genericRepository is null)
            {
                throw new ArgumentException($"No Repository for the Type {typeof(TEntity).Name} could be found!");
            }

            return genericRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}