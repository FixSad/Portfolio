using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourself.Domain.Enities
{
    public class GymTask
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public DateOnly TaskDate { get; set; }
        public GymCategory Category { get; set; }
    }
}
