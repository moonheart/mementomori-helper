using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

/// <summary>
/// 简化的登录响应
/// </summary>
[ExportTsClass]
public class SimpleLoginResponse
{
    public bool Success { get; set; }
    public long UserId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public long? WorldId { get; set; }
}

/// <summary>
/// 简化的登录请求
/// </summary>
[ExportTsClass]
public class SimpleLoginRequest
{
    public long UserId { get; set; }
    public string ClientKey { get; set; } = string.Empty;
}
