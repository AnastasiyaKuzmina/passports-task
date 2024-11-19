namespace PassportApplication.Models.Dto
{
    /// <summary>
    /// Passports changes DTO
    /// </summary>
    public record PassportChangesDto
    {
        /// <summary>
        /// Passport series
        /// </summary>
        public int Series { get; init; }

        /// <summary>
        /// Passport number
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        /// Type of change: true if become active, false if become not active
        /// </summary>
        public bool ChangeType { get; init; }
    }
}
