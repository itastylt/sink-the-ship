namespace BattleshipClient.GameLogic.Mediator
{
    public interface Mediator
    {

        public void addUser(Collegue user);
        public void broadcast(Collegue sender, String msg);
    }
}
