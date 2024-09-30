namespace PassportApplication.Models
{
    /// <summary>
    /// Passport changes history model
    /// </summary>
    public class PassportChangesHistory
    {
        /// <summary>
        /// Passport series
        /// </summary>
        public string Series { get; set; } = null!;
        /// <summary>
        /// Passport number
        /// </summary>
        public string Number { get; set; } = null!;
        /// <summary>
        /// Type of change: true if add, false if remove
        /// </summary>
        public bool ChangeType { get; set; }
        /// <summary>
        /// Date of change
        /// </summary>
        public DateOnly Date { get; set; }
    }
}
