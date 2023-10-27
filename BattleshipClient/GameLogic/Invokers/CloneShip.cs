using BattleshipClient.GameLogic.Command;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipClient.GameLogic.Invokers
{
    public class CloneShip : ICommand
    {
        string _user;

        ShipHub _hub;

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
                PlacedShip cloneableShip = cloner.GetShipsBoard().getShip(1);
                PlacedShip clone = (PlacedShip)cloneableShip.Clone();

                int[] coords = cloner.GetShipsBoard().GetAvailableCoordinate();
                clone.X = coords[1];
                clone.Y = coords[0];
                Console.WriteLine(string.Format("Cloned Y is {0}, X is {1}", coords[0], coords[1]));
                cloner.SetState(!cloner.GetState());
                oppenent.SetState(!oppenent.GetState());
                cloner.GetShipsBoard().PlaceShip(clone);
                ShipPlayers.UpdatePlayer(_user, cloner);
                ShipPlayers.UpdatePlayer(oppenent.Name, oppenent);
                await _hub.Clients.All.SendAsync("ClonedBoard", cloner.Name, cloner.Name + ";" + cloner.GetShipsBoard().ToString() + ";" + oppenent.Name);
            }
        }

        public void undo()
        {
            throw new NotImplementedException();
        }
    }
}
