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

        public bool CreateMatch(MatchDetail model)
        {
            var entity = new Match()
            {
                PlayerOne = model.PlayerOne,
                PlayerTwo = model.PlayerTwo,
                Court = model.Court,
                Date = model.Date,
                FinalScore = model.FinalScore
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Matches.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool EditMatch(MatchDetail model)
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

        public IEnumerable<MatchDetail> GetMatches()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Matches
                        .Select(
                            e =>
                                new MatchDetail
                                {
                                    MatchId = e.Id,
                                    PlayerOne = e.PlayerOne,
                                    PlayerTwo = e.PlayerTwo,
                                    Court = e.Court,
                                    Date = e.Date,
                                    FinalScore = e.FinalScore
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
