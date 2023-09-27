namespace API.Port.Database
{
    public class DbInitialiser
    {
        private readonly DiscordContext _context;

        public DbInitialiser(DiscordContext context)
        {
            _context = context;
        }

        public void Run()
        {
            RecreateDatabase();
        }

        private void RecreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}