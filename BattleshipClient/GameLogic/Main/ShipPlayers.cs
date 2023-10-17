using System.Security.AccessControl;

public static class ShipPlayers
{
    private static List<Player> ShipPlayersList;

    public static List<Player> AddPlayer(Player player)
    {
        if (ShipPlayersList == null)
        {
            ShipPlayersList = new List<Player>
            {
                player
            };
        } 
        else
        {
            if (!ShipPlayersList.Contains(player))
            {
                ShipPlayersList.Add(player);
            }
        }

        return ShipPlayersList;
    }

    public static Player GetPlayer(string playerName)
    {
        if(ShipPlayersList == null)
        {
            throw new InvalidOperationException("ShipPlayersList is not initialized");
        }

        foreach (Player player in ShipPlayersList)
        {
            if (player.Name == playerName)
            {
                return player;
            }
        }

        throw new InvalidOperationException("Player not found in getPlayer method");
    }

    public static Player GetPlayerOpponent(string playerName) //Cia su ta ideja, jog sarase yra tik 2 zaidejai viename sarase. -Simonas
    {
        if (ShipPlayersList == null)
        {
            throw new InvalidOperationException("ShipPlayersList is not initialized");
        }

        foreach (Player player in ShipPlayersList)
        {
            if (player.Name != playerName)
            {
                return player;
            }
        }
        throw new InvalidOperationException("Player not found in getPlayerOpponent method");
    }
}