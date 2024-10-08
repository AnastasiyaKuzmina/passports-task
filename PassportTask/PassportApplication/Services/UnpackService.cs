using PassportApplication.Services.Interfaces;
using System.IO.Compression;

namespace PassportApplication.Services
{
    public class UnpackService : IUnpackService
    {
        public async Task Unpack(string FilePath, string ExtractPath)
        {
            await Task.Run(() => ZipFile.ExtractToDirectory(FilePath, ExtractPath));
        }
    }
}
