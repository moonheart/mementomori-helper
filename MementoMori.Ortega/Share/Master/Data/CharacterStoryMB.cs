using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("回想テーブル")]
	public class CharacterStoryMB : MasterBookBase
	{
		[Description("対象キャラクターID")]
		[PropertyOrder(1)]
		public long CharacterId
		{
			get;
		}

		[Description("何個目の回想か")]
		[PropertyOrder(2)]
		public long EpisodeId
		{
			get;
		}

		[Description("制限レベル")]
		[PropertyOrder(3)]
		public int Level
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("制限レアリティ")]
		public CharacterRarityFlags RarityFlags
		{
			get;
		}

		[PropertyOrder(5)]
		[Nest(false, 0)]
		[Description("初閲覧時報酬")]
		public IReadOnlyList<UserItem> RewardItemList
		{
			get;
		}

		[Description("スキップ説明文")]
		[PropertyOrder(6)]
		public string TextKey
		{
			get;
		}

		[Description("タイトルキー")]
		[PropertyOrder(4)]
		public string TitleKey
		{
			get;
		}

		[SerializationConstructor]
		public CharacterStoryMB(long id, bool? isIgnore, string memo, long characterId, long episodeId, int level, IReadOnlyList<UserItem> rewardItemList, string titleKey, string textKey, CharacterRarityFlags rarityFlags)
			:base(id, isIgnore, memo)
		{
			this.CharacterId = characterId;
			this.EpisodeId = episodeId;
			this.Level = level;
			this.RewardItemList = rewardItemList;
			this.TitleKey = titleKey;
			this.TextKey = textKey;
			this.RarityFlags = rarityFlags;
		}

		public CharacterStoryMB() : base(0, false, "")
		{
		}
	}
}
