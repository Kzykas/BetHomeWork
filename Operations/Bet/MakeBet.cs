using Entities.Players;
using FluentValidation;
using Infrastructure.Extensions;
using Interfaces.Repositories;

namespace Operations.Bet;

public class MakeBet : IMakeBet
{
    // Player
    public const int DefaultPlayerBalance = 1000;
    public const string InsufficientBalance = "Insufficient balance";
    public const int InsufficientBalanceCode = 8;

    // section
    public const string DuplicateSectionFound = "Duplicate Section Found";
    public const int DuplicateSectionFoundCode = 10;
    public const int OddsMaxNumbersAfterDot = 3;
    public const int OddsMaxNumbersAfterDotCode = 11;
    public static readonly string OddsMaxNumbersAfterDotMessage = $"Maximum numbers after comma in odds are {OddsMaxNumbersAfterDot}";

    private readonly IValidator<MakeBetInput> _inputValidator;
    private readonly IPlayerRepository _playerRepository;

    public MakeBet(IValidator<MakeBetInput> inputValidator)
    {
        _inputValidator = inputValidator;
    }
    
    public async Task ExecuteAsync(MakeBetInput input)
    {
        var makeBetOutput = new MakeBetOutput();

        var player = await _playerRepository.GetAsync(input.PlayerId);

        ValidatePlayer(player, input.StakeAmount, makeBetOutput);
        
        var groupedSelections = input.Selections.GroupBy(x => x.Id).ToList();
        
        var duplicateSelections = groupedSelections.Where(e => e.Count() > 1).ToList();
        ValidateDuplicatedSelections(makeBetOutput, duplicateSelections);
        
        var uniqueSelections = groupedSelections.Where(e => e.Count() > 1).ToList();
        
    }
    
    private static void ValidatePlayer(MakeBetOutput makeBetOutput, Player player, decimal stakeAmount)
    {
        if (player.Balance < stakeAmount || (player.Balance == default && DefaultPlayerBalance < stakeAmount))
            makeBetOutput.Errors.Add(new MakeBetOutput.Error
            {
                Code = InsufficientBalanceCode,
                Message = InsufficientBalance
            });
    }

    private void ValidateDuplicatedSelections(IList<IGrouping<int,MakeBetInput.Selection>> duplicateSelections)
    {
        foreach (var duplicateSelectionGroup in duplicateSelections)
        {
            foreach (var duplicateSelection in duplicateSelectionGroup)
            {
                var selectionValidationResponse = new MakeBetOutput.Selection
                {
                    Id = duplicateSelection.Id,
                    Errors = new List<MakeBetOutput.Error>
                    {
                        new()
                        {
                            Code = DuplicateSectionFoundCode,
                            Message = DuplicateSectionFound
                        }
                    }
                };

                ValidateNumberCountAfterDotInDecimals(selectionValidationResponse.Errors, duplicateSelection.Odds);
                // validate min
                // validate max
            }
        }
    }

    private static void ValidateNumberCountAfterDotInDecimals(IList<MakeBetOutput.Error> errors, decimal value)
    {
        if (value.ExceededMaxNumbersAfterDot(OddsMaxNumbersAfterDot))
        {
            errors.Add(new MakeBetOutput.Error
            {
                Code = OddsMaxNumbersAfterDotCode,
                Message = OddsMaxNumbersAfterDotMessage
            });
        }
    }
    

    private static decimal CalculateMaxWinAmount(decimal stateAmount, IList<decimal> odds)
    {
        var maxWinAmount = stateAmount;

        foreach (var odd in odds)
        {
            maxWinAmount *= stateAmount;
        }
        
        return maxWinAmount;
    }
}