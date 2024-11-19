namespace PassportApplication.Models.Dto
{
    /// <summary>
    /// Passport DTO
    /// </summary>
    public record PassportDto
    {
        /// <summary>
        /// Passport activity
        /// </summary>
        public bool Active { get; init; }
    }
}
