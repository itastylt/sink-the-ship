using BattleshipClient.GameLogic.Invokers;

public class SelectShipExpression : AbstractExpression
{
    public SelectShipExpression(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override void Interpret(Context context)
    {
        string shipID = new CannonNumberExpression(this.Hub, this.Name).Interpret(context, 1);
        SelectWeapon selectWeapon = new SelectWeapon(shipID, this.Name, this.Hub);
        selectWeapon.execute();
    }

    public override string Interpret(Context context, int index)
    {
        throw new NotImplementedException();
    }
}
