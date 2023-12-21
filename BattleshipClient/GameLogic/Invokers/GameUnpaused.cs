using BattleshipClient.GameLogic.Command;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipClient.GameLogic.Invokers
{
    public class GameUnpaused : ICommand
    {
        ShipHub _hub;

        public GameUnpaused(string user,string number, ShipHub hub)
        { 
            _hub = hub;
        }

        public async void execute()
        {
            ShipPlayers.UpdateState();
            await _hub.Clients.All.SendAsync("GameUnpaused");
        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
