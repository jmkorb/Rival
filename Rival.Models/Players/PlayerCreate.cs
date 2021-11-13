using Rival.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.Players
{
    public class PlayerCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "1 or more characters required.")]
        [MaxLength(50, ErrorMessage = "Too many characters. 50 or less.")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "1 or more characters required.")]
        [MaxLength(50, ErrorMessage = "Too many characters. 50 or less.")]
        public string LastName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Your location needs to have 2 or more characters")]
        [MaxLength(50, ErrorMessage = "Your city cannot be longer than 50 characters")]
        public string City { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public PlayerAvailability Availability { get; set; }
        public PreferredSetNumber PreferredSetNumber { get; set; }
        public string UserId { get; set; }
    }
}
