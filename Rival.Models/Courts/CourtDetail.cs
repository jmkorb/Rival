using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Courts
{
    public class CourtDetail
    {
        public int CourtId { get; set; }
        public string Location { get; set; }
        public Condition Condition { get; set; }
    }
}
