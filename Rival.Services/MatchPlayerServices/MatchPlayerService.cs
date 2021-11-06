using Rival.Data;
using Rival.Models.MatchPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Services.MatchPlayerServices
{
    public class MatchPlayerService
    {
        private readonly Guid _userId;

        public MatchPlayerService(Guid userId)
        {
            _userId = userId;
        }

        public bool AddPlayerToList(int playerId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Players.Single(e => e.Id == playerId);
                var matchPlayer = new MatchPlayer()
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    City = entity.City,
                    State = entity.State,
                    DateJoined = entity.DateJoined,
                    Availability = entity.Availability,
                    PreferredSetNumber = entity.PreferredSetNumber
                };

                ctx.MatchPlayers.Add(matchPlayer);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<MatchPlayerListItem> GetMatchPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .MatchPlayers
                        .Select(
                            e =>
                                new MatchPlayerListItem
                                {
                                    PlayerId = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    City = e.City,
                                    State = e.State,
                                    DateJoined = e.DateJoined,
                                    SportsmanshipRating = e.SportsmanshipRating
                                }
                        );

                return query.ToArray();
            }
        }
        public bool EditMatchPlayer(MatchPlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MatchPlayers
                        .Single(e => e.Id == model.MatchPlayerId && e.UserId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.City = model.City;
                entity.State = model.State;
                entity.PreferredSetNumber = model.PreferredSetNumber;
                entity.Availability = model.Availability;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool RemoveMatchPlayer(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MatchPlayers
                        .Single(e => e.Id == id);

                ctx.MatchPlayers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

