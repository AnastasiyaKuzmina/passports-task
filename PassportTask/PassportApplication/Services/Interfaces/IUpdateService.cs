using PassportApplication.Results;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// Update service interface
    /// </summary>
    public interface IUpdateService
    {
        /// <summary>
        /// Updates database
        /// </summary>
        /// <returns>Result instance</returns>
        public Task<Result> UpdateAsync();
    }
}
