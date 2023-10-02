using System.ComponentModel.DataAnnotations.Schema;

namespace MementoMori.Account;

[Table("user_info")]
public class UserInfo
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("create_time")]
    public DateTime CreateTime { get; set; }

    [Column("last_login_time")]
    public DateTime LastLoginTime { get; set; }

    [Column("last_login_ip")]
    public string LastLoginIp { get; set; }

    [Column("max_worker_count")]
    public int MaxWorkerCount { get; set; }
}

[Table("game_account")]
internal class GameAccount
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("account_name")]
    public string AccountName { get; set; }

    [Column("account_id")]
    public string AccountId { get; set; }

    [Column("account_client_key")]
    public string AccountClientKey { get; set; }
}

[Table("game_player")]
public class GamePlayer
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("account_id")]
    public string AccountId { get; set; }

    [Column("player_id")]
    public string PlayerId { get; set; }

    [Column("world_id")]
    public string WorldId { get; set; }
}