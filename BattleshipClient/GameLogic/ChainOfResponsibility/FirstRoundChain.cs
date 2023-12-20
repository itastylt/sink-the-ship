using BattleshipClient.GameLogic.Template;
using System.Runtime.CompilerServices;

public class FirstRoundChain : RoundChain
{
    private RoundIterator RoundIterator;
    private Round Current;

    public FirstRoundChain(RoundIterator roundIterator)
    {
        this.RoundIterator = roundIterator;
        this.Current = roundIterator.First();
    }

    public override void ExecuteRound(string name1, string name2)
    {
        GameSetupTemplate gameSetupTemplate = new NormalPlayMatch1Setup(this.Current.GetPlayer(name1).Name, this.Current.GetPlayer(name2).Name);
        gameSetupTemplate.SetupGame(this.Current.GetPlayer(name1).Name, this.Current.GetPlayer(name2).Name);
    }

    public override RoundChain SetNextChain()
    {
       if(this.RoundIterator.HasNext())
       {
            Round nextRound = RoundIterator.Next();
       }

        return new SecondRoundChain(this.RoundIterator);
    }
}
