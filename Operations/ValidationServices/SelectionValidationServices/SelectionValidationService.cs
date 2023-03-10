using Infrastructure.Extensions;
using Operations.Operations.Bet;
using Operations.Operations.Shared;

namespace Operations.ValidationServices.SelectionValidationServices;

public class SelectionValidationService : ISelectionValidationService
{
    public void ValidateUniqueSelections(IList<ValidationResponse.Selection> selectionsResponse,
        IList<IGrouping<int, MakeBetInput.Selection>> uniqueSelections)
    {
        foreach (var uniqueSelection in uniqueSelections)
        {
            var selection = uniqueSelection.First();

            var numberCountAfterDotResponse = ValidateNumberCountAfterDotForOdds(selection.Odds);

            var isInAllowedIntervalResponse = ValidateIfOddsIsInAllowedInterval(selection.Odds);

            var hasInvalidFields = numberCountAfterDotResponse != null || isInAllowedIntervalResponse != null;

            if (hasInvalidFields)
            {
                var selectionErrorResponse = new ValidationResponse.Selection
                {
                    Id = selection.Id,
                };

                if (numberCountAfterDotResponse != null)
                    selectionErrorResponse.Errors.Add(numberCountAfterDotResponse);

                if (isInAllowedIntervalResponse != null)
                    selectionErrorResponse.Errors.Add(isInAllowedIntervalResponse);

                selectionsResponse.Add(selectionErrorResponse);
            }
        }
    }

    public void ValidateDuplicatedSelections(IList<ValidationResponse.Selection> selectionsResponse,
        IList<IGrouping<int, MakeBetInput.Selection>> duplicateSelections)
    {
        foreach (var duplicateSelectionGroup in duplicateSelections)
        {
            foreach (var duplicateSelection in duplicateSelectionGroup)
            {
                var selectionErrorResponse = new ValidationResponse.Selection
                {
                    Id = duplicateSelection.Id,
                };

                selectionErrorResponse.Errors.Add(new()
                {
                    Code = ValidationConstants.DuplicateSectionFoundCode,
                    Message = ValidationConstants.DuplicateSectionFound
                });
                
                var numberCountAfterDotResponse = ValidateNumberCountAfterDotForOdds(duplicateSelection.Odds);

                var isInAllowedIntervalResponse = ValidateIfOddsIsInAllowedInterval(duplicateSelection.Odds);

                var hasInvalidFields = numberCountAfterDotResponse != null || isInAllowedIntervalResponse != null;

                if (hasInvalidFields)
                {
                    if (numberCountAfterDotResponse != null)
                        selectionErrorResponse.Errors.Add(numberCountAfterDotResponse);

                    if (isInAllowedIntervalResponse != null)
                        selectionErrorResponse.Errors.Add(isInAllowedIntervalResponse);

                    selectionsResponse.Add(selectionErrorResponse);
                }

            }
        }
    }
    
    private static ValidationResponse.Error? ValidateNumberCountAfterDotForOdds(decimal value)
    {
        if (value.ExceededMaxNumbersAfterDot(ValidationConstants.OddsMaxNumbersAfterDot))
        {
            return new ValidationResponse.Error
            {
                Code = ValidationConstants.OddsMaxNumbersAfterDotCode,
                Message = ValidationConstants.OddsMaxNumbersAfterDotMessage
            };
        }

        return null;
    }

    private static ValidationResponse.Error? ValidateIfOddsIsInAllowedInterval(decimal value)
    {
        return value switch
        {
            > ValidationConstants.MaxOdds => new ValidationResponse.Error
            {
                Code = ValidationConstants.MaxOddsCode,
                Message = ValidationConstants.MaxOddsMessage
            },
            < ValidationConstants.MinOdds => new ValidationResponse.Error
            {
                Code = ValidationConstants.MinOddsCode,
                Message = ValidationConstants.OddsMinMessage
            },
            _ => null
        };
    }
}