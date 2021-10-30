using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Models.PlayerModels
{
    public class PlayerListItem
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
