namespace PassportApplication.Models
{
    /// <summary>
    /// Passport model
    /// </summary>
    public class Passport
    {
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
