namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch2Setup : GameSetupTemplate // landmines, adds up to the enemy board
    {
        public NormalPlayMatch2Setup(string playerName1, string playerName2) : base(playerName1, playerName2)
        {
        }

        protected override void InitializeBoard()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.UpdateBoardSize(12);
            board = new int[settings.BoardSize, settings.BoardSize];
        }

        protected override void InitializeIslands()
        {
            GameSettings settings = GameSettings.GetInstance();
        }

        protected override void InitializeSeaMines()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.UpdateLandMineCount(2);
        }
    }
}
