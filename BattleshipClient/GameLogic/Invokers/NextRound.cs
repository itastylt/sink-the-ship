using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Strategy;
using BattleshipClient.GameLogic.Strategy.Decorator;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace BattleshipClient.GameLogic.Invokers
{
    public class NextRound : ICommand
    {

        string _messageArgs;
        string _user;
        readonly ShipHub _hub;
        public NextRound(string messageArgs,string user, ShipHub hub)
        {
            _hub = hub;
            _messageArgs = messageArgs;
            _user = user;
            
        }

        public async void execute()
        {
            List<PlacedShip> ships = JsonSerializer.Deserialize<List<PlacedShip>>(_messageArgs);
            ShipsBoard board = FormBoard(ships);
            board.PlaceLandMines();
            board.PlaceIslands();
            Player player = ShipPlayers.GetPlayer(_user);
            player.SetShipsBoard(board);
            ShipPlayers.UpdatePlayer(_user, player);
            if(ShipPlayers.IncreaseRoundState()) foreach(Player x in ShipPlayers.GetPlayers()) await this._hub.Clients.All.SendAsync("Continue",x.Name, x.Name + ";" + x.GetShipsBoard().ToString());
        }

        private ShipsBoard FormBoard(IEnumerable<PlacedShip> shipList)
        {
            ShipsBoard shipsBoard = new ShipsBoard();

            ITeamFactory iTeamFactory = new ITeamFactory();

            ShipFactory teamFactory = null;

            if (ShipPlayers.PlayerCount() == 0)
            {
                teamFactory = iTeamFactory.GetTeam("B").GetFactory();

            }
            else
            {
                teamFactory = iTeamFactory.GetTeam("R").GetFactory();
            }
            if (teamFactory == null) { throw new Exception("Shipfactory team problem "); }

            foreach (PlacedShip ship in shipList) //Adding different cannon strategies to different weapons
            {
                switch (ship.Type)
                {
                    case "Boat":
                        IShip newShipB = teamFactory.CreateBoat(ship.X, ship.Y);
                        shipsBoard.PlaceShip(newShipB);
                        break;
                    case "Lavantier":

                        IShip newShipL = teamFactory.CreateLavantier(ship.X, ship.Y);
                        shipsBoard.PlaceShip(newShipL);
                        break;
                    case "Submarine":

                        IShip newShipS = teamFactory.CreateSubmarine(ship.X, ship.Y);
                        shipsBoard.PlaceShip(newShipS);
                        break;
                    case "Destroyer":

                        IShip newShipD = teamFactory.CreateDestroyer(ship.X, ship.Y);
                        shipsBoard.PlaceShip(newShipD);
                        break;
                    default:
                        throw new Exception("Invalid Ship type!");
                }
            }

            return shipsBoard;
        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
