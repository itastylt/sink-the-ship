namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch3Setup : GameSetupTemplate
    {
        public NormalPlayMatch3Setup(string playerName1, string playerName2) : base(playerName1, playerName2)
        {
        }

        protected override void InitializeBoard()
        {
            GameSettings settings = GameSettings.GetInstance();
            GameSettings.UpdateBoardSize(13);
            board = new int[GameSettings.BoardSize, GameSettings.BoardSize];
        }

        protected override void InitializeIslands()
        {
            GameSettings settings = GameSettings.GetInstance();
            GameSettings.UpdateIslandCount(6);
        }

        protected override void InitializeSeaMines()
        {
            GameSettings settings = GameSettings.GetInstance();
            GameSettings.UpdateLandMineCount(2);
        }
    }
}
