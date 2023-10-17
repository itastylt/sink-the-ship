namespace BattleshipClient.GameLogic.Strategy
{
    public class VerticalShot : ICannonStrategy
    {
        public void Fire(Player opponent, int x, int y)
        {
            Console.WriteLine("Firing with Vertical Shot");

            ShipsBoard opponent_board = opponent.GetShipsBoard();
            //opponent_board.PrintBoard();
            
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

            if (opponent_board.isAValidTarget(y + 1, x))
            {
                if (opponent_board.Board[y + 1, x] > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y + 1, x] = -(opponent_board.Board[y + 1, x]);
                }
                else
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y + 1, x] = -99;
                }
            }
        }
    }
}
