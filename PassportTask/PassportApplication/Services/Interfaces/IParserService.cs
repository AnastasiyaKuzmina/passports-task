using PassportApplication.Models;

namespace PassportApplication.Services.Interfaces
{
    public interface IParserService
    {
        public Task<List<Passport>> Parse(string filePath);
    }
}
