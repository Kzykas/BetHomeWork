using Microsoft.AspNetCore.Mvc;
using Operations.Operations.Bet;

namespace BetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BetController : ControllerBase
{
    private readonly IMakeBet _makeBet;

    public BetController(IMakeBet makeBet)
    {
        _makeBet = makeBet;
    }

    [HttpPost]
    public async Task<IActionResult> Bet(MakeBetInput makeBetInput, CancellationToken cancellationToken = default)
    {
        return await _makeBet.ExecuteAsync(makeBetInput, cancellationToken);
    }
}