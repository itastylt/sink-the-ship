public class RoundIterator
{
    private RoundAggregate RoundAggregate;
    private int Pointer;

    public RoundIterator(RoundAggregate roundAggregate)
    {
        this.Pointer = 0;
        this.RoundAggregate = roundAggregate;
    }


    public Round Current ()
    {
        if (this.Pointer > this.RoundAggregate.GetMaxRounds())
            throw new Exception("Invalid round pointer");

        return this.RoundAggregate.GetRound(this.Pointer);
    }

    public Round First ()
    {
        this.Pointer = 0;
        return this.Current();
    }

    public bool IsDone()
    {
        return this.Pointer == this.RoundAggregate.GetMaxRounds() + 1;
    }

    public bool HasNext ()
    {
        return this.RoundAggregate.GetRound(this.Pointer + 1) != null;
    }

    public Round Next ()
    {
        if(!this.IsDone())
        {
            this.Pointer++;
        }
        return this.Current();
    }

    public int GetPointerLocation()
    {
        return this.Pointer;
    }
}
