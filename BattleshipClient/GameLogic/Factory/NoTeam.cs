using BattleshipClient.GameLogic.Abstract_Factory;

namespace BattleshipClient.GameLogic.Factory
{
    public class NoTeam : ITeamFactory
    {
        public override ShipFactory GetFactory()
        {
            return new NoTeamFactory();
        }
    }
}
