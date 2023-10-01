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
    public string Name { get; set; }

    public ShipsBoard(string name)
    {
        this.Board = new int[10, 10];
        this.Name = name;
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
