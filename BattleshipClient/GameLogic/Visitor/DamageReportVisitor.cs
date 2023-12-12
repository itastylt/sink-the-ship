using BattleshipClient.GameLogic.Composite;
using BattleshipClient.GameLogic.Factory;

namespace BattleshipClient.GameLogic.Visitor
{
    public class DamageReportVisitor : IShipVisitor
    {
        public void VisitShip(IShip ship)
        {
            // Assess damage on an individual ship
        }

        public void VisitShipGroup(ShipGroup shipGroup)
        {
            // Assess damage on each ship in the group
            foreach (var ship in shipGroup.GetShips())
            {
                //ship.Accept(this);
            }
        }
    }
}
