using FluentValidation.Results;

namespace Operations.Bet;

public interface IMakeBet
{
    Task ExecuteAsync(MakeBetInput input);
}