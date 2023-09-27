using API.Models.DiscordUsers;
using FluentValidation;

namespace API.Services.DiscordUsers;

public class DiscordUsersValidator : AbstractValidator<DiscordUser>
{
    public DiscordUsersValidator()
    {
        RuleFor(user => user.Id).NotEmpty();
        RuleFor(user => user.DiscordId).NotEmpty();
    }
}