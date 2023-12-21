
using BattleshipClient.GameLogic.Invokers;

public class ExpressionParser : AbstractExpression
{
    public ExpressionParser(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override void Interpret(Context context)
    {
        if (context == null) throw new ArgumentNullException("Context cannot be null");
        else if (context.GetInput().Length == 0)
        {
            return;
        }

        else if (context.GetInput().StartsWith("FireShot"))
        {
            FireShotExpression fireShotExpression = new FireShotExpression(this.Hub, this.Name);
            fireShotExpression.Interpret(context);
        }
        else if (context.GetInput().StartsWith("Clone"))
        {
            CloneExpression cloneExpression = new CloneExpression(this.Hub, this.Name);
            cloneExpression.Interpret(context);
        }
        else if (context.GetInput().StartsWith("SelectShip"))
        {
            SelectShipExpression selectShipExpression = new SelectShipExpression(this.Hub, this.Name);
            selectShipExpression.Interpret(context);
        }
        else if (context.GetInput().StartsWith("UnselectShip"))
        {
            UnselectShipExpression unselectShipExpression = new UnselectShipExpression(this.Hub, this.Name);
            unselectShipExpression.Interpret(context);
        }
        else
        {
            throw new Exception("Invalid Command");
        }


    }

    public override string Interpret(Context context, int index)
    {
        throw new NotImplementedException();
    }
}
