using BattleshipClient.GameLogic.Factory;

namespace BattleshipClient.GameLogic.Main
{
    public class ShipBoardsMemento
    {
        private readonly int[,] board;
        private readonly List<IShip> allShips;

        public ShipBoardsMemento(int[,] board, List<IShip> allShips)
        {
            this.board = (int[,])board.Clone();  // Deep copy the board
            this.allShips = new List<IShip>(allShips);  // Deep copy the list of ships
        }

        public int[,] GetBoard() => (int[,])board.Clone();
        public List<IShip> GetAllShips() => new List<IShip>(allShips);
    }
}