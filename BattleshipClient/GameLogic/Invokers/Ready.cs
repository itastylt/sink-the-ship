using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Strategy;
using BattleshipClient.GameLogic.Strategy.Decorator;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace BattleshipClient.GameLogic.Invokers
{
    public class Ready : ICommand
    {

        string _messageArgs;
        string _user;
        private ReadyFacade Facade;
        readonly ShipHub _hub;

        public Ready(string messageArgs,string user, ShipHub hub)
        {
            _hub = hub;
            _messageArgs = messageArgs;
            _user = user;
            this.Facade = new ReadyFacade(this._hub);
        }

        public async void execute()
        {
            List<PlacedShip> ships = JsonSerializer.Deserialize<List<PlacedShip>>(_messageArgs);
            
            if (ships != null)
            {
                this.Facade.FormBoard(ships);
            } 
            else
            {
                throw new Exception("Invalid ships JSON provided!");
            }

            List<Player> players = this.Facade.CreatePlayer(this._user);

            this.Facade.StartPlayers(players);
        }
        public async void executeRandomPlayer()
        {
            ShipsBoard board = new ShipsBoard();
            ShipBuilder shipBuilder = new ShipBuilder(board);
            shipBuilder.BuildRandomShips();

            PlayerBuilder playerBuilder = new PlayerBuilder(board);
            Player randomPlayer = playerBuilder.CreateRandomPlayer(this._user);

            this.Facade = new ReadyFacade(this._hub);
            this.Facade.SetBoard(board);
            List<Player> players = this.Facade.CreateRandomPlayer(randomPlayer);
            this.Facade.StartPlayers(players);
        }

        public void undoAsync()
        {
            string temp = this._user;
            this.Facade.UnreadyPlayer(temp);
        }
    }
}
