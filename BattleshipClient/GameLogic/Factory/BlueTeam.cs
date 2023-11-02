namespace BattleshipClient.GameLogic.Factory
{
    public class BlueTeam : ITeamFactory
    {
        public override ShipFactory GetFactory()
        {
            return new BlueShipFactory();
        }
    }
}
