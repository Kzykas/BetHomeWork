using Entities.Bets;

namespace Entities.BetSelections;

public class BetSelection
{
    public int Id { get; set; }
    public int SelectionId { get; set; }
    public decimal Odds { get; set; }
    public int BetId { get; set; }
    public Bet Bet { get; set; }
}