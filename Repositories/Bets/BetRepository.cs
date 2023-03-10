using Entities;
using Entities.Bets;
using Interfaces.Repositories;
using Repositories.DbContexts;

namespace Repositories.Bets;

public class BetRepository : IBetRepository
{
    private readonly IBetDbContext _betDbContext;

    public BetRepository(IBetDbContext betDbContext)
    {
        _betDbContext = betDbContext;
    }

    public async Task InsertAsync(Bet bet, CancellationToken cancellationToken = default)
    {
        await _betDbContext.Bets.AddAsync(bet, cancellationToken);

        await _betDbContext.SaveChangesAsync(cancellationToken);
    }
}