using System;
using System.Collections.Generic;

public class PlayerBuilder
{
    private ShipsBoard shipsBoard;
    private Random random;

    public PlayerBuilder(ShipsBoard shipsBoard)
    {
        this.shipsBoard = shipsBoard;
        random = new Random();
    }

    public Player CreateRandomPlayer(string name)
    {
        Player player = new Player(name);
        player.SetShipsBoard(shipsBoard);
        return player;
    }

    private string GenerateRandomPlayerName()
    {
        List<string> names = new List<string>
        {
            "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Helen", "Ivan", "Jack", "Karen", "Liam", "Mia", "Noah", "Olivia"
        };

        int randomIndex = random.Next(names.Count);
        return names[randomIndex];
    }
}
