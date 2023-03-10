using Entities.Players;

namespace Entities.BalanceTransactions;

public class BalanceTransaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountBefore { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
}