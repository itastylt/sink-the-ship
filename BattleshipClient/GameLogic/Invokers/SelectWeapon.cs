using BattleshipClient.GameLogic.Command;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipClient.GameLogic.Invokers
{
    public class SelectWeapon : ICommand
    {
        string _message;
        string _user;
        ShipHub _hub;

        public SelectWeapon(string message, string user, ShipHub hub) 
        { 
            _message = message;
            _user = user;
            _hub = hub;
        }

        public void execute()
        {
            int chosenWeaponNumber = int.Parse(_message);
            Player player1 = ShipPlayers.GetPlayer(_user);

            player1.SetSelectedShip(chosenWeaponNumber);
            ShipPlayers.UpdatePlayer(_user, player1);
        }

        public async void undoAsync()
        {
            Player player1 = ShipPlayers.GetPlayer(_user);
            player1.CleanSelectedShip();
            ShipPlayers.UpdatePlayer(_user, player1);
            await _hub.Clients.All.SendAsync("UnScope;");
        }
    }
}
