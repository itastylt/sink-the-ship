using BattleshipClient.GameLogic.Invokers;

public class UnselectShipExpression : AbstractExpression
{
    public UnselectShipExpression(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override void Interpret(Context context)
    {
        SelectWeapon selectWeapon = new SelectWeapon("1", this.Name, this.Hub);
        selectWeapon.undoAsync();
    }

    public override string Interpret(Context context, int index)
    {
        throw new NotImplementedException();
    }
}
