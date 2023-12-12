namespace BattleshipClient.GameLogic.Strategy
{
    public class VerticalShot : ICannonStrategy
    {
        public void Fire(Player opponent, int x, int y, int flag)
        {
            Console.WriteLine("Firing with Vertical Shot");

            ShipsBoard opponent_board = opponent.GetShipsBoard();
            //opponent_board.PrintBoard();
            
            if (opponent_board.isAValidTarget(y,x)) {
                var tile = opponent_board.Board[y, x];
                if (tile > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y, x] = -(opponent_board.Board[y, x]);
                }
                else if (flag == 1)
                {
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

            if (opponent_board.isAValidTarget(y + 1, x))
            {
                var nextTile = opponent_board.Board[y + 1, x];
                if (nextTile > 0)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y + 1, x] = -(opponent_board.Board[y + 1, x]);
                }
                else if (flag == 1)
                {
                    if (opponent_board.Board[y + 1 , x] < 0 && opponent_board.Board[y + 1, x] != -99)
                    {
                        opponent_board.Board[y+1, x] = -(opponent_board.Board[y + 1, x]);
                    }
                    else
                    {
                        opponent_board.Board[y+1, x] = 0;
                    }
                }
                else if (nextTile == 0)
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y + 1, x] = -99;
                }
            }
        }
    }
}
