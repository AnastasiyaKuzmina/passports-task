namespace PassportApplication.Models.Dto
{
    /// <summary>
    /// Passport changes history DTO class
    /// </summary>
    public class PassportActivityHistoryDto
    {
        /// <summary>
        /// Change date
        /// </summary>
        public DateOnly Date {  get; set; }

        /// <summary>
        /// Passport activity
        /// </summary>
        public bool Active { get; set; }
    }
}
