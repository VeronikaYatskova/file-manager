using ftp_server.Models;
using ftp_server.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ftp_server.Controllers
{
    [ApiController]
    [Route("api/file-manager")]
    public class FileManagerController : ControllerBase
    {
        private readonly ILogger<FileManagerController> _logger;
        private readonly IFileManagerService _fileManagerService;

        public FileManagerController(ILogger<FileManagerController> logger, IFileManagerService fileManagerService)
        {
            _logger = logger;
            _fileManagerService = fileManagerService;
        }

        [HttpGet("files")]
        public IEnumerable<ReadFileModel> GetFiles()
        {
            var files = _fileManagerService.GetFiles();

            return files;
        }

        [HttpPost("files")]
        public async Task<IActionResult> UploadFile([FromForm]FileModel? fileModel)
        {
            var res = await _fileManagerService.UploadFile(fileModel);

            return res ? Created("File uploaded", fileModel) : BadRequest();
        }
    }
}
