using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BuildYourself.Models
{
    public class FileCategoryModel
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool CategoryIsEntertainment { get; set; }
    }
}
