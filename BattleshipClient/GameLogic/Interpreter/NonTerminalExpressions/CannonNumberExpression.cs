public class CannonNumberExpression : AbstractExpression
{
    public CannonNumberExpression(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override void Interpret(Context context)
    {
        
    }

    public override string Interpret(Context context, int index)
    {
        return context.GetInput().Split(' ')[index];
    }
}
