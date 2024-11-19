using PassportApplication.Results;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// Unpack service interface
    /// </summary>
    public interface IUnpackService
    {
        /// <summary>
        /// Unpacks the file
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <param name="ExtractPath">Extract path</param>
        /// <returns>Result instance</returns>
        public Result Unpack();
    }
}
