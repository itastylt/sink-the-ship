namespace BattleshipClient.GameLogic.Factory
{
    public class RedTeam : ITeamFactory
    {
        public override ShipFactory GetFactory()
        {
            return new RedShipFactory();
        }
    }
}
