using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

/// <summary>
/// 添加账号请求 - 方式1：直接使用 ClientKey
/// </summary>
[ExportTsClass]
public class AddAccountWithClientKeyRequest
{
    public string Name { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string ClientKey { get; set; } = string.Empty;
}

/// <summary>
/// 添加账号请求 - 方式2：使用密码（引继码）
/// </summary>
[ExportTsClass]
public class AddAccountWithPasswordRequest
{
    public string Name { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// 获取 ClientKey 响应
/// </summary>
[ExportTsClass]
public class GetClientKeyResponse
{
    public bool Success { get; set; }
    public string? ClientKey { get; set; }
    public string Message { get; set; } = string.Empty;
}
