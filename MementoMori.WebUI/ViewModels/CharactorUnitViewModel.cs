using System.Reactive.Linq;
using ReactiveUI;

namespace MementoMori.WebUI.ViewModels;

public class CharactorUnitViewModel : ReactiveObject
{
    private int _count;

    public int Count
    {
        get => _count;
        set => this.RaiseAndSetIfChanged(ref _count, value);
    }

    public CharactorUnitViewModel()
    {
        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe(l =>
        {
            Count++;
        });
    }
}