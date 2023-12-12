using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MementoMori.Option;

public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
{
    void Update(Action<T> applyChanges);
}

public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
{
    private readonly IFileProvider _fileProvider;
    private readonly IOptionsMonitor<T> _options;
    private readonly IConfigurationRoot _configuration;
    private readonly string _section;
    private readonly string _file;

    public WritableOptions(
        IFileProvider fileProvider,
        IOptionsMonitor<T> options,
        IConfigurationRoot configuration,
        string section,
        string file)
    {
        _fileProvider = fileProvider;
        _options = options;
        _configuration = configuration;
        _configuration.GetReloadToken().RegisterChangeCallback(x => { _value = _options.CurrentValue; }, null);
        _section = section;
        _file = file;
        _options.OnChange(obj => _value = obj);
    }

    private T _value;

    public T Value => _value ??= _options.CurrentValue;

    public T Get(string name)
    {
        return _options.Get(name);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Action<T> applyChanges)
    {
        applyChanges(Value);
        string physicalPath;
        if (_fileProvider != null)
        {
            var fileInfo = _fileProvider.GetFileInfo(_file);
            physicalPath = fileInfo.PhysicalPath;
        }
        else
        {
            physicalPath = Path.Combine(Directory.GetCurrentDirectory(), _file);
        }

        var jObject = physicalPath != null && File.Exists(physicalPath) ? JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath)) : new JObject();
        var sectionObject = jObject.TryGetValue(_section, out var section) ? JsonConvert.DeserializeObject<T>(section.ToString()) : Value ?? new T();

        applyChanges(sectionObject);

        jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
        File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
    }
}