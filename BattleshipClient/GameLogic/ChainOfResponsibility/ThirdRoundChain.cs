
using BattleshipClient.GameLogic.Template;

public class ThirdRoundChain : RoundChain
{
        private RoundIterator RoundIterator;
        private Round Current;
        
        public ThirdRoundChain(RoundIterator roundIterator)
        {
            RoundIterator = roundIterator;
            Current = roundIterator.Current();
        }

        public override void ExecuteRound(string name1, string name2)
        {
        Current.GetPlayer(name1).SetShipsBoard(new ShipsBoard());
        Current.GetPlayer(name2).SetShipsBoard(new ShipsBoard());

        GameSetupTemplate gameSetupTemplate = new NormalPlayMatch3Setup(this.Current.GetPlayer(name1).Name, this.Current.GetPlayer(name2).Name);
        gameSetupTemplate.SetupGame(this.Current.GetPlayer(name1).Name, this.Current.GetPlayer(name2).Name);
    }

        public override RoundChain SetNextChain()
        {
            return this;
        }
}
