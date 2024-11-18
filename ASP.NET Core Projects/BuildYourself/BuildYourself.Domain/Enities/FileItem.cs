using BuildYourself.Domain.Enums;

namespace BuildYourself.Domain.Enities
{
    public class FileItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public FileCategory Category { get; set; }
        public FileStatus Status { get; set; }
    }
}
