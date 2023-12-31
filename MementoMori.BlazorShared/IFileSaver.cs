namespace MementoMori.BlazorShared;

public interface IFileSaver
{
    Task SaveFile(string content, string filename);
}