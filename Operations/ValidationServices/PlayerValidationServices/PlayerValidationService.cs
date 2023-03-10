using Entities.Players;
using Infrastructure.Extensions;
using Operations.Operations.Shared;

namespace Operations.ValidationServices.PlayerValidationServices;

public class PlayerValidationService : IPlayerValidationService
{
	public void ValidatePlayer(ValidationResponse makeBetOutput, Player player, decimal stakeAmount)
	{
		if (player.Balance < stakeAmount || (player.Balance == default && ValidationConstants.DefaultPlayerBalance < stakeAmount))
			makeBetOutput.Errors.Add(new ValidationResponse.Error
			{
				Code = ValidationConstants.InsufficientBalanceCode,
				Message = ValidationConstants.InsufficientBalance
			});

		ValidateNumberCountAfterDotForStakeAmount(makeBetOutput.Errors, stakeAmount);
		ValidateIfStakeAmountIsInAllowedInterval(makeBetOutput.Errors, stakeAmount);
	}

	private static void ValidateIfStakeAmountIsInAllowedInterval(IList<ValidationResponse.Error> errors, decimal value)
	{
		if (value > ValidationConstants.MaxStakeAmount)
			errors.Add(new ValidationResponse.Error
			{
				Code = ValidationConstants.MaxStakeAmountCode,
				Message = ValidationConstants.MaxStakeAmountMessage
			});
		else if (value < ValidationConstants.MinStakeAmount)
			errors.Add(new ValidationResponse.Error
			{
				Code = ValidationConstants.MinStakeAmountCode,
				Message =ValidationConstants. MinStakeAmountMessage
			});
	}

	private static void ValidateNumberCountAfterDotForStakeAmount(IList<ValidationResponse.Error> errors, decimal value)
	{
		if (value.ExceededMaxNumbersAfterDot(ValidationConstants.StakeAmountMaxNumbersAfterDot))
		{
			errors.Add(new ValidationResponse.Error
			{
				Code = ValidationConstants.StakeAmountMaxNumbersAfterDotCode,
				Message = ValidationConstants.StakeAmountMaxNumbersAfterDotMessage
			});
		}
	}
}