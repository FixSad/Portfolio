using BuildYourself.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourself.Domain.Enities
{
    public class GymCategory : Category
    {
        public List<MuscleGroups> MuscleGroups { get; set; }
    }
}
