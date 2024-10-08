namespace PassportApplication.Services.Interfaces
{
    public interface IUnpackService
    {
        public Task Unpack(string FilePath, string ExtractPath);
    }
}
