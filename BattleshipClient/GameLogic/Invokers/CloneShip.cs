using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Factory;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipClient.GameLogic.Invokers
{
    public class CloneShip : ICommand
    {
        string _user;


        ShipHub _hub;
        private IShip Clone;

        public CloneShip(string user, ShipHub hub)
        {
            _user = user;
            _hub = hub;
        }

        public async void execute()
        {
            Player cloner = ShipPlayers.GetPlayer(_user);
            Player oppenent = ShipPlayers.GetPlayerOpponent(_user);
            if (!cloner.GetState() || !cloner.GetClonePowerup())
            {
                Console.WriteLine("Illegal player turn");
            }
            else
            {
                cloner.DisableClonePowerup();

                IShip cloneableShip = cloner.GetShipsBoard().getShip(1);
                this.Clone = (IShip)cloneableShip.Clone();

                int[] coords = cloner.GetShipsBoard().GetAvailableCoordinate();
                this.Clone.X = coords[1];
                this.Clone.Y = coords[0];
                Console.WriteLine(string.Format("Cloned Y is {0}, X is {1}", coords[0], coords[1]));
                cloner.SetState(!cloner.GetState());
                oppenent.SetState(!oppenent.GetState());
                cloner.GetShipsBoard().PlaceShip(this.Clone);
                cloner.setClone(this.Clone);
                ShipPlayers.UpdatePlayer(_user, cloner);
                ShipPlayers.UpdatePlayer(oppenent.Name, oppenent);
                await _hub.Clients.All.SendAsync("ClonedBoard", cloner.Name, cloner.Name + ";" + cloner.GetShipsBoard().ToString() + ";" + oppenent.Name);
            }
        }

        public async void undoAsync()
        {
            Player cloner = ShipPlayers.GetPlayer(_user);
            Player oppenent = ShipPlayers.GetPlayerOpponent(_user);
            if (!cloner.GetState())
            {
                Console.WriteLine("Illegal player turn");
            }
            else
            {
                cloner.SetState(!cloner.GetState());
                oppenent.SetState(!oppenent.GetState());
                cloner.GetShipsBoard().UnPlaceShip(cloner.getClone());
                cloner.GetShipsBoard().PrintBoard();
                ShipPlayers.UpdatePlayer(_user, cloner);
                ShipPlayers.UpdatePlayer(oppenent.Name, oppenent);
                await _hub.Clients.All.SendAsync("UnClonedBoard", cloner.Name, cloner.Name + ";" + cloner.GetShipsBoard().ToString() + ";" + oppenent.Name);

            }
        }
    }
}
