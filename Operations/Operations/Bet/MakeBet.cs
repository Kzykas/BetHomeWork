using Entities.BalanceTransactions;
using Entities.BetSelections;
using Entities.Players;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Operations.Operations.Shared;
using Operations.ValidationServices;
using Operations.ValidationServices.MaxWinAmountValidationServices;
using Operations.ValidationServices.PlayerValidationServices;
using Operations.ValidationServices.SelectionValidationServices;

namespace Operations.Operations.Bet;

public class MakeBet : IMakeBet
{
    private readonly IPlayerValidationService _playerValidationService;
    private readonly ISelectionValidationService _selectionValidationService;
    private readonly IWinAmountValidationService _winAmountValidationService;

    private readonly IPlayerRepository _playerRepository;
    private readonly IBetSelectionRepository _betSelectionRepository;
    private readonly IBetRepository _betRepository;
    private readonly IBalanceTransactionRepository _balanceTransactionRepository;

    public MakeBet(IPlayerValidationService playerValidationService,
        ISelectionValidationService selectionValidationService, IWinAmountValidationService winAmountValidationService,
        IPlayerRepository playerRepository, IBetSelectionRepository betSelectionRepository,
        IBetRepository betRepository, IBalanceTransactionRepository balanceTransactionRepository)
    {
        _playerValidationService = playerValidationService;
        _selectionValidationService = selectionValidationService;
        _winAmountValidationService = winAmountValidationService;
        _playerRepository = playerRepository;
        _betSelectionRepository = betSelectionRepository;
        _betRepository = betRepository;
        _balanceTransactionRepository = balanceTransactionRepository;
    }

    public async Task<IActionResult> ExecuteAsync(MakeBetInput input, CancellationToken cancellationToken)
    {
        var player = GetPlayer(input.PlayerId);

        var validationResponse = ValidateInput(player, input);

        var odds = input.Selections.Select(e => e.Odds).ToList();
        var winAmount = CalculateWinAmount(input.StakeAmount, odds);

        var winAmountValidationResponse = _winAmountValidationService.Validate(winAmount);

        if (winAmountValidationResponse != null)
            validationResponse.Errors.Add(winAmountValidationResponse);

        if (validationResponse.Selections.Any() || validationResponse.Errors.Any())
        {
            var errorJson = JsonConvert.SerializeObject(validationResponse);

            return new BadRequestObjectResult(errorJson);
        }

        var amountBefore = player.Balance;
        player.Balance -= input.StakeAmount;

        var balanceTransaction = new BalanceTransaction
        {
            PlayerId = player.Id,
            Player = player,
            Amount = player.Balance,
            AmountBefore = amountBefore
        };
        
        var bet = new Entities.Bets.Bet
        {
            StakeAmount = input.StakeAmount,
            CreatedAt = DateTime.UtcNow
        };

        var betSelections = input.Selections.Select(selection => new BetSelection
        {
            BetId = bet.Id,
            Odds = selection.Odds,
            SelectionId = selection.Id,
            Bet = bet
        }).ToList();
        
        await _balanceTransactionRepository.InsertAsync(balanceTransaction, cancellationToken);
        await _playerRepository.UpsertAsync(player, cancellationToken);
        await _betRepository.InsertAsync(bet, cancellationToken);
        await _betSelectionRepository.InsertManyAsync(betSelections, cancellationToken);

        return new CreatedResult("", "");
    }

    private Player GetPlayer(int playerId)
    {
        var player = _playerRepository.Get(playerId) ?? new Player
        {
            Id = playerId,
            Balance = ValidationConstants.DefaultPlayerBalance
        };

        return player;
    }

    private ValidationResponse ValidateInput(Player player, MakeBetInput input)
    {
        var errorResponse = new ValidationResponse();

        _playerValidationService.ValidatePlayer(errorResponse, player, input.StakeAmount);

        var groupedSelections = input.Selections.GroupBy(x => x.Id).ToList();

        var duplicateSelections = groupedSelections.Where(e => e.Count() > 1).ToList();
        _selectionValidationService.ValidateDuplicatedSelections(errorResponse.Selections, duplicateSelections);

        var uniqueSelections = groupedSelections.Where(e => e.Count() == 1).ToList();
        _selectionValidationService.ValidateUniqueSelections(errorResponse.Selections, uniqueSelections);

        return errorResponse;
    }

    private static decimal CalculateWinAmount(decimal stateAmount, IList<decimal> odds)
    {
        var maxWinAmount = stateAmount;

        foreach (var odd in odds)
        {
            maxWinAmount *= odd;
        }

        return maxWinAmount;
    }
}