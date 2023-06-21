using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("レベルリンク")]
	public class LevelLinkMB : MasterBookBase
	{
		[Description("パーティーレベル")]
		public int PartyLevel
		{
			get;
		}

		[Description("パーティーSubレベル")]
		public int PartySubLevel
		{
			get;
		}

		[Nest(false, 0)]
		[Description("レベルアップに必要なアイテム")]
		public IReadOnlyList<UserItem> RequiredLevelUpItems
		{
			get;
		}

		[SerializationConstructor]
		public LevelLinkMB(long id, bool? isIgnore, string memo, int partyLevel, int partySubLevel, IReadOnlyList<UserItem> requiredLevelUpItems)
			:base(id, isIgnore, memo)
		{
			PartyLevel = partyLevel;
			PartySubLevel = partySubLevel;
			RequiredLevelUpItems = requiredLevelUpItems;
		}

		public LevelLinkMB() :base(0L, false, ""){}
	}
}
