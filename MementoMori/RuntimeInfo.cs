using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MementoMori;

public class RuntimeInfo : ReactiveObject
{
    [Reactive]
    public string OrtegaAccessToken { get; set; }
    [Reactive]
    public string ApiHost { get; set; }
    [Reactive]
    public string OrtegaMasterVersion { get; set; }
}