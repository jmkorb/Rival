using Rival.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Courts
{
    public class CourtCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage ="Your location cannot be longer than 50 characters")]
        public string Location { get; set; }
        public Condition Condition { get; set; }
    }
}
