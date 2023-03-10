using Entities;
using Entities.BalanceTransactions;
using Entities.Bets;
using Entities.BetSelections;
using Entities.Players;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DbContexts;

public class BetDbContext : DbContext, IBetDbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<BalanceTransaction> BalanceTransactions { get; set; }
    public DbSet<Bet> Bets { get; set; }
    public DbSet<BetSelection> BetSelections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=BetDb;User Id=admin;Password=admin;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Player>().ToTable(nameof(Players)).HasKey(e => e.Id);
        builder.Entity<Player>()
            .HasMany(e => e.BalanceTransactions)
            .WithOne(e => e.Player)
            .HasForeignKey(e => e.PlayerId);

        builder.Entity<BalanceTransaction>().ToTable(nameof(BalanceTransactions)).HasKey(e => e.Id);
        builder.Entity<BalanceTransaction>().Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Entity<Bet>().ToTable(nameof(Bets)).HasKey(e => e.Id);
        builder.Entity<Bet>()
            .HasMany(e => e.BetSelections)
            .WithOne(e => e.Bet)
            .HasForeignKey(e => e.BetId);
        builder.Entity<Bet>().Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Entity<BetSelection>().ToTable(nameof(BetSelections)).HasKey(e => e.Id);
        builder.Entity<BetSelection>().Property(e => e.Id).ValueGeneratedOnAdd();
    }
}