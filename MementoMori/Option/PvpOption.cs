namespace MementoMori;

public class PvpOption
{
    public TargetSelectStrategy SelectStrategy { get; set; } = TargetSelectStrategy.Random;
    public List<CharacterFilter> CharacterFilters { get; set; } = new();
    public List<long> ExcludePlayerIds { get; set; } = new();
}