namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch1Setup : GameSetupTemplate
    {
        public NormalPlayMatch1Setup(string playerName1, string playerName2) : base(playerName1, playerName2)
        {

        }

        protected override void InitializeBoard()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.UpdateBoardSize(10);
            board = new int[settings.BoardSize, settings.BoardSize];
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
