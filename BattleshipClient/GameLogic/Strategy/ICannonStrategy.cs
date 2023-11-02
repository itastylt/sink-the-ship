namespace BattleshipClient.GameLogic.Strategy
{
    public interface ICannonStrategy
    {
        void Fire(Player opponent, int x, int y, int flag);
    }
}
