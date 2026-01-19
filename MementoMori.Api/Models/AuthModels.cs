using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

[ExportTsClass]
/// <summary>
/// 账号信息 DTO
/// </summary>
public class AccountDto
{
    public long UserId { get; set; }
    public string ClientKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsLoggedIn { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public long? CurrentWorldId { get; set; }
}

[ExportTsClass]
public class AddAccountRequest
{
    public long UserId { get; set; }
    public string ClientKey { get; set; } = string.Empty;
    public string? Name { get; set; }
}
