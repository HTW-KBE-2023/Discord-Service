using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscordController : ControllerBase
    {
        private readonly ILogger<DiscordController> _logger;

        public DiscordController(ILogger<DiscordController> logger)
        {
            _logger = logger;
        }
    }
}