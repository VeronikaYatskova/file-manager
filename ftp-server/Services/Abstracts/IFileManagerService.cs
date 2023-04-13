using ftp_server.Models;

namespace ftp_server.Services.Abstracts
{
    public interface IFileManagerService
    {
        public IEnumerable<ReadFileModel> GetFiles();
        public Task<bool> UploadFile(FileModel fileModel);
    }
}
