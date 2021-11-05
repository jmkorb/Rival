using Rival.Models.Matches;
using System.Collections.Generic;

namespace Rival.Services.MatchServices
{
    public interface IMatchService
    {
        bool CreateMatch(MatchCreate model);
        bool DeleteMatch(int id, string userId);
        bool EditMatch(MatchEdit model, string userId);
        MatchDetail GetMatchById(int id);
        IEnumerable<MatchListItem> GetMatches(string userId);
    }
}