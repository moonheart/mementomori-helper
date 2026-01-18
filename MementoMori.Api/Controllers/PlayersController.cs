using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    /// <summary>
    /// Get current player information
    /// </summary>
    [HttpGet("current")]
    public ActionResult<PlayerInfo> GetCurrentPlayer()
    {
        // TODO: Get current player from AccountManager
        return Ok(new PlayerInfo
        {
            PlayerId = 12345,
            Name = "Player001",
            Level = 85,
            Vip = 5,
            Gold = 1000000,
            Diamond = 5000
        });
    }

    /// <summary>
    /// Get player's characters
    /// </summary>
    [HttpGet("characters")]
    public ActionResult<List<CharacterInfo>> GetCharacters()
    {
        // TODO: Get characters from game data
        return Ok(new List<CharacterInfo>
        {
            new CharacterInfo
            {
                Guid = System.Guid.NewGuid().ToString(),
                CharacterId = 1001,
                Level = 200,
                Rarity = CharacterRarity.SSR,
                BattlePower = 150000
            }
        });
    }

    /// <summary>
    /// Get character details by GUID
    /// </summary>
    [HttpGet("characters/{guid}")]
    public ActionResult<CharacterDetailInfo> GetCharacterDetail(string guid)
    {
        // TODO: Get character detail with stats
        return Ok(new CharacterDetailInfo
        {
            Guid = guid,
            CharacterId = 1001,
            Level = 200,
            Rarity = CharacterRarity.SSR,
            BattlePower = 150000,
            BaseStats = new CharacterStats
            {
                Hp = 10000,
                Attack = 5000,
                Defense = 3000,
                Speed = 200
            },
            BattleStats = new CharacterStats
            {
                Hp = 15000,
                Attack = 7500,
                Defense = 4500,
                Speed = 250
            }
        });
    }
}

[ExportTsClass]
public class PlayerInfo
{
    public long PlayerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Vip { get; set; }
    public long Gold { get; set; }
    public long Diamond { get; set; }
}

[ExportTsClass]
public class CharacterInfo
{
    public string Guid { get; set; } = string.Empty;
    public long CharacterId { get; set; }
    public int Level { get; set; }
    public CharacterRarity Rarity { get; set; }
    public long BattlePower { get; set; }
}

[ExportTsClass]
public class CharacterDetailInfo : CharacterInfo
{
    public CharacterStats BaseStats { get; set; } = new();
    public CharacterStats BattleStats { get; set; } = new();
}

[ExportTsClass]
public class CharacterStats
{
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
}

[ExportTsEnum]
public enum CharacterRarity
{
    N = 1,
    R = 2,
    SR = 4,
    SSR = 8
}
