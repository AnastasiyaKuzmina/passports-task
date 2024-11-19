namespace PassportApplication.Models.Dto
{
    /// <summary>
    /// Passport changes history DTO
    /// </summary>
    public record PassportActivityHistoryDto
    {
        /// <summary>
        /// Change date
        /// </summary>
        public DateOnly Date {  get; init; }

        /// <summary>
        /// Passport activity
        /// </summary>
        public bool Active { get; init; }
    }
}
