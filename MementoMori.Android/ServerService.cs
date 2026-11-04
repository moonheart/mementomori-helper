using System.Reflection;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;

namespace MementoMoriApk;

/// <summary>
/// 前台服务: 跑 MementoMori.Api (Kestrel + Quartz), 切到后台/锁屏时系统也不杀, 自动任务照常跑.
/// 常驻通知上有 "Stop", 点了就结束整个进程 (Kestrel 在同一进程里没法干净地重启)
/// </summary>
[Register("io.github.moonheart.mementomorihelper.ServerService")] // 名字固定, AndroidManifest.xml 里手写了 (要加 specialUse 的 property)
public class ServerService : Service
{
    public const string Url = "http://127.0.0.1:5290";
    private const string Channel = "server";
    private const string ActionStop = "stop";
    public static Task? Server { get; private set; }

    public override Android.OS.IBinder? OnBind(Intent? intent) => null;

    public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
    {
        if (intent?.Action == ActionStop)
        {
            StopForeground(StopForegroundFlags.Remove);
            StopSelf();
            Java.Lang.JavaSystem.Exit(0);
            return StartCommandResult.NotSticky;
        }

        var nm = (NotificationManager) GetSystemService(NotificationService)!;
        nm.CreateNotificationChannel(new NotificationChannel(Channel, "Helper server", NotificationImportance.Low));
        var open = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), PendingIntentFlags.Immutable);
        var stop = PendingIntent.GetService(this, 1, new Intent(this, typeof(ServerService)).SetAction(ActionStop), PendingIntentFlags.Immutable);
        var notification = new Notification.Builder(this, Channel)
            .SetContentTitle("MementoMori Helper is running")
            .SetContentText("Auto tasks keep running in the background.")
            .SetSmallIcon(Resource.Mipmap.appicon)
            .SetContentIntent(open)
            .SetOngoing(true)
            .AddAction(new Notification.Action.Builder(null, "Stop", stop).Build())
            .Build();
        if (OperatingSystem.IsAndroidVersionAtLeast(34)) StartForeground(1, notification, ForegroundService.TypeSpecialUse);
        else StartForeground(1, notification);

        Server ??= Task.Run(() => RunServer(FilesDir!.AbsolutePath));
        return StartCommandResult.Sticky;
    }

    /// <summary>解压前端到私有目录, 然后跑 MementoMori.Api 的 Main (阻塞到进程结束)</summary>
    private void RunServer(string dataDir)
    {
        var contentRoot = Path.Combine(dataDir, "app");
        // 每次装新版本 (LastUpdateTime 变了) 才重新解压
        var stamp = Path.Combine(contentRoot, ".stamp");
        var version = PackageManager!.GetPackageInfo(PackageName!, 0)!.LastUpdateTime.ToString();
        if (!File.Exists(stamp) || File.ReadAllText(stamp) != version)
        {
            if (Directory.Exists(contentRoot)) Directory.Delete(contentRoot, true);
            CopyAssets("", contentRoot);
            File.WriteAllText(stamp, version);
        }

        // Api 的数据 (sqlite, Master, 图片缓存, 日志) 都是相对当前目录
        Directory.SetCurrentDirectory(dataDir);
        Environment.SetEnvironmentVariable("ASPNETCORE_CONTENTROOT", contentRoot);
        // Android 上没有 entry assembly, 不告诉它名字就找不到 Controllers
        Environment.SetEnvironmentVariable("ASPNETCORE_APPLICATIONNAME", "MementoMori.Api");
        Environment.SetEnvironmentVariable("ASPNETCORE_URLS", Url);
        Environment.SetEnvironmentVariable("Game__AutoRequestDelay", "500");
        Assembly.Load("MementoMori.Api").EntryPoint!.Invoke(null, [Array.Empty<string>()]);
    }

    private void CopyAssets(string path, string target)
    {
        var children = Assets!.List(path)!;
        if (children.Length == 0)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target)!);
            using var src = Assets.Open(path);
            using var dst = File.Create(target);
            src.CopyTo(dst);
            return;
        }
        foreach (var c in children)
        {
            // assets 根目录下还有系统自带的 (images, webkit...), 只要自己的
            if (path == "" && c != "wwwroot" && c != "appsettings.json") continue;
            CopyAssets(path == "" ? c : $"{path}/{c}", Path.Combine(target, c));
        }
    }
}
