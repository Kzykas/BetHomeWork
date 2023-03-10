using Entities.Players;
using Operations.Operations.Shared;

namespace Operations.ValidationServices.PlayerValidationServices;

public interface IPlayerValidationService
{
	void ValidatePlayer(ValidationResponse makeBetOutput, Player player, decimal stakeAmount);
}