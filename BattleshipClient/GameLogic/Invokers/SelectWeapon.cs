using BattleshipClient.GameLogic.Command;

namespace BattleshipClient.GameLogic.Invokers
{
    public class SelectWeapon : ICommand
    {
        string _message;
        string _user;

        public SelectWeapon(string message, string user) 
        { 
            _message = message;
            _user = user;
        }

        public void execute()
        {
            int chosenWeaponNumber = int.Parse(_message);
            Player player1 = ShipPlayers.GetPlayer(_user);

            player1.SetSelectedShip(chosenWeaponNumber);
            ShipPlayers.UpdatePlayer(_user, player1);
        }

        public void undo()
        {
            throw new NotImplementedException();
        }
    }
}
