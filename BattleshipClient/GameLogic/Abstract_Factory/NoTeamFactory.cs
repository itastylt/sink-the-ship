using BattleshipClient.GameLogic.Factory;

namespace BattleshipClient.GameLogic.Abstract_Factory
{
    public class NoTeamFactory : ShipFactory
    {
        public IShip CreateBoat(int x, int y)
        {
            return new IBoat(x, y);
        }

        public IShip CreateDestroyer(int x, int y)
        {
            return new IDestroyer(x, y);
        }

        public IShip CreateLavantier(int x, int y)
        {
            return new ILavantier(x, y);
        }

        public IShip CreateSubmarine(int x, int y)
        {
            return new ISubmarine(x, y);
        }
    }
}
