using BuildYourself.Domain.Enities;
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
        public FileCategory FileCategory { get; set; }
    }
}
