using System.ComponentModel;

[Flags]
public enum SphereLevelFilterFlags
{
    None = 0,
    Lv1To6 = 1,
    Lv7To9 = 2,
    Lv10To12 = 4,
    Lv13To15 = 8,
    All = 15
}
