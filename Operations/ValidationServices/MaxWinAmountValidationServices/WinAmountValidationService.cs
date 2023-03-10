using Operations.Operations.Shared;

namespace Operations.ValidationServices.MaxWinAmountValidationServices;

public class WinAmountValidationService : IWinAmountValidationService
{
    public ValidationResponse.Error? Validate(decimal winAmount)
    {
        if (winAmount < ValidationConstants.MaxWinAmount)
        {
            return new ValidationResponse.Error
            {
                Code = ValidationConstants.MaxWinAmountCode,
                Message = ValidationConstants.MaxWinAmountMessage
            };
        }

        return null;
    }
}