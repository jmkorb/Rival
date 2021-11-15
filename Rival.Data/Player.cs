﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Data
{
    public enum PlayerAvailability
    {
        Morning = 0,
        Lunch = 1,
        Afternoon = 2,
        Evening = 3,
        Weekends = 4,
        Anytime = 5
    }
    public enum PreferredSetNumber
    {
        NoPreference,
        [Description("1")]
        One,
        [Description("3")]
        Three,
        [Description("5")]
        Five
    }
    public enum State
    {
        AK, AL, AR, AS, AZ, CA, CO, CT, DC, DE, FL, 
        GA, GU, HI, IA, ID, IL, IN, KS, KY, LA, MA, 
        MD, ME, MI, MN, MO, MP, MS, MT, NC, ND, NE, 
        NH, NJ, NM, NV, NY, OH, OK, OR, PA, PR, RI, 
        SC, SD, TN, TX, UM, UT, VA, VI, VT, WA, WI, 
        WV, WY
    }
    public class Player
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
        public string Location { get { return City + ", " + State; } }
        [Required]
        public PlayerAvailability Availability { get; set; }
        public PreferredSetNumber PreferredSetNumber { get; set; } = PreferredSetNumber.NoPreference;
        //public MatchDetail LastMatchPlayed { get; set; }
        public virtual ICollection<Match> MatchesPlayed { get; set; }
        //public string Record { get; set; }
        public int? SportsmanshipRating { get; set; }

        public Player()
        {
            MatchesPlayed = new HashSet<Match>();
        }
    }
}
