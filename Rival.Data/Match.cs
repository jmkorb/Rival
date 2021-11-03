using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Data
{
    public class MatchPlayer
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string City { get; set; }
        public State State { get; set; }
        public string Location { get { return City + ", " + State; } }
    }
    public class Match
    {
        public int Id { get; set; }
        public MatchPlayer PlayerOne { get; set; }
        public MatchPlayer PlayerTwo { get; set; }
        public DateTime Date { get; set; }
        public Court Court { get; set; }
        public string FinalScore { get; set; }
    }
}
