using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MementoMori;

public class RuntimeInfo
{
    public string OrtegaAccessToken { get; set; }
    public string ApiHost { get; set; }
    public string OrtegaMasterVersion { get; set; }
    public string OrtegaAssetVersion { get; set; }
}