using BattleshipClient.GameLogic.Visitor;

namespace BattleshipClient.GameLogic.Composite
{
    public class ShipGroup : IShipComponent
    {
        private List<IShipComponent> ships = new List<IShipComponent>();

        public void Add(IShipComponent ship)
        {
            ships.Add(ship);
        }
        public void Remove(IShipComponent ship)
        {
            ships.RemoveAll(x => x == ship);
        }
        public void FireWeapon(Player opponent, int x, int y, int flag)
        {
            foreach (var ship in ships)
            {
                ship.FireWeapon(opponent, x, y, flag); 
            }
        }
        public void Accept(IShipVisitor visitor)
        {
            visitor.VisitShipGroup(this);
        }
        public IEnumerable<IShipComponent> GetShips()
        {
            foreach (var ship in ships)
            {
                yield return ship;
            }
        }
    }

}
