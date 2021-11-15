﻿using Rival.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Matches
{
    public class MatchEdit
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }
        public int CourtId { get; set; }
        public Court Court { get; set; }
        public string Winner { get; set; }
        public string FinalScore { get; set; }
        public string UserId { get; set; }
    }
}
