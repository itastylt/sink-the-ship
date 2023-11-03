namespace BattleshipClient.GameLogic.Factory
{
    public class RedTeam : ITeam
    {
        public override ShipFactory GetFactory()
        {
            return new RedShipFactory();
        }
    }
}
