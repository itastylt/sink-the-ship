﻿using BattleshipClient.GameLogic.Invokers;
using BattleshipClient.GameLogic.Memento;
using BattleshipClient.GameLogic.State;
using BattleshipClient.GameLogic.State.State_values;
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
    private static int Count;
    public static RoundChain _roundChain;

    private static Stack<ShipPlayersMemento> mementoStack = new Stack<ShipPlayersMemento>();

    private static Game gameState = new Game();
    private static Game lastGame = gameState;


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

    public static void UpdateState()
    {
        lastGame = gameState;
        gameState.getState().getNextState(gameState);
    }

    public static void ResumeGameState()
    {
        gameState = new Game();
    }

    public static void SetPause()
    {
        gameState = lastGame;
    }

    public static void SetWaiting()
    {
        GameState Waiting = new WaitingState();
        gameState.setState(Waiting);
    }

    public static void endGameState()
    {
        GameState endState = new GameEndedState();
        gameState.setState(endState);
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
        SaveState();
    }

    public static List<Player> GetPlayers()
    {
        return ShipPlayersList;
    }
    private static void SaveState()
    {
        var memento = new ShipPlayersMemento(ShipPlayersList);
        mementoStack.Push(memento);
    }

    public static void Clear()
    {
        ShipPlayersList = new List<Player>();
        SaveState(); // Save state after clearing the players
    }

    public static Player EndPlayer()
    {
        Player gameEnd = null;
        foreach (Player player in ShipPlayersList)
        {
            if(player.GetShipsBoard().BoardEnd())
            {
                gameEnd = player;
                break;
            }
        }
        return gameEnd;
    }


    public static void RemovePlayer(string playerName)
    {
        Count--;
        ShipPlayersList.RemoveAll(x => x.Name == playerName);
        SaveState(); // Save state after removing a player
    }

    public static List<Player> AddPlayer(Player player)
    {
        if (!ShipPlayersList.Contains(player))
        {
            ShipPlayersList.Add(player);
            SaveState(); // Save state after adding a player
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
            throw  new InvalidOperationException("Player list is empty!");
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
    public static int PlayerCount() { return ShipPlayersList.Count; }

    public static void RestoreState()
    {
        if (mementoStack.Count > 0)
        {
            var memento = mementoStack.Pop();
            ShipPlayersList = memento.Players;
        }
    }

    public static bool IncreaseRoundState() {
        Count++;
        return Count % 2 == 0;
    }
    public static void UpdateCurrentRoundChain()
    {
        if (_roundChain == null)
            _roundChain = new FirstRoundChain(new RoundAggregate(ShipPlayersList[0], ShipPlayersList[1]).GetRoundIterator());
        else
            _roundChain.SetNextChain();
        
        _roundChain.ExecuteRound(ShipPlayersList[0].Name, ShipPlayersList[1].Name);
        SaveState();
    }
}