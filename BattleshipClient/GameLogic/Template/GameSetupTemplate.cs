namespace BattleshipClient.GameLogic.Template
{
    public abstract class GameSetupTemplate
    {
        protected int[,] board;
        public void SetupGame()
        {
            InitializeBoard();
            InitializeIslands();
            InitializeSeaMines();
        }
        protected abstract void InitializeBoard();
        protected abstract void InitializeIslands();
        protected abstract void InitializeSeaMines();
    }
}
