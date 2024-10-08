namespace PassportApplication.Services.Interfaces
{
    public interface IParserService<T> where T : class
    {
        public T? Parse(string input);
    }
}
