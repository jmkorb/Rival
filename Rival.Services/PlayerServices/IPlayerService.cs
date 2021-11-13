using Rival.Models.PlayerModels;
using Rival.Models.Players;
using System.Collections.Generic;

namespace Rival.Services.PlayerServices
{
    public interface IPlayerService
    {
        bool CreatePlayer(PlayerCreate model);
        bool DeletePlayer(int id, string userId);
        bool EditPlayer(PlayerEdit model);
        PlayerDetail GetPlayerById(int id);
        IEnumerable<PlayerListItem> GetPlayers();
    }
}