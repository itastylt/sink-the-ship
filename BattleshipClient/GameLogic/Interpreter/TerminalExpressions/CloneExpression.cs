
using BattleshipClient.GameLogic.Invokers;

public class CloneExpression : AbstractExpression
{
    public CloneExpression(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override void Interpret(Context context)
    {
        CloneShip clone = new CloneShip(this.Name, this.Hub);
        clone.execute();
    }

    public override string Interpret(Context context, int index)
    {
        throw new NotImplementedException();
    }
}