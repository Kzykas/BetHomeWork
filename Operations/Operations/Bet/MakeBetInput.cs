namespace Operations.Operations.Bet;

public class MakeBetInput
{
    public int PlayerId { get; set; }
    public decimal StakeAmount { get; set; }
    public IList<Selection> Selections { get; set; }
    
    public class Selection
    {
        public int Id { get; set; }
        public decimal Odds { get; set; }
    }
}