namespace BattleshipClient.GameLogic.Template
{
    public  class GameSettings
    {
        private static GameSettings instance = null;
        public static int BoardSize { get; private set; }
        public static int MineCount { get; private set; }
        public static int IslandCount { get; private set; }

        private GameSettings(int? boardSize = null, int? mineCount = null, int? islandCount = null)
        {
            BoardSize = boardSize ?? 10;
            MineCount = mineCount ?? 0;
            IslandCount = islandCount ?? 0;
        }

        public static GameSettings GetInstance(int? boardSize = null)
        {
            if (instance == null)
            {
                instance = new GameSettings(boardSize);
            }
            return instance;
        }
        public static void UpdateBoardSize(int newBoardSize)
        {
            BoardSize = newBoardSize;
        }
        public static void UpdateLandMineCount(int newLandMineCount)
        {
            MineCount = newLandMineCount;
        }
        public static void UpdateIslandCount(int newLandMineCount)
        {
            MineCount = newLandMineCount;
        }
    }
}
