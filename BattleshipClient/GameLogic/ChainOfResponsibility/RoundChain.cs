public abstract class RoundChain
{
    public abstract void ExecuteRound(string name1, string name2);

    public abstract RoundChain SetNextChain(); 
}
