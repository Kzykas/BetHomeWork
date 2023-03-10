using Entities.Players;
using Interfaces.Repositories;
using Repositories.DbContexts;

namespace Repositories.Players;

public class PlayerRepository : IPlayerRepository
{
    private readonly IBetDbContext _betDbContext;

    public PlayerRepository(IBetDbContext betDbContext)
    {
        _betDbContext = betDbContext;
    }

    public async Task UpsertAsync(Player player, CancellationToken cancellationToken = default)
    {
        _betDbContext.Players.Update(player);

        await _betDbContext.SaveChangesAsync(cancellationToken);
    }

    public Player? Get(int playerId)
    {
        var player = _betDbContext.Players.FirstOrDefault(e => e.Id == playerId);

        return player;
    }
}