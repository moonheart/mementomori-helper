using Android.Content;
using Android.Webkit;

namespace MementoMoriApk;

/// <summary>只是个 WebView; 服务器在 ServerService 里跑</summary>
[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@android:style/Theme.Material.Light.NoActionBar", LaunchMode = Android.Content.PM.LaunchMode.SingleTask,
    ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.KeyboardHidden)]
public class MainActivity : Activity
{
    private WebView _web = null!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Android 13+ 要先允许通知, 不然常驻通知看不到 (服务照样跑)
        if (OperatingSystem.IsAndroidVersionAtLeast(33)) RequestPermissions([Android.Manifest.Permission.PostNotifications], 0);
        StartForegroundService(new Intent(this, typeof(ServerService)));

        _web = new WebView(this);
        _web.Settings.JavaScriptEnabled = true;
        _web.Settings.DomStorageEnabled = true; // 前端用 localStorage
        _web.SetWebViewClient(new WebViewClient()); // 链接在 app 内打开, 不跳浏览器
        SetContentView(_web);
        _web.LoadData("<body style='font-family:sans-serif;padding:24px'>Starting… the first launch downloads game data, this can take a few minutes.</body>", "text/html", "utf-8");

        _ = Task.Run(async () =>
        {
            using var http = new HttpClient {Timeout = TimeSpan.FromSeconds(5)};
            while (true)
            {
                if (ServerService.Server is {IsFaulted: true} s)
                {
                    RunOnUiThread(() => _web.LoadData($"<pre>{System.Net.WebUtility.HtmlEncode(s.Exception?.ToString())}</pre>", "text/html", "utf-8"));
                    return;
                }
                try { if ((await http.GetAsync(ServerService.Url)).IsSuccessStatusCode) break; } catch { }
                await Task.Delay(1000);
            }
            RunOnUiThread(() => _web.LoadUrl(ServerService.Url));
        });
    }

    public override void OnBackPressed()
    {
        if (_web.CanGoBack()) _web.GoBack();
        else MoveTaskToBack(true); // 返回键不结束 app, 服务继续跑
    }
}
