using BuildYourself.Domain.Enums;

namespace BuildYourself.Domain.ViewModel
{
    public class FileViewModel
    {
        public string FileName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FileDescription { get; set; }
        public string FileCategory { get; set; }
        public FileStatus FileStatus { get; set; }
    }
}