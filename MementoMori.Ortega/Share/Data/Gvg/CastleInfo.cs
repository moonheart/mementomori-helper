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
			long num = castleInfo.AttackerGuildId;
			this.AttackerGuildId = num;
			int num2 = castleInfo.AttackPartyCount;
			this.AttackPartyCount = num2;
			long num3 = castleInfo.CastleId;
			this.CastleId = num3;
			int num4 = castleInfo.DefensePartyCount;
			this.DefensePartyCount = num4;
			long num5 = castleInfo.GuildId;
			this.GuildId = num5;
			GvgCastleState gvgCastleState = castleInfo.GvgCastleState;
			this.GvgCastleState = gvgCastleState;
			long num6 = castleInfo.UtcFallenTimeStamp;
			this.UtcFallenTimeStamp = num6;
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
