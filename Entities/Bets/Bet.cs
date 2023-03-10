using Entities.BetSelections;

namespace Entities.Bets;

public class Bet
{
    public int Id { get; set; }
    public decimal StakeAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<BetSelection> BetSelections { get; set; }
}