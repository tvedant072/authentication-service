using AuthenticationService.Api.DTOs;
using AuthenticationService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController(
    IRegistrationService registrationService,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<RegisterResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var result = await registrationService.RegisterAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Email address is already registered.",
                Detail = "Use a different email address or sign in to the existing account."
            });
        }

        logger.LogInformation("Registered user {UserId}", result.User!.Id);

        return StatusCode(StatusCodes.Status201Created, result.User);
    }
}
