using BattleshipClient.GameLogic.Bridge;
using BattleshipClient.GameLogic.Strategy;

namespace BattleshipClient.GameLogic.Factory
{
    public class IDestroyer : IShip
    {
        public IDestroyer(int X, int Y)
        {
            this.Cannon = new DiagonalShot();
            this.X = X;
            this.Y = Y;
            this.Type = "Destroyer";
            this.Angle = 90;
            this.Size = 4;
            this.engine = new GasEngine();
        }
    }
}
