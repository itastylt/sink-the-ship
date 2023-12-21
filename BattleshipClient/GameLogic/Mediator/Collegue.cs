using System;

namespace BattleshipClient.GameLogic.Mediator
{
    public enum CollegueType {
        GamePauser,

    }

    public abstract class Collegue
    {
         MediatorImpl m;
         String name;

        public Collegue(MediatorImpl mediator, string newName)
        {
            m = mediator;
            name = newName;
        }

        public abstract CollegueType getType();

        public abstract void sendMessage(String msg);

        public abstract void receiveMessage(String msg);
    }
}
