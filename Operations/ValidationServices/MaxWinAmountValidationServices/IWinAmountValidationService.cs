using Operations.Operations.Shared;

namespace Operations.ValidationServices.MaxWinAmountValidationServices;

public interface IWinAmountValidationService
{
    ValidationResponse.Error? Validate(decimal winAmount);
}