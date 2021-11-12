using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Data
{
    public class JoinMatchPlayer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Player))]
        public int Player { get; set; }
        [ForeignKey(nameof(Match))]
        public int Match { get; set; }
    }
}
