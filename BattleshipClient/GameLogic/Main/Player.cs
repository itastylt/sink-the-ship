﻿
using BattleshipClient.GameLogic.Adapter;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Mediator;
/// <summary>
/// Player's class
/// </summary>
public class Player : Collegue, IObserver, ICloneable
{
    public string Name { get; set; }
    private ShipsBoard ShipsBoard { get; set; }

    private bool State = false;

    private IShip SelectedShip { get; set; }
    
    private bool CanClone = true;

    private IShip ClonedShip { get; set; }

    private List<int> lastShot { get; set; }

    private int lastShotType { get; set; }

    private int Round = 0;

    private bool GameEnded = false;

    protected MediatorImpl m;

    public bool GetEnd()
    {
        return this.GameEnded;
    }

    public void SetEnd(bool end)
    {
        this.GameEnded = end;
    }

    public bool GetClonePowerup()
    {
        return this.CanClone;
    }

    public void DisableClonePowerup()
    {
        this.CanClone = false;
    }

    public void setLastShot(List<int> last)
    { 
        this.lastShot = last;
    }
    public void setLastShotType(int type)
    { 
        this.lastShotType = type;
    }
    public List<int> getLastShot()
    { 
        return lastShot;
    }

    public int getLastShotType()
    {
        return lastShotType;
    }

    public void setClone(IShip clone)
    { 
        ClonedShip = clone;
    }

    public IShip getClone()
    {
        return ClonedShip;
    }

    public void SetSelectedShip(int cannonNumber)
    {
        SelectedShip = this.ShipsBoard.getShip(cannonNumber);
    }

    public IShip GetSelectedShip()
    {
        if (SelectedShip == null)
        {
            SelectedShip = this.ShipsBoard.getShip(1);
        }
        return SelectedShip;
    }
    public void SetSelectedShip(IShip shipObject)
    {
        SelectedShip = shipObject;
    }
    public void CleanSelectedShip()
    {
        SelectedShip = null;
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

    public Player(MediatorImpl m,string name) : base(m, name){ 
        this.Name = name;
        this.m = m;
    }

    public bool Equals(string name)
    {
        return this.Name.Equals(name);
    }

    public object Clone()
    {
        return this;
    }

    public override CollegueType getType()
    {
        throw new NotImplementedException();
    }

    public override void sendMessage(string msg)
    {
        throw new NotImplementedException();
    }

    public override void receiveMessage(string msg)
    {
        return;
    }
}
