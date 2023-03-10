using Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Operations.Operations.Bet;
using Operations.ValidationServices.MaxWinAmountValidationServices;
using Operations.ValidationServices.PlayerValidationServices;
using Operations.ValidationServices.SelectionValidationServices;
using Repositories.BalanceTransactions;
using Repositories.Bets;
using Repositories.BetSelections;
using Repositories.DbContexts;
using Repositories.Players;

namespace IoC;

public static class Registry
{
    public static void AddRegistries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<IBetDbContext, BetDbContext>();
        
        serviceCollection.AddScoped<IPlayerRepository, PlayerRepository>();
        serviceCollection.AddScoped<IBalanceTransactionRepository, BalanceTransactionRepository>();
        serviceCollection.AddScoped<IBetSelectionRepository, BetSelectionRepository>();
        serviceCollection.AddScoped<IBetRepository, BetRepository>();

        serviceCollection.AddScoped<IPlayerValidationService, PlayerValidationService>();
        serviceCollection.AddScoped<ISelectionValidationService, SelectionValidationService>();
        serviceCollection.AddScoped<IWinAmountValidationService, WinAmountValidationService>();
        
        serviceCollection.AddScoped<IMakeBet, MakeBet>();
    }
}