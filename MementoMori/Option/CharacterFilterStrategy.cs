namespace MementoMori;

public enum CharacterFilterStrategy
{
    /// <summary>
    /// 不包含这个角色
    /// </summary>
    Character,

    /// <summary>
    /// 这个角色的属性比自己的角色的高
    /// </summary>
    PropertyMoreThanSelf
}