using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Matches
{
    public class MatchCreate
    {
        public IEnumerable<MatchPlayer> PlayerOne { get; set; }
        public IEnumerable<MatchPlayer> PlayerTwo { get; set; }
        public DateTime Date { get; set; }
        public Court Court { get; set; }
        public string UserId { get; set; }
    }
}
