namespace MementoMori.Api.Infrastructure;

/// <summary>
/// 日志写到 logs/app-yyyyMMdd.log (相对工作目录 = 数据目录), 容器重建/窗口关了也还在, 保留 14 天
/// </summary>
public sealed class FileLoggerProvider : ILoggerProvider
{
    private const int KeepDays = 14;
    private readonly object _lock = new();
    private readonly string _dir = Path.GetFullPath("logs");

    public FileLoggerProvider()
    {
        Directory.CreateDirectory(_dir);
        foreach (var f in Directory.GetFiles(_dir, "app-*.log"))
            if (File.GetLastWriteTime(f) < DateTime.Now.AddDays(-KeepDays)) File.Delete(f);
    }

    public ILogger CreateLogger(string categoryName) => new FileLogger(this, categoryName);

    public void Dispose() { }

    private void Write(string line)
    {
        // ponytail: 每行开一次文件, 量大了再换成后台队列
        lock (_lock)
        {
            try { File.AppendAllText(Path.Combine(_dir, $"app-{DateTime.Now:yyyyMMdd}.log"), line); }
            catch (IOException) { }
        }
    }

    private sealed class FileLogger(FileLoggerProvider provider, string category) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel.ToString()[..4].ToUpper()}] {category}: {formatter(state, exception)}{Environment.NewLine}";
            if (exception != null) line += exception + Environment.NewLine;
            provider.Write(line);
        }
    }
}
