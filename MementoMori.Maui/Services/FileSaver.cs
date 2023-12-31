using Injectio.Attributes;
using MementoMori.BlazorShared;

namespace MementoMori.Maui.Services;

[RegisterSingleton]
public class FileSaver: IFileSaver
{
    public async Task SaveFile(string content, string filename)
    {
        var tempPath = Path.Combine(FileSystem.CacheDirectory, filename);
        await File.WriteAllTextAsync(tempPath, content);
        await Share.Default.RequestAsync(new ShareFileRequest() { File = new ShareFile(tempPath) });
    }
}