namespace MementoMori;

public class CharacterFilter
{
    public long CharacterId { get; set; }
    public CharacterFilterStrategy FilterStrategy { get; set; } = CharacterFilterStrategy.Character;
    public BattleParameterType BattleParameterType { get; set; } = BattleParameterType.AttackPower;
}