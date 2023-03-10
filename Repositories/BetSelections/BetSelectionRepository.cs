using Entities;
using Entities.BetSelections;
using Interfaces.Repositories;
using Repositories.DbContexts;

namespace Repositories.BetSelections;

public class BetSelectionRepository : IBetSelectionRepository
{
    private readonly IBetDbContext _betDbContext;

    public BetSelectionRepository(IBetDbContext betDbContext)
    {
        _betDbContext = betDbContext;
    }

    public async Task InsertManyAsync(IList<BetSelection> betSelections, CancellationToken cancellationToken = default)
    {
        await _betDbContext.BetSelections.AddRangeAsync(betSelections, cancellationToken);

        await _betDbContext.SaveChangesAsync(cancellationToken);
    }
}