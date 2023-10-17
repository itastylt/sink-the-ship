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
    public int[,] Board { get; set; }

    public ShipsBoard()
    {
        this.Board = new int[10, 10];
    }

    public void PlaceShip(PlacedShip ship)
    {
        for (int i = ship.Y; i <= ship.Y; i++)
        {
            for (int j = ship.X; j <= ship.X + ship.Size - 1; j++)
            {
                Board[i, j] = (int)(Ship)Enum.Parse(typeof(Ship), ship.Type);
            }
        }
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
