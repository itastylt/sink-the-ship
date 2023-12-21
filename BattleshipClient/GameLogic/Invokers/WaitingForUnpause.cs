namespace BattleshipClient.GameLogic.Invokers
{
    public class WaitingForUnpause
    {
        ShipHub _hub;
        int number;

        public WaitingForUnpause(string user, string number, ShipHub hub)
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
                //ingame
                ShipPlayers.UpdateState();
            }else
            { 
                //pause
            }
            
        }

        public void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
