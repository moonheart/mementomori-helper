using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(false)]
	public class CastleInfo
	{
		[Key(2)]
		public long AttackerGuildId { get; set; }

		[Key(3)]
		public int AttackPartyCount { get; set; }

		[Key(0)]
		public long CastleId { get; set; }

		[Key(4)]
		public int DefensePartyCount { get; set; }

		[Key(1)]
		public long GuildId { get; set; }

		[Key(5)]
		public GvgCastleState GvgCastleState { get; set; }

		[Key(6)]
		public long UtcFallenTimeStamp { get; set; }

		public void Copy(CastleInfo castleInfo)
		{
            AttackerGuildId = castleInfo.AttackerGuildId;
            AttackPartyCount = castleInfo.AttackPartyCount;
            CastleId = castleInfo.CastleId;
            DefensePartyCount = castleInfo.DefensePartyCount;
            GuildId = castleInfo.GuildId;
            GvgCastleState = castleInfo.GvgCastleState;
            UtcFallenTimeStamp = castleInfo.UtcFallenTimeStamp;
		}

		public long GetOffenseGuildId()
		{
			if (this.GvgCastleState > GvgCastleState.InBattle)
			{
				return this.AttackerGuildId;
			}
			return this.GuildId;
		}

		public long GetDefenseGuildId()
		{
			if (this.GvgCastleState > GvgCastleState.InBattle)
			{
				return this.GuildId;
			}
			return this.AttackerGuildId;
		}
	}
}
