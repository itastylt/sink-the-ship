namespace BattleshipClient.GameLogic.Strategy
{
    public class HorizontalShot : ICannonStrategy
    {
        public void Fire(Player opponent, int x, int y)
        {
            Console.WriteLine("Firing with Horizontal Shot");

            ShipsBoard opponent_board = opponent.GetShipsBoard();
            opponent_board.PrintBoard();
            
            if (opponent_board.isAValidTarget(y,x)) {
                if (opponent_board.Board[y, x] > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y, x] = -(opponent_board.Board[y, x]);
                }
                else
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y, x] = -99;
                }
            }

            if (opponent_board.isAValidTarget(y, x + 1))
            {
                if (opponent_board.Board[y, x + 1] > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y, x + 1] = -(opponent_board.Board[y, x + 1]);
                }
                else
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y, x + 1] = -99;
                }
            }
        }
    }
}
