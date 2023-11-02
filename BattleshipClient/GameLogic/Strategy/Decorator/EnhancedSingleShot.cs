namespace BattleshipClient.GameLogic.Strategy.Decorator
{
    public class EnhancedSingleShot : ICannonStrategy
    {
        private class Coordinates
        {
            int x { get; set; }
            int y { get; set; }
            public Coordinates(int y, int x)
            {
                this.x = x;
                this.y = y;
            }
            public int getX()
            {
                return x;
            }
            public int getY()
            {
                return y;
            }
        }

        private ICannonStrategy baseStrategy;

        public EnhancedSingleShot(ICannonStrategy baseStrategy)
        {
            this.baseStrategy = baseStrategy;
        }
        public void Fire(Player opponent, int x, int y, int flag)
        {
            Console.WriteLine("Firing with Enhanced Single Shot");

            Coordinates Coordinates = HomingShot(opponent, x, y);  

            baseStrategy.Fire(opponent, Coordinates.getX(), Coordinates.getY(), flag);
        }
        private Coordinates HomingShot(Player opponent, int x, int y) //x=2, y=2
        {
            ShipsBoard opponent_board = opponent.GetShipsBoard();

            if (opponent_board.isAValidTarget(x, y) && opponent_board.Board[y, x] > 0)
            {
                //Console.WriteLine("The target it aimed at is a destructable ship");
                return new Coordinates(y, x);
            }

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int newY = j + y;
                    int newX = i + x;
                    if (opponent_board.isAValidTarget(newY, newX)) 
                    {
                        if (opponent_board.Board[newY, newX] > 0)
                        {
                            //Console.WriteLine("Retargeted ship");
                            opponent.setLastShot(new List<int>() { newX, newY });
                            return new Coordinates(newY, newX);
                        }
                    }
                }
            }
            //Console.WriteLine("Didn't detect any valid ship around");
            return new Coordinates(y, x);
        }
    }
}
