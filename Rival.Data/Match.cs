using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Data
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid CreatorId { get; set; }
        [Required]
        public MatchPlayer PlayerOne { get; set; }
        [Required]
        public MatchPlayer PlayerTwo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Court Court { get; set; }
        public MatchPlayer Winner { get; set; }
        public string FinalScore { get; set; }
    }
}
