using PassportApplication.Models;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// Database management interface
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Updates the database
        /// </summary>
        /// <returns></returns>
        public Task UpdateAsync();
    }
}
