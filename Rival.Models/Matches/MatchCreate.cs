using Rival.Data;
using Rival.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Matches
{
    public class MatchCreate
    {
        public int PlayerTwoId { get; set; }
        public DateTime Date { get; set; }
        public int CourtId { get; set; }
        public string UserId { get; set; }
    }
}
