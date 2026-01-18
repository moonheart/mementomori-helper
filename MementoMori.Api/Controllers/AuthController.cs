using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Get list of configured accounts
    /// </summary>
    [HttpGet("accounts")]
    public ActionResult<List<AccountInfo>> GetAccounts()
    {
        // TODO: Implement account management
        return Ok(new List<AccountInfo>
        {
            new AccountInfo { Id = 1, Name = "Account 1", IsActive = true },
            new AccountInfo { Id = 2, Name = "Account 2", IsActive = false }
        });
    }

    /// <summary>
    /// Add a new account
    /// </summary>
    [HttpPost("accounts")]
    public ActionResult<AccountInfo> AddAccount([FromBody] AddAccountRequest request)
    {
        // TODO: Implement add account logic
        var account = new AccountInfo
        {
            Id = 100,
            Name = request.Name,
            IsActive = false
        };
        return CreatedAtAction(nameof(GetAccounts), new { id = account.Id }, account);
    }

    /// <summary>
    /// Delete an account
    /// </summary>
    [HttpDelete("accounts/{id}")]
    public ActionResult DeleteAccount(long id)
    {
        // TODO: Implement delete account logic
        return NoContent();
    }

    /// <summary>
    /// Set active account
    /// </summary>
    [HttpPost("accounts/{id}/activate")]
    public ActionResult ActivateAccount(long id)
    {
        // TODO: Implement activate account logic
        return Ok();
    }
}

[ExportTsClass]
public class AccountInfo
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

[ExportTsClass]
public class AddAccountRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
