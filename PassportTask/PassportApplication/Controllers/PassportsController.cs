using Microsoft.AspNetCore.Mvc;
using PassportApplication.Models.Dto;
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
        [Route("GetPassportActivity")]
        public IActionResult GetPassportActivity(string series, string number)
        {
            PassportDto? result = _repository.GetPassportActivity(series, number);

            if (result == null)
            {
                return NotFound();
            }

            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("GetPassportsChangesForDate")]
        public IActionResult GetPassportsChangesForDate(short day, short month, short year)
        {
            List<PassportChangesDto>? result = _repository.GetPassportsChangesForDate(day, month, year);

            if (result == null)
            {
                return NotFound();
            }

            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("GetPassportHistory")]
        public IActionResult GetPassportHistory(string series, string number)
        {
            List<PassportActivityHistoryDto>? result = _repository.GetPassportHistory(series, number);

            if (result == null)
            {
                return NotFound();
            }

            return new OkObjectResult(result);
        }
    }
}
