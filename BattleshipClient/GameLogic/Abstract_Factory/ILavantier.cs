using BattleshipClient.GameLogic.Strategy;

namespace BattleshipClient.GameLogic.Factory
{
    public class ILavantier : IShip
    {
        public ILavantier(int X, int Y)
        {
            this.Cannon = new HorizontalShot();
            this.X = X;
            this.Y = Y;
            this.Type = "Lavantier";
            this.Angle = 90;
            this.Size = 2;
        }
    }
}
