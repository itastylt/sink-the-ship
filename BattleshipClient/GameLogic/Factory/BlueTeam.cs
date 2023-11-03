namespace BattleshipClient.GameLogic.Factory
{
    public class BlueTeam : ITeam
    {
        public override ShipFactory GetFactory()
        {
            return new BlueShipFactory();
        }
    }
}
