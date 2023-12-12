namespace BattleshipClient.GameLogic.Composite
{
    public interface IShipComponent
    {
        void FireWeapon(Player opponent, int x, int y, int flag);
    }
}
