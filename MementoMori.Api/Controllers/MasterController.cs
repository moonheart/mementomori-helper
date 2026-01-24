using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Master.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MasterController : ControllerBase
{
    private readonly VersionService _versionService;
    private readonly ILogger<MasterController> _logger;

    public MasterController(VersionService versionService, ILogger<MasterController> logger)
    {
        _versionService = versionService;
        _logger = logger;
    }

    /// <summary>
    /// 获取 Master 数据清单（包含版本和各表哈希）
    /// </summary>
    [HttpGet("manifest")]
    public IActionResult GetManifest()
    {
        var tables = Masters.GetAllTables().ToList();
        
        // 特殊处理 TextResourceTable
        // 我们不需要在通用清单中返回具体的 TextResource*MB
        // 因为它们是按语言加载的
        var filteredTables = tables.Where(t => t.GetType().Name != "TextResourceTable").ToList();

        var manifest = new
        {
            Version = _versionService.MasterVersion,
            Tables = filteredTables.Select(t => new
            {
                Name = t.GetType().Name,
                Hash = CalculateTableHash(t),
                Count = GetTableCount(t)
            }).ToList()
        };

        return Ok(manifest);
    }

    /// <summary>
    /// 获取全表数据
    /// </summary>
    [HttpGet("table/{tableName}")]
    public IActionResult GetTable(string tableName)
    {
        var table = Masters.GetAllTables().FirstOrDefault(t => t.GetType().Name == tableName);
        if (table == null)
        {
            return NotFound(new { error = $"Table {tableName} not found" });
        }

        // 获取底层数据数组
        var getDataMethod = table.GetType().GetMethod("GetArray");
        if (getDataMethod == null)
        {
            return BadRequest(new { error = "Could not get data array for table" });
        }

        var data = getDataMethod.Invoke(table, null);
        return Ok(data);
    }

    private string CalculateTableHash(ITable table)
    {
        try
        {
            // 对于 TableBase<TM>，GetMasterBookName() 返回的是 TM 的类名 (如 CharacterMB)
            var mbName = table.GetMasterBookName();
            if (string.IsNullOrEmpty(mbName)) return "unknown";

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Master", mbName);

            if (System.IO.File.Exists(filePath))
            {
                using var md5 = MD5.Create();
                using var stream = System.IO.File.OpenRead(filePath);
                var hashBytes = md5.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to calculate hash for table {TableName}", table.GetType().Name);
        }

        return "unknown";
    }

    private int GetTableCount(ITable table)
    {
        var countMethod = table.GetType().GetMethod("Count");
        if (countMethod != null)
        {
            return (int)countMethod.Invoke(table, null)!;
        }
        return 0;
    }
}
