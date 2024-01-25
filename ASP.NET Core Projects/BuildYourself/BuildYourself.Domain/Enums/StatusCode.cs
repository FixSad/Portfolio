using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourself.Domain.Enums
{
    public enum StatusCode
    {
        CategoryIsHadAlready = 1,
        ItemIsHadAlready = 2,

        Success = 200,
        InternalServerError = 500,
    }
}
