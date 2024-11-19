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
        /// Gets passport's current activity status
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's current activity status</returns>
        [HttpGet]
        [Route("GetPassportActivity")]
        public async Task<ActionResult<PassportDto>> GetPassportActivity(string? series, string? number, CancellationToken cancellationToken)
        {
            if ((series == null) || (number == null)) 
            {
                return new BadRequestResult();
            }
            var result = await _repository.GetPassportActivityAsync(series, number, cancellationToken);
            return result;
        }

        /// <summary>
        /// Gets passport's activity history
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's history</returns>
        [HttpGet]
        [Route("GetPassportHistory")]
        public async Task<ActionResult<List<PassportActivityHistoryDto>>> GetPassportHistory(string? series, string? number, CancellationToken cancellationToken)
        {
            if ((series == null) || (number == null))
            {
                return new BadRequestResult();
            }
            var result = await _repository.GetPassportHistoryAsync(series, number, cancellationToken);
            return result;
        }

        /// <summary>
        /// Gets passports' changes for date
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns>Passports' changes for date</returns>
        [HttpGet]
        [Route("GetPassportsChangesForDate")]
        public async Task<ActionResult<List<PassportChangesDto>>> GetPassportsChangesForDate(short? day, short? month, short? year, CancellationToken cancellationToken)
        {
            if ((day == null) || (month == null) || (year == null))
            {
                return new BadRequestResult();
            }
            var result = await _repository.GetPassportsChangesForDateAsync((short)day, (short)month, (short)year, cancellationToken);
            return result;
        }
    }
}
