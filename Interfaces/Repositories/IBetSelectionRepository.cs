using Entities;
using Entities.BetSelections;

namespace Interfaces.Repositories;

public interface IBetSelectionRepository
{
    Task InsertManyAsync(IList<BetSelection> betSelections, CancellationToken cancellationToken = default);
}