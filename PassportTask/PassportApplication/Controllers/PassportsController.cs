using Microsoft.AspNetCore.Mvc;

namespace PassportApplication.Controllers
{
    /// <summary>
    /// Passports API controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassportsController : ControllerBase
    {
        private readonly ILogger <PassportsController> _logger;

        public PassportsController(ILogger<PassportsController> logger)
        {
            _logger = logger;
        }


    }
}
