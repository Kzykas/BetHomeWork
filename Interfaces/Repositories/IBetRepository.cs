using Entities;
using Entities.Bets;

namespace Interfaces.Repositories;

public interface IBetRepository
{
    Task InsertAsync(Bet bet, CancellationToken cancellationToken = default);
}