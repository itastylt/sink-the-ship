using BattleshipClient.GameLogic.Abstract_Factory;

namespace BattleshipClient.GameLogic.Factory
{
    public class NoTeam : ITeam
    {
        public override ShipFactory GetFactory()
        {
            return new NoTeamFactory();
        }
    }
}
