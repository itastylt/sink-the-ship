namespace BattleshipClient.GameLogic.Flyweight
{

    public class ShipImageFlyweight
    {
        private string image; // Intrinsic data (shared among instances)

        public ShipImageFlyweight(string image)
        {
            this.image = image;
        }

        public void Display(int x, int y)
        {
            Console.WriteLine($"Displaying ship at ({x}, {y}) with image: {image}");
        }
    }
}
