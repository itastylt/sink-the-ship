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

        readonly ShipHub _hub;

        public Ready(string messageArgs,string user, ShipHub hub)
        {
            _hub = hub;
            _messageArgs = messageArgs;
            _user = user;
        }

        public async void execute()
        {
            ShipsBoard userBoard = new ShipsBoard();

            List<PlacedShip> ships = JsonSerializer.Deserialize<List<PlacedShip>>(_messageArgs);

            foreach (PlacedShip ship in ships) //Adding different cannon strategies to different weapons
            {
                switch (ship.Type)
                {
                    case "Boat":
                        ship.Cannon = new SingleShot(); //Applying Strategy
                        ship.Cannon = new EnhancedSingleShot(ship.Cannon); //Applying Decorator
                        break;
                    case "Lavantier":
                        ship.Cannon = new HorizontalShot();
                        break;
                    case "Submarine":
                        ship.Cannon = new VerticalShot();
                        break;
                    case "Destroyer":
                        ship.Cannon = new DiagonalShot();
                        break;
                }
            }

            foreach (PlacedShip ship in ships)
            {
                userBoard.PlaceShip(ship);
            }


            Player player = new Player(_user);
            player.SetShipsBoard(userBoard);

            List<Player> Players = ShipPlayers.AddPlayer(player);

            foreach (var online in Players)
            {
                Console.WriteLine(online.Name);
            }

            if (Players.Count % 2 == 0)
            {
                Random random = new Random();
                int random_number = random.Next(Players.Count());
                Console.WriteLine(random_number);
                Player luckyPlayer = Players.ElementAt(random_number);
                luckyPlayer.SetState(true);
                ShipPlayers.UpdatePlayer(luckyPlayer.Name, luckyPlayer);
                foreach (var online in Players)
                {
                    await _hub.Clients.All.SendAsync("StartGame", luckyPlayer.Name, online.Name + ";" + online.GetShipsBoard().ToString() + ";" + luckyPlayer.Name);
                }


            }
        }

        public void undo()
        {
            throw new NotImplementedException();
        }
    }
}
