using PassportApplication.Models;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// Parser service interface
    /// </summary>
    public interface IParserService
    {
        /// <summary>
        /// Parses csv file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        public Task<List<Passport>> Parse(string filePath);
    }
}
