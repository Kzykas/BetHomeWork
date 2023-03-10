using Entities;
using Entities.Players;

namespace Interfaces.Repositories;

public interface IPlayerRepository
{
    Task UpsertAsync(Player player, CancellationToken cancellationToken = default);
    Player? Get(int playerId);
}