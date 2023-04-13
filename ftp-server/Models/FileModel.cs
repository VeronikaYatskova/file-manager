
namespace ftp_server.Models
{
    public class FileModel
    {
        public string? FileName { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
