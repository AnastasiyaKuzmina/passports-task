namespace PassportApplication.Models
{
    /// <summary>
    /// Passport model
    /// </summary>
    public class Passport
    {
        /// <summary>
        /// Passport Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Passport series
        /// </summary>
        public string Series { get; set; } = null!;
        /// <summary>
        /// Passport number
        /// </summary>
        public string Number { get; set; } = null!;

    }
}
