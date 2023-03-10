using Entities.BalanceTransactions;

namespace Entities.Players;

public class Player
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public IList<BalanceTransaction> BalanceTransactions { get; set; } = new List<BalanceTransaction>();
}