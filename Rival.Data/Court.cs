using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Data
{
    public enum Condition
    {
        One,
        Two,
        Three,
        Four,
        Five
    }
    public class Court
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public Condition Condition { get; set; }
    }
}
