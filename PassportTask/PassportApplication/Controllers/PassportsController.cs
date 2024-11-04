using Microsoft.AspNetCore.Mvc;

using PassportApplication.Models;
using PassportApplication.Repositories.Interfaces;

namespace PassportApplication.Controllers
{
    /// <summary>
    /// Passports API controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassportsController : ControllerBase
    {
        private readonly IRepository _repository;

        public PassportsController(IRepository repository)
        {
            _repository = repository;
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
            PassportDto? passportDto = _repository.GetPassportActivity(series, number);

            if (passportDto == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passportDto);
        }
    }
}
