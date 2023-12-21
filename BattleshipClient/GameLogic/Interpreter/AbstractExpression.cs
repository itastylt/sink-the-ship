public abstract class AbstractExpression
{
    public  string Name;
    public  ShipHub Hub;
    public AbstractExpression(ShipHub hub, string name)
    {
        this.Hub = hub;
        this.Name = name;
    }

    // Terminal Expressions
    public abstract void Interpret(Context context);

    // NonTerminal Expressions
    public abstract string Interpret(Context context, int index);

}

