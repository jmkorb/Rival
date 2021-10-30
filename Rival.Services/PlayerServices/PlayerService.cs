using Rival.Data;
using Rival.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Services.PlayerServices
{
    public class PlayerService
    {
        private readonly Guid _userId;

        public PlayerService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var entity = new Player()
            {
                UserId = _userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = model.State,
                DateJoined = DateTime.Now,
                Availability = model.Availability,
                PreferredSetNumber = model.PreferredSetNumber
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
