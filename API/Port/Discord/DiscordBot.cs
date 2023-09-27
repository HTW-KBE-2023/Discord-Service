using Discord.WebSocket;
using Discord;
using Discord.Net;
using Newtonsoft.Json;
using API.Port.Discord.Options;
using API.Models.Surveys;
using API.Services;
using API.Port.Discord.Commands;
using Models.Fights;
using System.Threading.Channels;

namespace API.Port.Discord
{
    public class DiscordBot : IDiscordBot
    {
        private readonly ILogger<DiscordBot> _logger;
        private readonly DiscordBotOptions _options;

        private readonly DiscordSocketClient _client;

        private readonly IDiscordCommandRegister _commandRegister;

        public DiscordBot(ILogger<DiscordBot> logger, DiscordBotOptions options, IDiscordCommandRegister commandRegister)
        {
            _logger = logger;
            _options = options;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 50,
            });

            _commandRegister = commandRegister;

            RegisterLogging();

            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;
            _client.SelectMenuExecuted += MyMenuHandler;
        }

        private void RegisterLogging()
        {
            _client.Log += Log;
        }

        private Task Log(LogMessage message)
        {
            _logger.LogInformation("{time} [{Severity}] {Source}: {Message} {Exception}", DateTime.Now, message.Severity, message.Source, message.Message, message.Exception);

            return Task.CompletedTask;
        }

        public async Task LoginAsync() => await _client.LoginAsync(_options.TokenType, _options.Token);

        public async Task LogoutAsync() => await _client.LogoutAsync();

        public async Task StartAsync() => await _client.StartAsync();

        public async Task StopAsync() => await _client.StopAsync();

        private async Task Client_Ready()
        {
            var guild = _client.GetGuild(1104714955468574805);

            var guildCommand = new SlashCommandBuilder()
                .WithName("RequestRpgFight")
                .WithDescription("try triggering a fight!");

            try
            {
                await guild.BulkOverwriteApplicationCommandAsync(new[] { guildCommand.Build() });
            }
            catch (HttpException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

                _logger.LogInformation(json);
            }
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            var discordCommand = _commandRegister.SlashCommands.FirstOrDefault(discordCommand => discordCommand.Name == command.Data.Name, _commandRegister.DefaultSlashCommand);
            await discordCommand.Execute(command);
        }

        private async Task MyMenuHandler(SocketMessageComponent arg)
        {
            var text = string.Join(", ", arg.Data.Values);
            await arg.RespondAsync($"You have selected {text}");
        }

        public async Task AddSurvey(Survey survey)
        {
            var channel = _client.GetGuild(1104714955468574805).GetTextChannel(1156253208574955610);
            var (embedBuiler, componentBuilder) = SurveyToDiscordMessageComponents(survey);

            var message = await channel.SendMessageAsync(embed: embedBuiler.Build(), components: componentBuilder.Build());
            survey.ConnectedMessageId = message.Id;
        }

        public async Task ModifySurvey(Survey survey)
        {
            var channel = _client.GetGuild(1104714955468574805).GetTextChannel(1156253208574955610);
            if (survey.ConnectedMessageId is null)
            {
                _logger.LogError("Trying to Modify a Survey in Discord without a connected message Id! SurveyId: {Id}", survey.Id);
                return;
            }
            var originalMessage = await channel.GetMessageAsync(survey.ConnectedMessageId.Value);
            await originalMessage.DeleteAsync();

            var (embedBuiler, componentBuilder) = SurveyToDiscordMessageComponents(survey);
            var modifiedMessage = await channel.SendMessageAsync(embed: embedBuiler.Build(), components: componentBuilder.Build());
            survey.ConnectedMessageId = modifiedMessage.Id;
        }

        private (EmbedBuilder embedBuiler, ComponentBuilder componentBuilder) SurveyToDiscordMessageComponents(Survey survey)
        {
            var embedBuiler = new EmbedBuilder()
                .WithAuthor("")
                .WithTitle($"Survey: {survey.Title}")
                .WithDescription(survey.Description)
                .WithColor(Color.Green)
                .WithCurrentTimestamp();
            var menuBuilder = new SelectMenuBuilder()
                .WithPlaceholder("Select an option")
                .WithCustomId(survey.Id.ToString())
                .WithMinValues(1)
                .WithMaxValues(1);

            foreach (var option in survey.SurveyOptions.OrderBy(option => option.Position))
            {
                menuBuilder.AddOption(option.Text, option.Id.ToString());
            }

            var componentBuilder = new ComponentBuilder().WithSelectMenu(menuBuilder);

            return (embedBuiler, componentBuilder);
        }

        public async Task ReplyToUserForConcludedFight(Fight fight)
        {
            var discordUserId = fight.Player.DiscordUser?.DiscordId;
            if (discordUserId is null)
            {
                return;
            }

            var discordUser = _client.GetGuild(1104714955468574805).GetTextChannel(1156253208574955610).GetUser(discordUserId.Value);
            var channel = await discordUser.CreateDMChannelAsync();

            var embedBuiler = new EmbedBuilder()
                .WithAuthor("Bot")
                .WithTitle($"Fight Concluded!")
                .WithDescription(string.Join(Environment.NewLine, fight.Summary))
                .WithColor(Color.Blue)
                .WithCurrentTimestamp();

            await channel.SendMessageAsync(embed: embedBuiler.Build());

            await channel.CloseAsync();
        }
    }
}