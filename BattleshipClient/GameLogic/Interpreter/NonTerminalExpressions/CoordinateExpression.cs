
public class CoordinateExpression : AbstractExpression
{
    public CoordinateExpression(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override string Interpret(Context context, int index)
    {
        return context.GetInput().Split(' ')[index];
    }

    public override void Interpret(Context context)
    {
        throw new NotImplementedException();
    }
}

