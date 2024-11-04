using Mapster;
using Microsoft.AspNetCore.Mvc;

using PassportApplication.Database;
using PassportApplication.Models;

namespace PassportApplication.Controllers
{
    /// <summary>
    /// Passports API controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassportsController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public PassportsController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Gets passport's activity
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPassportActivity(string series, string number)
        {
            Passport? passport = _applicationContext.Passports.Find(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport.Adapt<PassportDto>());
        }
    }
}
