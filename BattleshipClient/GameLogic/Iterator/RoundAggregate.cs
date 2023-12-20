public class RoundAggregate
{
    private static int MAX_ROUNDS = 3;
    List<Round> Rounds = new List<Round>(MAX_ROUNDS);

    public RoundAggregate(Player player1, Player player2)
    {
        this.Rounds.Add(new Round(player1, player2));
        this.Rounds.Add(new Round(player1, player2));
        this.Rounds.Add(new Round(player1, player2));
    }

    public int GetMaxRounds()
    {
        return MAX_ROUNDS;
    }

    public Round GetRound(int index)
    {
        if (MAX_ROUNDS > index)
        {
            throw new Exception("Invalid round specified");
        }

        return this.Rounds[index];
    }
}
