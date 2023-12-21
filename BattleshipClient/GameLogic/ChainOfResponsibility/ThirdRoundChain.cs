
using BattleshipClient.GameLogic.Template;

public class ThirdRoundChain : RoundChain
{
    private RoundIterator RoundIterator;
    private Round Current;

    public ThirdRoundChain(RoundIterator roundIterator)
    {
        RoundIterator = roundIterator;
        Current = this.RoundIterator.Current();
    }

    public override void ExecuteRound(string name1, string name2)
    {
        GameSetupTemplate gameSetupTemplate = new NormalPlayMatch3Setup(this.Current.GetPlayer(name1).Name, this.Current.GetPlayer(name2).Name);
        gameSetupTemplate.SetupGame();
        Current.GetPlayer(name1).SetShipsBoard(new ShipsBoard());
        Current.GetPlayer(name2).SetShipsBoard(new ShipsBoard());
        ShipPlayers.UpdatePlayer(name1, Current.GetPlayer(name1));
        ShipPlayers.UpdatePlayer(name2, Current.GetPlayer(name2));

    }

    public override void SetNextChain()
    {
        return;
    }
}
