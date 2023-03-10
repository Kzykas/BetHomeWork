using Entities;
using Entities.BalanceTransactions;
using Entities.Bets;
using Entities.BetSelections;
using Entities.Players;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DbContexts;

public interface IBetDbContext 
{
    DbSet<Player> Players { get; set; }
    DbSet<BalanceTransaction> BalanceTransactions { get; set; }
    DbSet<Bet> Bets { get; set; }
    DbSet<BetSelection> BetSelections { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}