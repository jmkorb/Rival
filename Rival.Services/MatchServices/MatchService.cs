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
                        .MatchPlayers
                        .Single(e => e.UserId == _userId);

                var matchPlayerOne = new MatchPlayerRecord
                {
                    MatchPlayerId = currentUser.Id,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName
                };

                var matchPlayerTwo = new MatchPlayerRecord
                {
                    MatchPlayerId = model.PlayerTwo.Id,
                    FirstName = model.PlayerTwo.FirstName,
                    LastName = model.PlayerTwo.LastName
                };

                var entity = new Match()
                {
                    CreatorId = _userId,
                    PlayerOne = matchPlayerOne,
                    PlayerTwo = matchPlayerTwo,
                    Date = model.Date,
                    Court = model.Court
                };

                ctx.Matches.Add(entity);
                return ctx.SaveChanges() == 1;
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
                        .Matches
                        .Where(e => e.CreatorId == _userId)
                        .Select(
                            e =>
                                new MatchListItem
                                {
                                    MatchId = e.Id,
                                    PlayerOne = e.PlayerOne,
                                    PlayerTwo = e.PlayerTwo,
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
                    PlayerOne = entity.PlayerOne,
                    PlayerTwo = entity.PlayerTwo,
                    Date = entity.Date,
                    Court = entity.Court,
                    FinalScore = entity.FinalScore
                };
            }
        }
    }
}
