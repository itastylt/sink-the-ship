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
            GameSettings.UpdateBoardSize(12);
            board = new int[GameSettings.BoardSize, GameSettings.BoardSize];
        }

        protected override void InitializeIslands()
        {
            GameSettings settings = GameSettings.GetInstance();
            GameSettings.UpdateIslandCount(0);
        }

        protected override void InitializeSeaMines()
        {
            GameSettings settings = GameSettings.GetInstance();
            GameSettings.UpdateLandMineCount(2);
        }
    }
}
