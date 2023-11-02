using BattleshipClient.GameLogic.Strategy;

namespace BattleshipClient.GameLogic.Factory
{
    public abstract class IShip : ICloneable
    {
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Angle { get; set; }

        //Strategy pattern
        public ICannonStrategy Cannon { get; set; }

        public void FireWeapon(Player opponent, int x, int y)
        {
            Cannon.Fire(opponent, x, y);
        }

        // Prototype pattern
        public object Clone()
        {
            return (IShip)this.MemberwiseClone();
        }
    }
}
