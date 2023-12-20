namespace BattleshipClient.GameLogic.Memento

{
    public class ShipPlayersMemento
    {
        
        
        public List<Player> Players { get; private set; }

            public ShipPlayersMemento(List<Player> players)
            {
                Players = new List<Player> (players.Select(player => (Player)player.Clone())); // Cloning players to create a deep copy
            }
        
    }
}
