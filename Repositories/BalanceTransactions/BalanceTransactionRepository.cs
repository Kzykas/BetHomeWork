using Entities;
using Entities.BalanceTransactions;
using Interfaces.Repositories;
using Repositories.DbContexts;

namespace Repositories.BalanceTransactions;

public class BalanceTransactionRepository : IBalanceTransactionRepository
{
    private readonly IBetDbContext _betDbContext;

    public BalanceTransactionRepository(IBetDbContext betDbContext)
    {
        _betDbContext = betDbContext;
    }

    public async Task InsertAsync(BalanceTransaction balanceTransaction, CancellationToken cancellationToken = default)
    {
        await _betDbContext.BalanceTransactions.AddAsync(balanceTransaction, cancellationToken);

        await _betDbContext.SaveChangesAsync(cancellationToken);
    }
}