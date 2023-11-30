using BattleshipClient.GameLogic.Strategy.Decorator;
using BattleshipClient.GameLogic.Strategy;
using System;
using System.Collections.Generic;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Builder;

public class ShipBuilder
{
    private ShipsBoard board;
    private Random random = new Random();

    public ShipBuilder(ShipsBoard board)
    {
        this.board = board;
    }
    public void BuildRandomShips()
    {
        List<Ship> shipTypes = Enum.GetValues(typeof(Ship)).OfType<Ship>().ToList();

        foreach (Ship shipType in shipTypes)
        {
            int size = (int)shipType;
            ConcreteShip ship = (ConcreteShip)BuildRandomShip(shipType.ToString(), size);
            PlaceShipOnBoard(ship);
        }
    }

    public ShipsBoard GetBoard()
    {
        return board;
    }

    private IShip BuildRandomShip(string type, int size)
    {
        IShip ship = new ConcreteShip
        {
            Type = type,
            Size = size,
            Angle = 0 // Set the angle as needed
        };

        Random random = new Random();
        bool isValidPlacement = false;

        while (!isValidPlacement)
        {
            int x = random.Next(10);
            int y = random.Next(10);

            if (isAGoodPlace(size, x, y))
            {
                ship.X = x;
                ship.Y = y;
                isValidPlacement = true;

                switch (ship.Type)
                {
                    case "Boat":
                        ship.Cannon = new SingleShot(); //Applying Strategy
                        ship.Cannon = new EnhancedSingleShot(ship.Cannon); //Applying Decorator
                        this.board.PlaceShip(ship);
                        break;
                    case "Lavantier":
                        ship.Cannon = new HorizontalShot();
                        this.board.PlaceShip(ship);
                        break;
                    case "Submarine":
                        ship.Cannon = new VerticalShot();
                        this.board.PlaceShip(ship);
                        break;
                    case "Destroyer":
                        ship.Cannon = new DiagonalShot();
                        this.board.PlaceShip(ship);
                        break;
                    default:
                        throw new Exception("Invalid Ship type!");
                }
            }
        }
        return ship;
    }

    private void PlaceShipOnBoard(IShip ship)
    {
        board.PlaceShip(ship);
    }
    private bool isAGoodPlace(int sizeOfShip, int x, int y)
    {
        if (x < 0 || x + sizeOfShip > 10 || y < 0 || y >= 10)
            return false; //ship outside the boundary

        for (int i = x; i < x + sizeOfShip; i++)
            if (board.Board[y, i] != 0)
                return false; //ship overlays another ship

        return true; //ship is within board and doesn't overlay
    }


}
