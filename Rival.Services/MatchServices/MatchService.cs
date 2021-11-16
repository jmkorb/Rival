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
            using (var ctx = new ApplicationDbContext())
            {
                var userId = Guid.Parse(model.UserId);
                var currentUser =
                    ctx
                        .Players
                        .Single(e => e.UserId == userId);

                List<Player> players = new List<Player>();
                players.Add(currentUser);
                Player playerTwo = ctx.Players.Single(e => e.Id == model.PlayerTwoId);
                players.Add(playerTwo);

                var entity = new Match()
                {
                    CreatorId = Guid.Parse(model.UserId),
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
                var userId = Guid.Parse(model.UserId);
                var entity =
                    ctx
                        .Matches
                        .Where(e => e.CreatorId == userId)
                        .Single(e => e.Id == model.MatchId && e.CreatorId == userId);

                entity.Id = model.MatchId;
                entity.Date = model.Date;
                entity.Court = model.Court;
                entity.Winner = model.Winner;
                entity.FinalScore = model.FinalScore;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMatch(int id, string userId)
        {
            var creatorId = Guid.Parse(userId);
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                        .Matches
                        .Single(e => e.Id == id && e.CreatorId == creatorId);

                ctx.Matches.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MatchListItem> GetMatches()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Matches.Include("SetOfPlayers");
                List<MatchListItem> matchList = new List<MatchListItem>();

                foreach (var match in query)
                {
                    var matchListItem = new MatchListItem
                    {
                        MatchId = match.Id,
                        PlayerOne = match.SetOfPlayers[0],
                        PlayerTwo = match.SetOfPlayers[1],
                        Date = match.Date
                    };
                    matchList.Add(matchListItem);
                }
                // old code

                //for each (Match in matches){
                //    Match.
                //}
                //var query =
                //    ctx
                //        .Matches.
                //        .Select(
                //            e =>
                //                new MatchListItem
                //                {
                //                    MatchId = e.Id,
                //                    PlayerOne = e.SetOfPlayers.ElementAt(1),
                //                    PlayerTwo = e.SetOfPlayers.ElementAt(0),
                //                    Date = e.Date
                //                }
                //        );

                return matchList.ToArray();
            }
        }
        public IEnumerable<MatchListItem> GetUserMatches(int id)
        {
            //var id = Guid.Parse(userId);
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Matches.AsEnumerable();

                var matches = new List<MatchListItem>();

                foreach (var match in query)
                {
                    var players = match.SetOfPlayers;
                    foreach(var player in players)
                    {
                        if (player.Id == id)
                        {
                            var addItem = new MatchListItem
                            {
                                MatchId = match.Id,
                                PlayerOne = match.SetOfPlayers.ElementAt(1),
                                PlayerTwo = match.SetOfPlayers.ElementAt(0),
                                Date = match.Date
                            };
                            matches.Add(addItem);
                        }
                    }
                }

                return matches.ToArray();
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
                    PlayerOne = entity.SetOfPlayers.ElementAt(1),
                    PlayerTwo = entity.SetOfPlayers.ElementAt(0),
                    Date = entity.Date,
                    Winner = entity.Winner,
                    Court = entity.Court,
                    FinalScore = entity.FinalScore
                };
            }
        }
        public IEnumerable<Match> MatchesByDateDesc(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Matches.Where(e => e.SetOfPlayers[0].Id == id || e.SetOfPlayers[1].Id == id);
                var descendingQuery = query.OrderByDescending(d => d.Date);

                return descendingQuery;
            }
        }
    }
}
