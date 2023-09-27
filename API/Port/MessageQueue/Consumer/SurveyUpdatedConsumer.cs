using API.Models.Surveys;
using API.Port.Discord;
using API.Services;
using Boxed.Mapping;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Port.MessageQueue.Consumer;

public class SurveyUpdatedConsumer : IConsumer<SurveyUpdated>
{
    private readonly ILogger<SurveyUpdatedConsumer> _logger;
    private readonly IGenericService<Survey> _surveyService;
    private readonly IMapper<SurveyUpdated, Survey> _surveyUpdatedMapper;
    private readonly IDiscordBot _discordBot;

    public SurveyUpdatedConsumer(ILogger<SurveyUpdatedConsumer> logger,
                                       IGenericService<Survey> surveyService,
                                       SurveyMapper surveyMapper,
                                       IDiscordBot discordBot)
    {
        _logger = logger;
        _surveyService = surveyService;
        _surveyUpdatedMapper = surveyMapper;
        _discordBot = discordBot;
    }

    public Task Consume(ConsumeContext<SurveyUpdated> context)
    {
        var message = context.Message;

        var survey = _surveyUpdatedMapper.Map(message);
        if (survey is null)
        {
            return Task.CompletedTask;
        }

        _discordBot.ModifySurvey(survey);
        _surveyService.Create(survey);

        return Task.CompletedTask;
    }
}