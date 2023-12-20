namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch3Setup : GameSetupTemplate
    {
        public NormalPlayMatch3Setup(string playerName1, string playerName2) : base(playerName1, playerName2)
        {
        }

        protected override void InitializeBoard()
        {
            board = new int[15, 15];
        }

        protected override void InitializeIslands()
        {
            throw new NotImplementedException();
        }

        protected override void InitializeSeaMines()
        {
            throw new NotImplementedException();
        }
    }
}
