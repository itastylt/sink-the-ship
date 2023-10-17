
public interface IObserver
{
    public bool GetState();
    public void SetState(bool state);
    public ShipsBoard GetShipsBoard();
    public void SetShipsBoard(ShipsBoard shipsBoard);
}
