using PassportApplication.Options.FormatOptions;
using PassportApplication.Results;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// Database management interface
    /// </summary>
    public interface ICopyService
    {
        /// <summary>
        /// Copies from csv to database
        /// </summary>
        /// <returns>Result instance</returns>
        public Task<Result> CopyAsync(string filePath, FormatSettings formatSettings, CancellationToken cancellationToken);
    }
}
