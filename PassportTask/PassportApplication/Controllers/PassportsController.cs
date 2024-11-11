using Microsoft.AspNetCore.Mvc;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;
using PassportApplication.Models.Dto;
using PassportApplication.Results;
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
        public async Task<ActionResult<PassportDto>> GetPassportActivity(string? series, string? number)
        {
            if ((series == null) || (number == null)) 
            {
                Result<PassportDto> res = new Result<PassportDto>(new Error(ErrorType.ControllerNullArgument, "There is no passport series or number"));
                return res.ToActionResult();
            }
            var result = await _repository.GetPassportActivityAsync(series, number);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("GetPassportHistory")]
        public async Task<ActionResult<List<PassportActivityHistoryDto>>> GetPassportHistory(string? series, string? number)
        {
            if ((series == null) || (number == null))
            {
                Result<List<PassportActivityHistoryDto>> res = new Result<List<PassportActivityHistoryDto>>(new Error(ErrorType.ControllerNullArgument, "There is no passport series or number"));
                return res.ToActionResult();
            }
            var result = await _repository.GetPassportHistoryAsync(series, number);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("GetPassportsChangesForDate")]
        public async Task<ActionResult<List<PassportChangesDto>>> GetPassportsChangesForDate(short? day, short? month, short? year)
        {
            if ((day == null) || (month == null) || (year == null))
            {
                Result<List<PassportChangesDto>> res = new Result<List<PassportChangesDto>>(new Error(ErrorType.ControllerNullArgument, "There is no day or month or year"));
                return res.ToActionResult();
            }
            var result = await _repository.GetPassportsChangesForDateAsync((short)day, (short)month, (short)year);
            return result.ToActionResult();
        }
    }
}
