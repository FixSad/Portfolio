using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildYourself.Domain.Enities
{
    public class FileCategory : Category
    {
        public bool IsEntertainment { get; set; }
    }
}
