using Rival.Data;
using Rival.Models.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Services.MatchServices
{
    public class MatchService : IMatchService
    {
        public bool CreateMatch(MatchCreate model)
        {
            var entity = new Match()
            {
                Court = model.Court,
                Date = model.Date
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Matches.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool EditMatch(MatchEdit model, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Matches
                        .Single(e => e.Id == model.MatchId);

                entity.Court = model.Court;
                entity.Date = model.Date;
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
                        .Single(e => e.Id == id);

                ctx.Matches.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MatchListItem> GetMatches(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Matches
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

        public MatchDetail GetMatchById(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Matches
                        .Single(e => e.Id == id);

                return new MatchDetail
                {
                    MatchId = entity.Id,
                    PlayerOne = entity.PlayerOne,
                    PlayerTwo = entity.PlayerTwo,
                    Court = entity.Court,
                    Date = entity.Date,
                    FinalScore = entity.FinalScore
                };
            }
        }
    }
}
