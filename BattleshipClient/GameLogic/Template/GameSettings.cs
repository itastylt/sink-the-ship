namespace BattleshipClient.GameLogic.Template
{
    public class GameSettings
    {
        private static GameSettings instance = null;
        public int BoardSize { get; private set; }
        public int MineCount { get; private set; }

        private GameSettings(int? boardSize = null, int? mineCount = null)
        {
            this.BoardSize = boardSize ?? 10;
            this.MineCount = mineCount ?? 0;
        }

        public static GameSettings GetInstance(int? boardSize = null)
        {
            if (instance == null)
            {
                instance = new GameSettings(boardSize);
            }
            return instance;
        }
        public void UpdateBoardSize(int newBoardSize)
        {
            this.BoardSize = newBoardSize;
        }
        public void UpdateLandMineCount(int newLandMineCount)
        {
            this.MineCount = newLandMineCount;
        }
    }
}
