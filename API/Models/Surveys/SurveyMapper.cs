using API.Models.SurveyOptions;
using Boxed.Mapping;
using MessagingContracts.Survey;

namespace API.Models.Surveys;

public class SurveyMapper :
    IMapper<SurveyCreated, Survey>,
    IMapper<SurveyUpdated, Survey>,
    IMapper<Survey, SurveyUpdated>
{
    private readonly IMapper<SurveyOption, Option> _toMessageQueueMapper = new SurveyOptionMapper();
    private readonly IMapper<Option, SurveyOption> _fromMessageQueueMapper = new SurveyOptionMapper();

    public void Map(SurveyCreated source, Survey destination)
    {
        destination.Id = source.SurveyId;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.SurveyOptions = _fromMessageQueueMapper.MapList(source.SurveyOptions);
    }

    public void Map(SurveyUpdated source, Survey destination)
    {
        destination.Id = source.SurveyId;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.Completed = source.Completed;
        destination.SurveyOptions = _fromMessageQueueMapper.MapList(source.SurveyOptions);
    }

    public void Map(Survey source, SurveyUpdated destination)
    {
        throw new NotImplementedException();
    }
}