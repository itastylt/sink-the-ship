namespace BattleshipClient.GameLogic.Factory
{
    public abstract class ITeam
    {
        public abstract ShipFactory GetFactory();
    }
}
