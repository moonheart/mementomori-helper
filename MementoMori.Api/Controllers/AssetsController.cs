using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace MementoMori.Api.Controllers;

/// <summary>
/// 游戏图片的本地缓存: 第一次从资源服务器下载存到磁盘, 之后直接返回, 浏览器缓存一年
/// </summary>
[ApiController]
[Route("api/assets")]
public partial class AssetsController : ControllerBase
{
    private const string CacheDir = "AssetCache";

    private static readonly Dictionary<string, string> Upstreams = new()
    {
        ["convert"] = "https://list.moonheart.dev/d/public/mmtm/AddressableConvertAssets",
        ["local"] = "https://list.moonheart.dev/d/public/mmtm/AddressableLocalAssets",
    };

    private static readonly HttpClient Http = new() {Timeout = TimeSpan.FromSeconds(30)};
    private static readonly FileExtensionContentTypeProvider ContentTypes = new();

    // 只允许普通的资源路径, 防止 ../ 之类跑出缓存目录
    [GeneratedRegex(@"^[A-Za-z0-9_\-/]+\.(png|jpg|jpeg|webp|json|atlas|txt)$")]
    private static partial Regex SafePath();

    [HttpGet("{source}/{**path}")]
    public async Task<IActionResult> Get(string source, string path, CancellationToken ct)
    {
        if (!Upstreams.TryGetValue(source, out var upstream) || path.Contains("..") || !SafePath().IsMatch(path)) return NotFound();

        var file = Path.GetFullPath(Path.Combine(CacheDir, source, path));
        if (!System.IO.File.Exists(file))
        {
            using var resp = await Http.GetAsync($"{upstream}/{path}", ct);
            if (!resp.IsSuccessStatusCode) return StatusCode((int) resp.StatusCode);
            Directory.CreateDirectory(Path.GetDirectoryName(file)!);
            var tmp = $"{file}.{Guid.NewGuid():N}.tmp";
            await System.IO.File.WriteAllBytesAsync(tmp, await resp.Content.ReadAsByteArrayAsync(ct), ct);
            System.IO.File.Move(tmp, file, true);
        }

        Response.Headers.CacheControl = "public, max-age=31536000, immutable";
        return PhysicalFile(file, ContentTypes.TryGetContentType(file, out var type) ? type : "application/octet-stream");
    }
}
