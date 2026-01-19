using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly AccountService _accountService;

    public AuthController(ILogger<AuthController> logger, AccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    /// <summary>
    /// Get all accounts
    /// </summary>
    [HttpGet("accounts")]
    public ActionResult<List<AccountDto>> GetAccounts()
    {
        var accounts = _accountService.GetAllAccounts();
        return Ok(accounts);
    }

    /// <summary>
    /// Add a new account with ClientKey (Method 1)
    /// </summary>
    [HttpPost("accounts/with-clientkey")]
    public ActionResult<AccountDto> AddAccountWithClientKey([FromBody] AddAccountWithClientKeyRequest request)
    {
        try
        {
            var account = _accountService.AddAccount(
                request.UserId,
                request.ClientKey,
                request.Name
            );
            return Ok(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add account with ClientKey");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Add a new account with Password (Method 2 - get ClientKey from password)
    /// </summary>
    [HttpPost("accounts/with-password")]
    public async Task<ActionResult<GetClientKeyResponse>> AddAccountWithPassword([FromBody] AddAccountWithPasswordRequest request)
    {
        try
        {
            var result = await _accountService.AddAccountWithPasswordAsync(
                request.UserId,
                request.Password,
                request.Name
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add account with password");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Delete an account
    /// </summary>
    [HttpDelete("accounts/{userId}")]
    public ActionResult DeleteAccount(long userId)
    {
        try
        {
            _accountService.DeleteAccount(userId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete account {UserId}", userId);
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Login to account
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<SimpleLoginResponse>> Login([FromBody] SimpleLoginRequest request)
    {
        try
        {
            var result = await _accountService.LoginAsync(request.UserId, request.ClientKey);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for user {UserId}", request.UserId);
            return BadRequest(new { error = ex.Message });
        }
    }
}
