namespace BattleshipClient.GameLogic.Strategy
{
    public class SingleShot : ICannonStrategy
    {
        public void Fire(Player opponent, int x, int y)
        {
            Console.WriteLine("Firing with Single Shot");

            ShipsBoard opponent_board = opponent.GetShipsBoard();
            opponent_board.PrintBoard();

            if (opponent_board.Board[x, y] >= 1)
            {
                Console.WriteLine("Hit a ship");
                opponent_board.Board[x, y] = -(opponent_board.Board[x, y]);
            }
            else if (opponent_board.Board[x, y] == 0)
            {
                Console.WriteLine("Missed a shot");
                opponent_board.Board[x, y] = -99;
            }
            else if (opponent_board.Board[x, y] < 0)
            {
                Console.WriteLine("Hit already hit target");
            }
        }
    }
}
