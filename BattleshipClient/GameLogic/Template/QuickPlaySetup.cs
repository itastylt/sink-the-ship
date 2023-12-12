namespace BattleshipClient.GameLogic.Template
{
    public class QuickPlaySetup : GameSetupTemplate
    {
        protected override void InitializeBoard()
        {
            board = new int[8, 8];
        }

        protected override void InitializeIslands()
        {
            return;
        }

        protected override void InitializeSeaMines()
        {
            return;
        }
    }
}
