using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Players
{
    public class PlayerDetail
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public DateTime DateJoined { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public PlayerAvailability Availability { get; set; }
        public PreferredSetNumber PreferredSetNumber { get; set; } 
        //public MatchDetail LastMatchPlayed { get; set; }
        //public List<MatchDetail> MatchesPlayed { get; set; }
        //public string Record { get; set; }
        public int? SportsmanshipRating { get; set; }
    }
}
