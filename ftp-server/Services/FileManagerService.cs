using ftp_server.Models;
using ftp_server.Services.Abstracts;

namespace ftp_server.Services
{
    public class FileManagerService : IFileManagerService
    {
        private readonly ILogger<FileManagerService> _logger;
        private readonly IConfiguration _config;
        private readonly string rootFolder;
        private readonly IWebHostEnvironment _env;

        public FileManagerService(ILogger<FileManagerService> logger, IConfiguration config, IWebHostEnvironment env)
        {
            _logger = logger;
            _config = config;
            _env = env;
            rootFolder = Path.Combine(_env.ContentRootPath, _config["RootFolder"]);
        }

        public IEnumerable<ReadFileModel> GetFiles()
        {
            var files = Directory.GetFiles(rootFolder, "*.*" , SearchOption.AllDirectories);
            
            if (!files.Any())
            {
                throw new ArgumentNullException("No files");
            }

            var fileModels = new List<ReadFileModel>();

            files.ToList().ForEach(f => fileModels.Add(ParseFilePath(f)));

            return fileModels;
        }

        public async Task<bool> UploadFile(FileModel fileModel)
        {
            if (fileModel.FileName is null || fileModel.FormFile is null)
            {
                throw new ArgumentException("No data provided");
            }

            string path = Path.Combine(rootFolder, fileModel.FileName);

            using Stream stream = new FileStream(path, FileMode.Create);
            await fileModel.FormFile.CopyToAsync(stream);

            return true;
        }

        private ReadFileModel ParseFilePath(string path)
        {
            var fileInfo = new FileInfo(path);
            
            return new ReadFileModel 
            {
                Id = Guid.NewGuid(),
                FileName = fileInfo.Name,
            };
        }
    }
}