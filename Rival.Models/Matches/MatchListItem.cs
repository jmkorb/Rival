using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Matches
{
    public class MatchListItem
    {
        public int MatchId { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public DateTime Date { get; set; }
    }
}
