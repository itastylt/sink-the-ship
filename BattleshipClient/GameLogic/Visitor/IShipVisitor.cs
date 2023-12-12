using BattleshipClient.GameLogic.Composite;
using BattleshipClient.GameLogic.Factory;

namespace BattleshipClient.GameLogic.Visitor
{
    public interface IShipVisitor
    {
        void VisitShip(IShip ship);
        void VisitShipGroup(ShipGroup shipGroup);
    }
}