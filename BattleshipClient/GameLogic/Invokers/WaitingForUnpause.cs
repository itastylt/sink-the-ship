using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Main;
using BattleshipClient.GameLogic.Mediator;

namespace BattleshipClient.GameLogic.Invokers
{
    public class WaitingForUnpause :ICommand
    {
        ShipHub _hub;
        int number;
        string user;

        public WaitingForUnpause(string user, string number, ShipHub hub)
        {
            _hub = hub;
            number = number;
            this.user = user;
        }

        public async void execute()
        {
            if (number == 1)
            {
                MediatorImpl m = new MediatorImpl();

                GamePauser gamePauser = new GamePauser(m, "godas", _hub);
                Player player = ShipPlayers.GetPlayer(user);
                Player opponent = ShipPlayers.GetPlayerOpponent(user);

                m.addUser(gamePauser);
                m.addUser(player);
                m.addUser(opponent);
                m.broadcast(player, "WaitingForUnPause");
            }
            else if (number == 2)
            {
                MediatorImpl m = new MediatorImpl();

                GamePauser gamePauser = new GamePauser(m, "godas", _hub);
                Player player = ShipPlayers.GetPlayer(user);
                Player opponent = ShipPlayers.GetPlayerOpponent(user);

                m.addUser(gamePauser);
                m.addUser(player);
                m.addUser(opponent);
                m.broadcast(player, "UnPause");
            }
            else if (number == 3)
            {
                MediatorImpl m = new MediatorImpl();

                GamePauser gamePauser = new GamePauser(m, "godas", _hub);
                Player player = ShipPlayers.GetPlayer(user);
                Player opponent = ShipPlayers.GetPlayerOpponent(user);

                m.addUser(gamePauser);
                m.addUser(player);
                m.addUser(opponent);
                m.broadcast(player, "CancelUnPause");
            }
            

        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
