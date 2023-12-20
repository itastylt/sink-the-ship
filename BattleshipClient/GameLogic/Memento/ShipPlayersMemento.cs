namespace BattleshipClient.GameLogic.Memento

{
    public class ShipPlayersMemento
    {
        
        
        public List<Player> Players { get; private set; }

            public ShipPlayersMemento(List<Player> players)
            {
                Players = new List<Player>((IEnumerable<Player>)players.Select(player => player.Clone())); // Cloning players to create a deep copy
            }
        
    }
}
