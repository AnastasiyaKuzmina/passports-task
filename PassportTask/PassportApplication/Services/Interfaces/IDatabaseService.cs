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
        /// <param name="passports">List of passports</param>
        /// <returns></returns>
        public Task Update(List<Passport> passports);
    }
}
