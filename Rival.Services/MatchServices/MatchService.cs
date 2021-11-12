using Rival.Data;
using Rival.Models.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Services.MatchServices
{
    public class MatchService
    {
        private readonly Guid _userId;

        public MatchService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMatch(MatchCreate model)
        {
            using (var ctx = new ApplicationDbContext()) 
            {
                var currentUser =
                    ctx
                        .Players
                        .Single(e => e.UserId == _userId);

                List<Player> players = new List<Player>();
                players.Add(currentUser);
                Player playerTwo = ctx.Players.Single(e => e.Id == model.PlayerTwoId);
                players.Add(playerTwo);

                var entity = new Match()
                {
                    CreatorId = _userId,
                    SetOfPlayers = players,
                    Date = model.Date,
                    CourtId = model.CourtId
                };

                ctx.Matches.Add(entity);
                var save = ctx.SaveChanges();

                return save >= 1;
            }
        }

        public bool EditMatch(MatchEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Matches
                        .Where(e => e.CreatorId == _userId)
                        .Single(e => e.Id == model.MatchId && e.CreatorId == _userId);

                entity.Id = model.MatchId;
                entity.Date = model.Date;
                entity.Court = model.Court;
                entity.Winner = model.Winner;
                entity.FinalScore = model.FinalScore;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMatch(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Matches
                        .Single(e => e.Id == id && e.CreatorId == _userId);

                ctx.Matches.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MatchListItem> GetMatches()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Matches.AsEnumerable()
                        .Where(e => e.CreatorId == _userId)
                        .Select(
                            e =>
                                new MatchListItem
                                {
                                    MatchId = e.Id,
                                    PlayerOne = e.SetOfPlayers.ElementAt(0),
                                    PlayerTwo = e.SetOfPlayers.ElementAt(1),
                                    Date = e.Date
                                }
                        );

                return query.ToArray();
            }
        }
        public MatchDetail GetMatchById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Matches
                        .Single(e => e.Id == id && e.CreatorId == _userId);

                return new MatchDetail
                {
                    MatchId = entity.Id,
                    PlayerOne = entity.SetOfPlayers.ElementAt(0),
                    PlayerTwo = entity.SetOfPlayers.ElementAt(1),
                    Date = entity.Date,
                    Court = entity.Court,
                    FinalScore = entity.FinalScore
                };
            }
        }
    }
}
