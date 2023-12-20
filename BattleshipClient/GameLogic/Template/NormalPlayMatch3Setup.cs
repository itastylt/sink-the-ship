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
            settings.UpdateBoardSize(15);
            board = new int[settings.BoardSize, settings.BoardSize];
        }

        protected override void InitializeIslands()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.UpdateIslandCount(6);
        }

        protected override void InitializeSeaMines()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.UpdateLandMineCount(2);
        }
    }
}
