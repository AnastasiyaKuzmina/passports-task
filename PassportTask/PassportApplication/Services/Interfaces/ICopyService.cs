using PassportApplication.Results;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// Database management interface
    /// </summary>
    public interface ICopyService
    {
        /// <summary>
        /// Updates the database
        /// </summary>
        /// <returns></returns>
        public Task<Result> CopyAsync(string filePath);
    }
}
