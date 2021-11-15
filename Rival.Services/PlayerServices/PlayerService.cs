using Rival.Data;
using Rival.Models.PlayerModels;
using Rival.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rival.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        public bool CreatePlayer(PlayerCreate model)
        {
            var entity = new Player()
            {
                UserId = Guid.Parse(model.UserId),
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
        public bool EditPlayer(PlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.Id == model.PlayerId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.City = model.City;
                entity.State = model.State;
                entity.PreferredSetNumber = model.PreferredSetNumber;
                entity.Availability = model.Availability;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePlayer(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.Id == id);

                ctx.Players.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PlayerListItem> GetPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Select(
                            e =>
                                new PlayerListItem
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
        public PlayerDetail GetPlayerById(int id)
        { 
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players.Include("MatchesPlayed").Include("MatchesPlayed.SetOfPlayers")
                        .Single(e => e.Id == id);

                return new PlayerDetail
                {
                    PlayerId = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    City = entity.City,
                    State = entity.State,
                    DateJoined = entity.DateJoined,
                    PreferredSetNumber = entity.PreferredSetNumber,
                    Availability = entity.Availability,
                    MatchesPlayed = entity.MatchesPlayed.OrderByDescending(e => e.Date)
                };
            }
        }

        public PlayerDashboard GetPlayerDashboard(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var playerDetail = GetPlayerById(id);
                var possibleMatchups = PlayersMatchingState(userId);

                return new PlayerDashboard
                {
                    PlayerId = playerDetail.PlayerId,
                    FirstName = playerDetail.FirstName,
                    LastName = playerDetail.LastName,
                    City = playerDetail.City,
                    State = playerDetail.State,
                    DateJoined = playerDetail.DateJoined,
                    PreferredSetNumber = playerDetail.PreferredSetNumber,
                    Availability = playerDetail.Availability,
                    MatchesPlayed = playerDetail.MatchesPlayed,
                    PossibleMatchups = possibleMatchups
                };
            }
        }
        public IEnumerable<Player> PlayersMatchingState(string userId)
        {
            var currentUserId = Guid.Parse(userId);
            using (var ctx = new ApplicationDbContext())
            {
                var usersState = ctx.Players.Single(u => u.UserId == currentUserId).State;
                var query = ctx.Players.Where(q => q.State == usersState && q.UserId != currentUserId);

                return query.ToArray();
            }
        }
    }
}
