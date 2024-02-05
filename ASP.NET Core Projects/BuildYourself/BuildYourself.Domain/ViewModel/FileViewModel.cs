using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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