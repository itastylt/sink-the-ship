
using BattleshipClient.GameLogic.Mediator;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipClient.GameLogic.Main
{
    public class GamePauser : Collegue
    {
        protected MediatorImpl m;
        protected string name;
        protected ShipHub hub;

        public GamePauser(MediatorImpl mediator, string newName, ShipHub hub) : base(mediator, newName)
        {
            m = mediator;
            name = newName;
            this.hub = hub;
        }

        public override CollegueType getType()
        {
            return (CollegueType)Enum.Parse(typeof(CollegueType),"GamePauser");
        }

        public async override void receiveMessage(string msg)
        {
        
            if (msg == "WaitingForPause")
            {
                ShipPlayers.UpdateState();
                await hub.Clients.All.SendAsync("WaitingForPause");
            }
            else if (msg == "Pause")
            {
                ShipPlayers.UpdateState();
                await hub.Clients.All.SendAsync("Pause");
            }
            else if (msg == "CancelPause")
            {
                ShipPlayers.ResumeGameState();
                await hub.Clients.All.SendAsync("GameResumed");
            }
            else if (msg == "CancelUnPause")
            {
                ShipPlayers.SetPause();
                await hub.Clients.All.SendAsync("GamePaused");
            } 
            else if (msg == "WaitingForUnPause")
            {
                ShipPlayers.SetWaiting();
                await hub.Clients.All.SendAsync("WaitingForUnpause");
            } 
            else if (msg == "UnPause")
            {
                ShipPlayers.ResumeGameState();
                await hub.Clients.All.SendAsync("InGame");
            }
        }

        public override void sendMessage(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
