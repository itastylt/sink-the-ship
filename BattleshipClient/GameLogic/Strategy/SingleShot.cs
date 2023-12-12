using BattleshipClient.GameLogic.Visitor;

namespace BattleshipClient.GameLogic.Strategy
{
    public class SingleShot : ICannonStrategy
    {
        public void Fire(Player opponent, int x, int y, int flag)
        {
            Console.WriteLine("Firing with Single Shot");

            ShipsBoard opponent_board = opponent.GetShipsBoard();
            //opponent_board.PrintBoard();

            if (flag != 1)
            {
                if (opponent_board.Board[y, x] >= 1)
                {
                    Console.WriteLine("Hit a ship");
                    opponent_board.Board[y, x] = -(opponent_board.Board[y, x]);
                }

                else if (opponent_board.Board[y, x] == 0)
                {
                    Console.WriteLine("Missed a shot");
                    opponent_board.Board[y, x] = -99;
                }
            }
        }
    }
}
