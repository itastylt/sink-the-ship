using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Flyweight;

namespace BattleshipClient.GameLogic.Abstract_Factory
{
    public class NoTeamFactory : ShipFactory
    {
        private Dictionary<string, ShipImageFlyweight> imageFlyweights
            = new Dictionary<string, ShipImageFlyweight>();

        private ShipImageFlyweight GetImageFlyweight(string image)
        {
            if (!imageFlyweights.ContainsKey(image))
            {
                imageFlyweights[image] = new ShipImageFlyweight(image);
            }

            return imageFlyweights[image];
        }
        public IShip CreateBoat(int x, int y)
        {
            // Use Flyweight to share ship images
            ShipImageFlyweight imageFlyweight = GetImageFlyweight("boat_image.jpg");
            return new IBoat(x, y);
        }

        public IShip CreateDestroyer(int x, int y)
        {
            ShipImageFlyweight imageFlyweight = GetImageFlyweight("destroyer_image.jpg");
            return new IDestroyer(x, y);
        }

        public IShip CreateLavantier(int x, int y)
        {
            ShipImageFlyweight imageFlyweight = GetImageFlyweight("lavantier_image.jpg");
            return new ILavantier(x, y);
        }

        public IShip CreateSubmarine(int x, int y)
        {
            ShipImageFlyweight imageFlyweight = GetImageFlyweight("submarine_image.jpg");
            return new ISubmarine(x, y);
        }
    }
}
