using BattleshipClient.GameLogic.Command;

namespace BattleshipClient.GameLogic.Invokers
{
    public class WaitingForPause : ICommand
    {

        ShipHub _hub;
        int number;

        public WaitingForPause(string user,string number, ShipHub hub) 
        { 
            _hub = hub;
            number = number;
        }

        public async void execute()
        {
            if (number == 1)
            {
                ShipPlayers.UpdateState();
            }
            else if (number == 2)
            {
                ShipPlayers.UpdateState();
            }
            else { 
                
            }
            
        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
