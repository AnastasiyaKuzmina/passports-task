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
        public short Series { get; set; }
        /// <summary>
        /// Passport number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Passport activity
        /// </summary>
        public bool Active { get; set; }

    }
}
