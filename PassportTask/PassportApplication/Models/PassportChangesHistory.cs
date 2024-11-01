namespace PassportApplication.Models
{
    /// <summary>
    /// Passport changes history model
    /// </summary>
    public class PassportChangesHistory
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Passport series
        /// </summary>
        public int Series { get; set; }
        /// <summary>
        /// Passport number
        /// </summary>
        public int Number { get; set; }
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
