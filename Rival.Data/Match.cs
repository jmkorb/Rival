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
        public virtual List<Player> SetOfPlayers { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Court))]
        public int CourtId { get; set; }
        public virtual Court Court { get; set; }
        public string Winner { get; set; }
        public string FinalScore { get; set; }
    }
}
