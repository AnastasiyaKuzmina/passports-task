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
        /// <returns></returns>
        public Task Update();
    }
}
