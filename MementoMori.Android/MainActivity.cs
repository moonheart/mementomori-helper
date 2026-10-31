using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Webkit;

namespace MementoMoriApk;

/// <summary>只是个 WebView; 服务器在 ServerService 里跑</summary>
[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@android:style/Theme.Material.Light.NoActionBar", LaunchMode = Android.Content.PM.LaunchMode.SingleTask,
    ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.KeyboardHidden)]
public class MainActivity : Activity, View.IOnApplyWindowInsetsListener
{
    private WebView _web = null!;
    private FrameLayout _root = null!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Android 15 (targetSdk 35) 起强制 edge-to-edge: 内容默认画到状态栏/导航栏下面,
        // 所以状态栏改透明, 再在 OnApplyWindowInsets 里用 padding 让开 (老系统上 insets 是 0, 不会重复让)
        if (OperatingSystem.IsAndroidVersionAtLeast(30)) Window!.SetDecorFitsSystemWindows(false);
        Window!.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
        Window.SetStatusBarColor(Color.Transparent);

        // Android 13+ 要先允许通知, 不然常驻通知看不到 (服务照样跑)
        if (OperatingSystem.IsAndroidVersionAtLeast(33)) RequestPermissions([Android.Manifest.Permission.PostNotifications], 0);
        StartForegroundService(new Intent(this, typeof(ServerService)));

        _web = new WebView(this);
        _web.Settings.JavaScriptEnabled = true;
        _web.Settings.DomStorageEnabled = true; // 前端用 localStorage
        _web.SetWebViewClient(new WebViewClient()); // 链接在 app 内打开, 不跳浏览器
        _web.AddJavascriptInterface(new ThemeBridge(this), "AndroidTheme");
        // 让位用外层容器的 padding 做, 不给 WebView 加 padding: Chromium 的命中测试不认 padding, 顶栏会点不动
        _root = new FrameLayout(this);
        _root.SetBackgroundColor(Color.White); // 让出来的那条边显示的是它, 前端切主题时会通过 AndroidTheme 改
        _root.AddView(_web, new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        SetContentView(_root);
        _root.SetOnApplyWindowInsetsListener(this);
        ApplySystemBarIcons(dark: false); // 页面加载前是浅色, 先用深色图标 (要等上面的 decor 建好)
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

    /// <summary>状态栏 / 导航栏 / 刘海 / 输入法占的高度都算成 padding, 不然内容会压在系统栏下面</summary>
    public WindowInsets OnApplyWindowInsets(View v, WindowInsets insets)
    {
        int left, top, right, bottom;
        if (OperatingSystem.IsAndroidVersionAtLeast(30))
        {
            var i = insets.GetInsets(WindowInsets.Type.SystemBars() | WindowInsets.Type.DisplayCutout() | WindowInsets.Type.Ime());
            (left, top, right, bottom) = (i.Left, i.Top, i.Right, i.Bottom);
        }
        else
        {
#pragma warning disable CA1422, CS0618 // 老系统上这些属性才有效, 30+ 走上面的分支
            (left, top, right, bottom) = (insets.SystemWindowInsetLeft, insets.SystemWindowInsetTop, insets.SystemWindowInsetRight, insets.SystemWindowInsetBottom);
#pragma warning restore CA1422, CS0618
            if (OperatingSystem.IsAndroidVersionAtLeast(28) && insets.DisplayCutout is { } cutout)
            {
                left = Math.Max(left, cutout.SafeInsetLeft);
                top = Math.Max(top, cutout.SafeInsetTop);
                right = Math.Max(right, cutout.SafeInsetRight);
                bottom = Math.Max(bottom, cutout.SafeInsetBottom);
            }
        }
        if (_root.PaddingLeft != left || _root.PaddingTop != top || _root.PaddingRight != right || _root.PaddingBottom != bottom)
            _root.SetPadding(left, top, right, bottom);
        return insets;
    }

    /// <summary>前端切主题时回传页面底色和明暗, 让出来的状态栏那条跟着页面走</summary>
    internal void ApplyWebTheme(string? rgb, bool dark)
    {
        // 解析不了就给个兜底色, 至少别永远是白条
        var color = TryParseRgb(rgb, out var parsed) ? parsed : dark ? Color.Rgb(10, 10, 10) : Color.White;
        _root.SetBackgroundColor(color);
        // 30 以下窗口不是 edge-to-edge, 状态栏是不透明的, 直接染成页面底色
        if (!OperatingSystem.IsAndroidVersionAtLeast(30)) Window!.SetStatusBarColor(color);
        ApplySystemBarIcons(dark);
    }

    /// <summary>状态栏/导航栏图标: 浅色页面用深色图标, 深色页面用浅色图标</summary>
    private void ApplySystemBarIcons(bool dark)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(30))
        {
            // 开屏时 decor 可能还没建好, InsetsController 会是 null
            if (Window!.InsetsController is not { } controller) return;
            var light = (int) (WindowInsetsControllerAppearance.LightStatusBars | WindowInsetsControllerAppearance.LightNavigationBars);
            controller.SetSystemBarsAppearance(dark ? 0 : light, light);
        }
        else if (OperatingSystem.IsAndroidVersionAtLeast(23))
        {
#pragma warning disable CS0618 // 30 以下只能用这套老标记, 行为正常
            var flags = SystemUiFlags.LayoutStable;
            if (!dark) flags |= SystemUiFlags.LightStatusBar | (OperatingSystem.IsAndroidVersionAtLeast(26) ? SystemUiFlags.LightNavigationBar : 0);
            Window!.DecorView.SystemUiVisibility = (StatusBarVisibility) flags;
#pragma warning restore CS0618
        }
    }

    /// <summary>解析前端用 canvas 转出来的 "r,g,b"</summary>
    private static bool TryParseRgb(string? rgb, out Color color)
    {
        color = Color.White;
        if (string.IsNullOrEmpty(rgb)) return false;
        var parts = rgb.Split(',');
        if (parts.Length < 3) return false;
        if (!int.TryParse(parts[0], out var r) || !int.TryParse(parts[1], out var g) || !int.TryParse(parts[2], out var b)) return false;
        color = Color.Argb(255, Math.Clamp(r, 0, 255), Math.Clamp(g, 0, 255), Math.Clamp(b, 0, 255));
        return true;
    }

    public override void OnBackPressed()
    {
        if (_web.CanGoBack()) _web.GoBack();
        else MoveTaskToBack(true); // 返回键不结束 app, 服务继续跑
    }
}

/// <summary>页面 -> 原生的窄接口 (window.AndroidTheme), 只在主题变化时调一次</summary>
public sealed class ThemeBridge(MainActivity activity) : Java.Lang.Object
{
    [JavascriptInterface]
    [Java.Interop.Export("setTheme")]
    public void SetTheme(string rgb, bool dark) => activity.RunOnUiThread(() => activity.ApplyWebTheme(rgb, dark));
}
