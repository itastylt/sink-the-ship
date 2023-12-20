namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch2Setup : GameSetupTemplate
    {
        public NormalPlayMatch2Setup(string playerName1, string playerName2) : base(playerName1, playerName2)
        {
        }

        protected override void InitializeBoard()
        {
            board = new int[12, 12];
        }

        protected override void InitializeIslands()
        {
            return;
        }

        protected override void InitializeSeaMines()
        {
            throw new NotImplementedException();
        }
    }
}
