using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Players
{
    public class PlayerEdit
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public PreferredSetNumber PreferredSetNumber { get; set; }
        public PlayerAvailability Availability { get; set; }

    }
}
