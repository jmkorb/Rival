using Rival.Data;
using Rival.Models.Matches;
using System.Collections.Generic;

namespace Rival.Services.MatchServices
{
    public interface IMatchService
    {
        bool CreateMatch(MatchCreate model);
        bool DeleteMatch(int id, string userId);
        bool EditMatch(MatchEdit model);
        MatchDetail GetMatchById(int id);
        IEnumerable<MatchListItem> GetMatches();
        IEnumerable<Match> MatchesByDateDesc(int id);
    }
}