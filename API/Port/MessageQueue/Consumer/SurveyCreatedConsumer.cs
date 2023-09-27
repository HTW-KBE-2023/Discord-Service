using API.Models.Surveys;
using API.Port.Discord;
using API.Services;
using Boxed.Mapping;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Port.MessageQueue.Consumer;

public class SurveyCreatedConsumer : IConsumer<SurveyCreated>
{
    private readonly ILogger<VoteOnSurveyChanged> _logger;
    private readonly IGenericService<Survey> _surveyService;
    private readonly IMapper<SurveyCreated, Survey> _surveyCreatedMapper;
    private readonly IDiscordBot _discordBot;

    public SurveyCreatedConsumer(ILogger<VoteOnSurveyChanged> logger,
                                       IGenericService<Survey> surveyService,
                                       SurveyMapper surveyMapper,
                                       IDiscordBot discordBot)
    {
        _logger = logger;
        _surveyService = surveyService;
        _surveyCreatedMapper = surveyMapper;
        _discordBot = discordBot;
    }

    public Task Consume(ConsumeContext<SurveyCreated> context)
    {
        var message = context.Message;

        var survey = _surveyCreatedMapper.Map(message);
        if (survey is null)
        {
            return Task.CompletedTask;
        }

        _discordBot.AddSurvey(survey);
        _surveyService.Create(survey);

        return Task.CompletedTask;
    }
}