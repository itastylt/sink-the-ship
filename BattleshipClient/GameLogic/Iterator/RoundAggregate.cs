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
        if (index > MAX_ROUNDS)
        {
            throw new Exception("Invalid round specified");
        }

        return this.Rounds[index];
    }

    public RoundIterator GetRoundIterator()
    {
        return new RoundIterator(this);
    }
}
