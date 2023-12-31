using System.Text;
using AutoCtor;
using Injectio.Attributes;
using MementoMori.BlazorShared;
using Microsoft.JSInterop;

namespace MementoMori.WebUI.Services;

[RegisterScoped]
[AutoConstruct]
public partial class FileSaver : IFileSaver
{
    private readonly IJSRuntime _jsRuntime;

    public async Task SaveFile(string content, string filename)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        using var streamRef = new DotNetStreamReference(new MemoryStream(bytes));
        await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", filename, streamRef);
    }
}