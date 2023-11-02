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
}
