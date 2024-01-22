using BuildYourself.Domain.Enities;

namespace BuildYourself.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string FileDescription { get; set; }
        public FileCategory FileCategory { get; set; }
    }
}
