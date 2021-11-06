using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Data
{
    public class MatchPlayer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public DateTime DateJoined { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public State State { get; set; }
        public string Location { get { return City + " " + State; } }
        [Required]
        public PlayerAvailability Availability { get; set; }
        public PreferredSetNumber PreferredSetNumber { get; set; } = PreferredSetNumber.NoPreference;
        //public MatchDetail LastMatchPlayed { get; set; }
        //public List<MatchDetail> MatchesPlayed { get; set; }
        //public string Record { get; set; }
        public int? SportsmanshipRating { get; set; }
    }
}
