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
}