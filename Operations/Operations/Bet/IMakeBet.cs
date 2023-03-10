using Microsoft.AspNetCore.Mvc;

namespace Operations.Operations.Bet;

public interface IMakeBet
{
    Task<IActionResult> ExecuteAsync(MakeBetInput input, CancellationToken cancellationToken = default);
}