namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch1Setup : GameSetupTemplate
    {
        protected override void InitializeBoard()
        {
            board = new int[10, 10];
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
