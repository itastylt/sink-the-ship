using BattleshipClient.GameLogic.Template;
using System.Runtime.CompilerServices;

public class FirstRoundChain : RoundChain
{
    private RoundIterator RoundIterator;
    private Round Current;
    private string Player1Name;
    private string Player2Name;

    public string GetPlayer1Name()
    { return Player1Name; }

    public void SetPlayer1Name( string name )
    { this.Player1Name = name; }

    public string GetPlayer2Name()
    { return Player2Name; }

    public void SetPlayer2Name(string name)
    { this.Player2Name = name; }

    public FirstRoundChain(RoundIterator roundIterator)
    {
        this.RoundIterator = roundIterator;
        this.Current = roundIterator.First();
    }


    public override void ExecuteRound(string name1, string name2)
    {
        GameSetupTemplate gameSetupTemplate = new NormalPlayMatch1Setup(this.Current.GetPlayer(name1).Name, this.Current.GetPlayer(name2).Name);
        gameSetupTemplate.SetupGame();
    }

    public override void SetNextChain()
    {
       if(this.RoundIterator.HasNext())
       {
            RoundIterator.Next();
       }

        ShipPlayers._roundChain = new SecondRoundChain(this.RoundIterator);
    }
}
