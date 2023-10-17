namespace BattleshipClient.GameLogic.Strategy
{
    public class HorizontalShot : ICannonStrategy
    {
        public void Fire(int x, int y)
        {
            Console.WriteLine("Firing with Horizontal Shot");
        }
    }
}
