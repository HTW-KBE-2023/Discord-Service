using API.Models;
using API.Models.Surveys;
using API.Models.Validations;
using API.Utility;
using Boxed.Mapping;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Services
{
    public class SurveyService : IGenericService<Survey>
    {
        private readonly IGenericService<Survey> _genericService;
        private readonly IMapper<Survey, SurveyUpdated> _toMessageQueueMapper;
        private readonly IBus _bus;

        public SurveyService(IGenericService<Survey> genericService, SurveyMapper surveyMapper, IBus bus)
        {
            _genericService = genericService;
            _toMessageQueueMapper = surveyMapper;
            _bus = bus;
        }

        public Result<Survey, ValidationFailed> Create(Survey entity)
        {
            return _genericService.Create(entity);
        }

        public void DeleteById(object id)
        {
            _genericService.DeleteById(id);
        }

        public IEnumerable<Survey> GetAll()
        {
            return _genericService.GetAll();
        }

        public Survey? GetById(object id)
        {
            return _genericService.GetById(id);
        }

        public Result<Survey?, ValidationFailed> Update(Survey entity)
        {
            var message = _toMessageQueueMapper.Map(entity);
            _bus.Publish(message);
            return _genericService.Update(entity);
        }
    }
}