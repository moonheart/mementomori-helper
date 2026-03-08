using MementoMori.Ortega.Share.Enums;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

/// <summary>
/// 开始自动刷主线请求
/// </summary>
[ExportTsClass]
public class StartAutoBossRequest
{
    /// <summary>
    /// 目标关卡ID（可选，0或不设置表示无限制）
    /// </summary>
    public long? TargetQuestId { get; set; }
}

/// <summary>
/// 开始自动爬塔请求
/// </summary>
[ExportTsClass]
public class StartAutoTowerRequest
{
    /// <summary>
    /// 目标塔类型
    /// </summary>
    public TowerType TowerType { get; set; } = TowerType.Infinite;

    /// <summary>
    /// 目标层数（可选，0或不设置表示无限制）
    /// </summary>
    public long? TargetFloor { get; set; }
}

/// <summary>
/// 后台任务状态
/// </summary>
[ExportTsClass]
public class BackgroundTaskStatus
{
    /// <summary>
    /// 是否正在运行自动刷主线
    /// </summary>
    public bool IsAutoBossRunning { get; set; }

    /// <summary>
    /// 是否正在运行自动爬塔
    /// </summary>
    public bool IsAutoTowerRunning { get; set; }

    /// <summary>
    /// 自动刷主线当前进度
    /// </summary>
    public AutoBossProgress? AutoBossProgress { get; set; }

    /// <summary>
    /// 自动爬塔当前进度
    /// </summary>
    public AutoTowerProgress? AutoTowerProgress { get; set; }
}

/// <summary>
/// 自动刷主线进度
/// </summary>
[ExportTsClass]
public class AutoBossProgress
{
    /// <summary>
    /// 当前挑战的关卡ID
    /// </summary>
    public long CurrentQuestId { get; set; }

    /// <summary>
    /// 总挑战次数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 胜利次数
    /// </summary>
    public int WinCount { get; set; }

    /// <summary>
    /// 错误次数
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// 是否已完成
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// 最新日志消息
    /// </summary>
    public string? LastMessage { get; set; }
}

/// <summary>
/// 自动爬塔进度
/// </summary>
[ExportTsClass]
public class AutoTowerProgress
{
    /// <summary>
    /// 目标塔类型
    /// </summary>
    public TowerType TowerType { get; set; }

    /// <summary>
    /// 当前挑战的层数
    /// </summary>
    public long CurrentFloor { get; set; }

    /// <summary>
    /// 总挑战次数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 胜利次数
    /// </summary>
    public int WinCount { get; set; }

    /// <summary>
    /// 错误次数
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// 今日已通关层数（元素塔用）
    /// </summary>
    public int TodayClearCount { get; set; }

    /// <summary>
    /// 是否已完成
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// 最新日志消息
    /// </summary>
    public string? LastMessage { get; set; }
}
