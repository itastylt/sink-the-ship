using BattleshipClient.GameLogic.Factory;
using System;

public enum Ship
{
    Boat = 1,
    Lavantier = 2,
    Submarine = 3,
    Destroyer = 4
}

public class ShipsBoard
{
    List<IShip> allShips { get; set; }
    public int[,] Board { get; set; }

    public ShipsBoard()
    {
        this.Board = new int[10, 10];
        allShips = new List<IShip>();
    }
    public bool isAValidTarget(int x, int y)
    {
        if (x >= 10 || y >= 10 || x < 0 || y < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
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
