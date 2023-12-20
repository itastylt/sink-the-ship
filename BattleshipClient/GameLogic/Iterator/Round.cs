public class Round
{
    private Player Player1;
    private Player Player2;
    
    public Round(Player player1, Player player2)
    {
        this.Player1 = player1;
        this.Player2 = player2;
    }

    public Player GetPlayer(string name)
    {
        if ( name == null)
        {
            throw new Exception("Unspecified name error");
        }

        if (Player1.Equals(name))
        {
            return Player1;
        }
        else if(Player2.Equals(name))
        {
            return Player2;
        }

        throw new Exception("Player not found in this round");
    }
}
