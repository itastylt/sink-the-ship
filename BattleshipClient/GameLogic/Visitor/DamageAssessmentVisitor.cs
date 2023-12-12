using BattleshipClient.GameLogic.Composite;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Strategy.Decorator;
using BattleshipClient.GameLogic.Strategy;

namespace BattleshipClient.GameLogic.Visitor
{
    public class DamageAssessmentVisitor : IShipVisitor
    {
        private readonly ShipsBoard board;
        public int totalDamage { get; private set; } = 0;
        public DamageAssessmentVisitor(ShipsBoard board)
        {
            this.board = board;
        }
        public void VisitShip(IShip ship)
        {
            int damage = CalculateDamage(ship);
            totalDamage += damage;
        }

        public void VisitShipGroup(ShipGroup shipGroup)
        {
            foreach (var ship in shipGroup.GetShips())
            {
                ship.Accept(this);
            }
        }

        private int CalculateDamage(IShip ship)
        {
            int damage = 0;
            for (int i = 0; i < ship.Size; i++)
            {
                int x = ship.X + i;

                if (x < 0 || x >= board.Board.GetLength(1) || ship.Y < 0 || ship.Y >= board.Board.GetLength(0))
                {
                    continue;
                }

                if (board.Board[ship.Y, x] < 0) // A negative value indicates a hit
                {
                    damage++;
                }
            }
            return damage;
        }
    }
}
