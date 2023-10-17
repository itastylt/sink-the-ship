
using BattleshipClient.GameLogic.Strategy;

public class PlacedShip
{
    public string Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Size {  get; set; } 
    public int Angle { get; set; }

    //Strategy pattern
    public ICannonStrategy Cannon { get; set; }
    public void FireWeapon()
    {
        Cannon.Fire(X,Y);
    }

}

