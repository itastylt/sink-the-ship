using BattleshipClient.GameLogic.Strategy;

namespace BattleshipClient.GameLogic.Factory
{
    public class ISubmarine : IShip
    {
        public ISubmarine(int X, int Y)
        {
            this.Cannon = new VerticalShot();
            this.X = X;
            this.Y = Y;
            this.Type = "Submarine";
            this.Angle = 90;
            this.Size = 3;
        }
    }
}
