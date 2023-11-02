using System.Security.AccessControl;

/// <summary>
/// Singleton class
/// Used for storing object properties in private fashion, only exposing static methods for gathering and manipulating data.
/// </summary>
public class ShipPlayers
{
    // Create a list of players. List should contain two values, because there's only two players at one board at the time.  
    private static List<Player> ShipPlayersList = new List<Player>();

    // Define a lock, for locking instance. Relevant, when instance is used parallel code.
    private static object _lock = new object();

    // Define private object instance of singleton and contructor 
    private static ShipPlayers _instance;
    private ShipPlayers() { }

    // Method for getting object instance
    public static ShipPlayers Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ShipPlayers() { };
                    }
                }
            }
            return _instance;
        }
    }

    public static void UpdatePlayer(string updateName, Player update)
    {
        var result = (from pred in ShipPlayersList where pred.Equals(updateName) select pred) ?? throw new InvalidOperationException("Player not found in UpdatePlayer method");
        result.First().Name = update.Name;
        result.First().SetSelectedShip(update.GetSelectedShip());
        result.First().SetShipsBoard(update.GetShipsBoard());
        result.First().SetState(update.GetState());

        foreach (Player player in ShipPlayersList)
        {
            if (player.GetSelectedShip() == null)
            {
                player.SetSelectedShip(1);
            }
        }
    }

    public static void RemovePlayer(string playerName)
    {
        ShipPlayersList.RemoveAll(x => x.Name == playerName);
    }

    public static List<Player> AddPlayer(Player player)
    {
        if (!ShipPlayersList.Contains(player))
        {
            ShipPlayersList.Add(player);
        }

        return ShipPlayersList;
    }

    public static Player GetPlayer(string playerName)
    {
        if (ShipPlayersList.Count != 0)
        {
            foreach (Player player in ShipPlayersList)
            {
                if (player.Name == playerName)
                {
                    return player;
                }
            }
            throw new InvalidOperationException("Player not found in getPlayer method");
        }
        else
        {
            throw new InvalidOperationException("Player list is empty!");
        }
    }

    public static Player GetPlayerOpponent(string playerName) //Cia su ta ideja, jog sarase yra tik 2 zaidejai viename sarase. -Simonas
    {
        if (ShipPlayersList.Count != 0)
        {
            foreach (Player player in ShipPlayersList)
            {
                if (player.Name != playerName)
                {
                    return player;
                }
            }
            throw new InvalidOperationException("Player not found in GetPlayerOpponent method");
        }
        else
        {
            throw new InvalidOperationException("Player list is empty!");
        }
    }
}