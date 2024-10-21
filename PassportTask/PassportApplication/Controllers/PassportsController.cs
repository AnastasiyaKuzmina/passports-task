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
        public IActionResult GetPassport(int id)
        {
            Passport? passport = _applicationContext.Passports.Find(id);

            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }
    }
}
