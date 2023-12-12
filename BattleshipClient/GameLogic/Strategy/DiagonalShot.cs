namespace BattleshipClient.GameLogic.Strategy
{
    public class DiagonalShot : ICannonStrategy
    {
        public void Fire(Player opponent, int x, int y, int flag)
        {
            Console.WriteLine("Firing with Diagonal Shot");

            ShipsBoard opponent_board = opponent.GetShipsBoard();

            if (opponent_board.isAValidTarget(y, x))
            {
                var tile = opponent_board.Board[y, x];
                if (tile > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y, x] = -(opponent_board.Board[y, x]);
                } else if (flag == 1) {
                    if (opponent_board.Board[y, x] < 0 && opponent_board.Board[y, x] != -99)
                    {
                        opponent_board.Board[y, x] = -(opponent_board.Board[y, x]);
                    }
                    else
                    {
                        opponent_board.Board[y, x] = 0;
                    }
                }
                else if (tile == 0)
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y, x] = -99;
                }
            }

            if (opponent_board.isAValidTarget(y + 1, x + 1))
            {
                var nextTile = opponent_board.Board[y+1, x +1];
                if (nextTile > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y + 1, x + 1] = -(opponent_board.Board[y + 1, x + 1]);
                }
                else if (flag == 1)
                {
                    if (opponent_board.Board[y + 1, x + 1] < 0 && opponent_board.Board[y + 1, x +1] != -99)
                    {
                        opponent_board.Board[y + 1, x + 1] = -(opponent_board.Board[y + 1, x + 1]);
                    }
                    else
                    {
                        opponent_board.Board[y + 1, x + 1] = 0;
                    }
                }
                else if (nextTile == 0)
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y + 1, x + 1] = -99;
                }
            }
        }
    }
}
