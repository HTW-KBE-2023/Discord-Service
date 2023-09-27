using API;
using Discord;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddDatabaseConfiguration();
builder.AddRabbitMqConfiguration();

builder.AddApplicationServices();
builder.AddMapper();
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

builder.Services.AddDiscordBot(option =>
{
    option.WithToken(TokenType.Bot, "MTE1NDQ2Njk1OTAwNjY0MjIxNg.GwUrKG.7SEN4xClTioTBQp9OSvHnfDGwjropatRioCgGw")
        .WithConfiguration(builder.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.RecreateDatabaseWithData();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();