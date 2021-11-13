using Rival.Models.Courts;
using System.Collections.Generic;

namespace Rival.Services.CourtServices
{
    public interface ICourtService
    {
        bool CreateCourt(CourtCreate model);
        bool DeleteCourt(int id);
        bool EditCourt(CourtEdit model);
        CourtDetail GetCourtById(int id);
        IEnumerable<CourtListItem> GetCourts();
    }
}