namespace BattleshipClient.GameLogic.Factory
{
    public interface ShipFactory
    {
        IShip CreateBoat(int x, int y);
        IShip CreateLavantier(int x, int y);
        IShip CreateSubmarine(int x, int y);
        IShip CreateDestroyer(int x, int y);
    }


}
