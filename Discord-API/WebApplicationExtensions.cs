using API.Port.Database;

namespace API
{
    public static class WebApplicationExtensions
    {
        public static void RecreateDatabaseWithData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var initialiser = services.GetRequiredService<DbInitialiser>();

            initialiser.Run();
        }
    }
}