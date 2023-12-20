using BattleshipClient.GameLogic.Composite;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Template;
using DotLiquid.Util;
using System;
using System.Runtime.InteropServices;

public enum Ship
{
    Boat = 1,
    Lavantier = 2,
    Submarine = 3,
    Destroyer = 4
}

public class ShipsBoard
{
    ShipGroup shipGroup { get; set; }
    List<IShip> allShips { get; set; }
    public int[,] Board { get; set; }

    public ShipsBoard()
    {
        GameSettings settings = GameSettings.GetInstance();
        this.Board = new int[settings.BoardSize, settings.BoardSize];
        allShips = new List<IShip>();
        shipGroup = new ShipGroup();
    }


    public bool ContainsGreaterThan(int value)
    {
        for(int i = 0; i < Board.GetLength(0); i++)
        {
            for(int j = 0; j < this.Board.GetLength(1); j++)
            {
                if (this.Board[i, j] > value)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public bool isAValidTarget(int x, int y)
    {
        GameSettings settings = GameSettings.GetInstance();
        int boardSize = settings.BoardSize;

        if (x >= boardSize || y >= boardSize || x < 0 || y < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool BoardEnd()
    {
        bool noBiggerThanZero = true;
        for(int i = 0; i <this.Board.GetLength(0); i++)
        {
            for(int j = 0; j < this.Board.GetLength(1); j++)
            {
                Console.Write(this.Board[i, j]);
                if (this.Board[i,j] > 0)
                {
                    noBiggerThanZero = false;
                    i = this.Board.GetLength(0) - 1;
                }
            }
        }

        return noBiggerThanZero;
    }

    public IShip getShip(int cannonNumber)
    {
        foreach (IShip ship in allShips)
        {
            if (cannonNumber == (int)(Ship)Enum.Parse(typeof(Ship), ship.Type))
            {
                return ship;
            }
        }
        throw new InvalidOperationException("Ship not found in getShip method");
    }
    public ShipGroup getShipGroup()
    {
        return shipGroup;
    }

    public int[] GetAvailableCoordinate()
    {
        int[] coordinates = new int[2];
        int maxX = this.Board.GetLength(1);
        int maxY = this.Board.GetLength(0);

        Random random = new Random();
        int luckyX = random.Next(maxX);
        int luckyY = random.Next(maxY);
        int tile = this.Board[luckyY, luckyX];

        while (tile != 0 && tile != -99)
        {
            Console.WriteLine(string.Format("y: {0} ; x : {1}", luckyY, luckyX));
            luckyX = random.Next(maxX);
            luckyY = random.Next(maxY);
            tile = this.Board[luckyY, luckyX];
        }

        coordinates[0] = luckyY;
        coordinates[1] = luckyX;
        return coordinates;
    }
    public void PlaceLandMines()
    {
        GameSettings settings = GameSettings.GetInstance();
        int mineCount = settings.MineCount;

        for (int i = 0; i < mineCount; i++)
        {
            int[] coords = GetAvailableCoordinate();
            Board[coords[1], coords[0]] = 5;
        }
    }

    public void PlaceIslands()
    {
        GameSettings settings = GameSettings.GetInstance();
        int islandCount = settings.IslandCount;

        for (int i = 0; i < islandCount; i++)
        {
            int[] coords = GetAvailableCoordinate();
            Board[coords[1], coords[0]] = 6;
        }
    }

    public void PlaceShip(IShip ship)
    {
        for (int i = ship.Y; i <= ship.Y; i++)
        {
            for (int j = ship.X; j <= ship.X + ship.Size - 1; j++)
            {
                Board[i, j] = (int)(Ship)Enum.Parse(typeof(Ship), ship.Type);
            }
        }
        allShips.Add(ship);
        shipGroup.Add(ship);
    }

    public void UnPlaceShip(IShip ship)
    {
        for (int i = ship.Y; i <= ship.Y; i++)
        {
            for (int j = ship.X; j <= ship.X + ship.Size - 1; j++)
            {
                Board[i, j] = 0;
            }
        }
        allShips.RemoveAll(x => x == ship);
        shipGroup.Remove(ship);
    }

    override public string ToString()
    {
        string boardAsString = "[";
        for (int i = 0; i < this.Board.GetLength(0); i++)
        {
            boardAsString += "[";
            for(int j = 0; j < this.Board.GetLength(1); j++ )
            {
                if(j < this.Board.GetLength(1)-1)
                {
                    boardAsString += this.Board[i, j].ToString() + ',';
                } else
                {
                    boardAsString += this.Board[i, j];
                }

            }
            if(i < this.Board.GetLength(0) - 1)
            {
                boardAsString += "],";
            } else
            {
                boardAsString += ']';
            }


        }
        boardAsString += "]";


        return boardAsString;
    }
    public void PrintBoard()
    {
        for(int i = 0; i  < this.Board.GetLength(0); i++)
        {
            for(int j = 0; j < this.Board.GetLength(1); j++) {
                Console.Write(this.Board[i,j]);
            }
            Console.WriteLine();
        }
    }

}
