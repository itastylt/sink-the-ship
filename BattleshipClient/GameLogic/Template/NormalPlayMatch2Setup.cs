namespace BattleshipClient.GameLogic.Template
{
    public class NormalPlayMatch2Setup : GameSetupTemplate
    {
        protected override void InitializeBoard()
        {
            board = new int[12, 12];
        }

        protected override void InitializeIslands()
        {
            return;
        }

        protected override void InitializeSeaMines()
        {
            throw new NotImplementedException();
        }
    }
}
