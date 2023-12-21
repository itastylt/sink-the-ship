using BattleshipClient.GameLogic.Command;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipClient.GameLogic.Invokers
{
    public class GamePaused : ICommand
    {
        ShipHub _hub;

        public GamePaused(string user, ShipHub hub)
        { 
            _hub = hub;
        }

        public async void execute()
        {
            ShipPlayers.UpdateState();
            await _hub.Clients.All.SendAsync("GamePaused");
        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
