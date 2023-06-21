using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("キャラクターレベル")]
	public class CharacterLevelMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("レベル")]
		public long Level
		{
			get;
		}

		[Description("レベルアップ必要アイテム")]
		[Nest(false, 0)]
		[PropertyOrder(2)]
		public IReadOnlyList<UserItem> RequiredItemList
		{
			get;
		}

		[SerializationConstructor]
		public CharacterLevelMB(long id, bool? isIgnore, string memo, long level, IReadOnlyList<UserItem> requiredItemList)
			:base(id, isIgnore, memo)
		{
			Level = level;
			RequiredItemList = requiredItemList;
		}

		public CharacterLevelMB()
		 			:base(0, false, "")
		{
		
		}
	}
}
