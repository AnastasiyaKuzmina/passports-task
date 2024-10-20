using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
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

        [HttpGet]
        public IActionResult GetPassport(string series, string number)
        {
            Passport? passport = _applicationContext.Passports.Find(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }
    }
}
