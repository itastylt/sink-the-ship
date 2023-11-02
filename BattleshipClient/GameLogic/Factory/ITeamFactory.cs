namespace BattleshipClient.GameLogic.Factory
{
    public abstract class ITeamFactory
    {
        public abstract ShipFactory GetFactory();
    }
}
