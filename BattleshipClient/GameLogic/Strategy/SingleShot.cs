namespace BattleshipClient.GameLogic.Strategy
{
    public class SingleShot : ICannonStrategy
    {
        public void Fire(int x, int y)
        {
            Console.WriteLine("Firing with Single Shot");
        }
    }
}
