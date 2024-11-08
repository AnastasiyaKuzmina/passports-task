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
        public async Task<ActionResult<PassportDto>> GetPassportActivity(string series, string number)
        {
            var result = await _repository.GetPassportActivityAsync(series, number);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("GetPassportHistory")]
        public async Task<ActionResult<List<PassportActivityHistoryDto>>> GetPassportHistory(string series, string number)
        {
            var result = await _repository.GetPassportHistoryAsync(series, number);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("GetPassportsChangesForDate")]
        public async Task<ActionResult<List<PassportChangesDto>>> GetPassportsChangesForDate(short day, short month, short year)
        {
            var result = await _repository.GetPassportsChangesForDateAsync(day, month, year);
            return result.ToActionResult();
        }
    }
}
