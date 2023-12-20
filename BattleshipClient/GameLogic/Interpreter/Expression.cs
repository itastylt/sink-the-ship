
using BattleshipClient.GameLogic.Invokers;

public abstract class Expression
{
    public void Interpret(Context context)
    {
        if (context == null) throw new ArgumentNullException("Context cannot be null");
        else if (context.GetInput().Length == 0) {
            return;
        }
        
        else if(context.GetInput().StartsWith("Random")) {

            string name = context.GetInput().Split(' ')[1];
            
            if(name == null || name == " ")
            {
                context.SetOutput("Usage: Random <name>");
            }
            else
            {
                this.RandomCreate(name);
                context.SetOutput(string.Format("Created random player {0}", name));
            }
        }
        

    }
    public abstract void RandomCreate(string name);
 }
