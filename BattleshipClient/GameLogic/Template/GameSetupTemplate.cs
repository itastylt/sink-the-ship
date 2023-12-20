namespace BattleshipClient.GameLogic.Template
{
    public abstract class GameSetupTemplate
    {
        protected int[,] board;
        protected string playerName1;
        protected string playerName2;

        public GameSetupTemplate(string playerName1, string playerName2)
        {
            this.playerName1 = playerName1;
            this.playerName2 = playerName2;
        }
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
