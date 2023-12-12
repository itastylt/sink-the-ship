using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Factory;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;


namespace BattleshipClient.GameLogic.Invokers
{
    public class FireGroup : ICommand
    {
        string _message;
        string _user;
        string _messageArgs;

        ShipHub _hub;

        public FireGroup(string messageArgs, string message, string user, ShipHub hub)
        {
            _message = message;
            _messageArgs = messageArgs;
            _user = user;
            _hub = hub;
        }

        public async void execute()
        {
            int x_cord = int.Parse(_messageArgs);
            int y_cord = int.Parse(_message.Split(';')[2]);

            Player current_player = ShipPlayers.GetPlayer(_user);
            if (!current_player.GetState())
            {
                Console.WriteLine("Illegal player turn");
            }
            else
            {
                Player opponent_player = ShipPlayers.GetPlayerOpponent(_user);
                Console.WriteLine(String.Format(x_cord + " " + y_cord));
                current_player.GetShipsBoard().getShipGroup().FireWeapon(opponent_player, x_cord, y_cord, 0);

                current_player.SetState(!current_player.GetState());
                opponent_player.SetState(!opponent_player.GetState());
                ShipPlayers.UpdatePlayer(current_player.Name, current_player);
                ShipPlayers.UpdatePlayer(opponent_player.Name, opponent_player);
                if (opponent_player.GetShipsBoard().BoardEnd())
                {
                    await _hub.Clients.All.SendAsync("WinnerGame", current_player.Name, current_player.Name + ";");
                }
                else
                {
                    await _hub.Clients.All.SendAsync("FireShot", current_player.Name, opponent_player.Name + ";" + opponent_player.GetShipsBoard().ToString() + ";" + opponent_player.Name);
                }
            }
        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
