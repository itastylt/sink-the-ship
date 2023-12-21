
using BattleshipClient.GameLogic.Invokers;

public class FireShotExpression : AbstractExpression
{
    public FireShotExpression(ShipHub hub, string name) : base(hub, name)
    {
    }

    public override void Interpret(Context context)
    {
        string x = new CoordinateExpression(this.Hub, this.Name).Interpret(context, 1);
        string y = new CoordinateExpression(this.Hub, this.Name).Interpret(context, 2);

        string args = "asd;" + x + ";" +  y;

        FireWeapon fireWeapon = new FireWeapon(x, args, this.Name, this.Hub);
        fireWeapon.execute();
    }

    public override string Interpret(Context context, int index)
    {
        throw new NotImplementedException();
    }
}

