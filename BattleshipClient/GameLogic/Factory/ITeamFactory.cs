namespace BattleshipClient.GameLogic.Factory
{
    public class ITeamFactory
    {
        public ITeam GetTeam(string team)
        {
            if (team == "B")
            {
                return new BlueTeam();
            }
            if (team == "R")
            {
                return new RedTeam();
            }
           return new NoTeam();
        }

    }
}
