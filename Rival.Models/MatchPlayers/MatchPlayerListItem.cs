using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.MatchPlayers
{
    public class MatchPlayerListItem
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string City { get; set; }
        public State State { get; set; }
        public string Location { get { return City + ", " + State; } }
        public DateTime DateJoined { get; set; }
        public int? SportsmanshipRating { get; set; }
    }
}
