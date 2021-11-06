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
        public MatchPlayer PlayerOne { get; set; }
        public MatchPlayer PlayerTwo { get; set; }
        public DateTime Date { get; set; }
        public Court Court { get; set; }
    }
}
