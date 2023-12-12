namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch3Setup : GameSetupTemplate
    {
        protected override void InitializeBoard()
        {
            board = new int[15, 15];
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
