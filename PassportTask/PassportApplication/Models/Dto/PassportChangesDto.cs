namespace PassportApplication.Models.Dto
{
    /// <summary>
    /// Passports changes DTO class
    /// </summary>
    public class PassportChangesDto
    {
        /// <summary>
        /// Passport series
        /// </summary>
        public int Series { get; set; }

        /// <summary>
        /// Passport number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Type of change: true if become active, false if become not active
        /// </summary>
        public bool ChangeType { get; set; }
    }
}
