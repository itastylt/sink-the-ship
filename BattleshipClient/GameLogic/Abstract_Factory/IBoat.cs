using BattleshipClient.GameLogic.Strategy.Decorator;
using BattleshipClient.GameLogic.Strategy;
using System.Runtime.CompilerServices;

namespace BattleshipClient.GameLogic.Factory
{
    public class IBoat : IShip
    {
        public IBoat(int X, int Y)
        {
            this.Cannon = new SingleShot(); //Applying Strategy
            this.Cannon = new EnhancedSingleShot(this.Cannon); //Applying Decorator
            this.X = X;
            this.Y = Y;
            this.Type = "Boat";
            this.Angle = 90;
            this.Size = 1;

        }

    }
}
