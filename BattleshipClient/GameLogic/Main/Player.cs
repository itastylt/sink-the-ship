
/// <summary>
/// Player's class
/// </summary>
public class Player : IObserver
{
    public string Name { get; set; }
    private ShipsBoard ShipsBoard { get; set; }

    private bool State = false;

    private PlacedShip SelectedShip { get; set; }

    public void SetSelectedShip(int cannonNumber)
    {
        SelectedShip = this.ShipsBoard.getShip(cannonNumber);
    }
    public PlacedShip GetSelectedShip()
    {
        if (SelectedShip == null)
        {
            SelectedShip = this.ShipsBoard.getShip(1);
        }
        return SelectedShip;
    }

    public bool GetState()
    {
        return this.State;
    }

    public void SetState(bool state)
    {
        this.State = state;
    }

    public ShipsBoard GetShipsBoard()
    {
        return this.ShipsBoard;
    }

    public void SetShipsBoard(ShipsBoard shipsBoard)
    {
        this.ShipsBoard = shipsBoard;
    }

    public Player(string name) { 
        this.Name = name; 
    }

    public bool Equals(string name)
    {
        return this.Name.Equals(name);
    }

}
