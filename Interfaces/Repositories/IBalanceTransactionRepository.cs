using Entities;
using Entities.BalanceTransactions;

namespace Interfaces.Repositories;

public interface IBalanceTransactionRepository
{
    Task InsertAsync(BalanceTransaction balanceTransaction, CancellationToken cancellationToken = default);
}