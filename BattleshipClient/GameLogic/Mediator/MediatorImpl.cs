namespace BattleshipClient.GameLogic.Mediator
{
    public class MediatorImpl : Mediator
    {

        List<Collegue> collegues = new List<Collegue>();

        public void addUser(Collegue user)
        {
            collegues.Add(user);
        }

        public void broadcast(Collegue sender, string msg)
        {
            foreach (var collegue in collegues)
            {
                collegue.receiveMessage(msg);
            }
        }
    }
}
