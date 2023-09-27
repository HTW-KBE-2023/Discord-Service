using API.Models.DiscordUsers;
using API.Models.Surveys;
using Models.Fights;

namespace API.Port.Discord
{
    public interface IDiscordBot
    {
        Task LoginAsync();

        Task LogoutAsync();

        Task StartAsync();

        Task StopAsync();

        Task AddSurvey(Survey survey);

        Task ModifySurvey(Survey survey);

        Task ReplyToUserForConcludedFight(Fight fight, DiscordUser user);
    }
}