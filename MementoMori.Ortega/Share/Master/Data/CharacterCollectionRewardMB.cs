using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("アルカナ解放報酬")]
	[MessagePackObject(true)]
	public class CharacterCollectionRewardMB : MasterBookBase
	{
		[Description("構成キャラ数")]
		[PropertyOrder(1)]
		public int CharacterCount { get; }

		[Description("段階（アルカナレベル）")]
		[PropertyOrder(2)]
		public int CollectionLevel { get; }

		[Description("アルカナが解放されるキャラレアリティ")]
		[PropertyOrder(3)]
		public CharacterRarityFlags CharacterRarityFlags { get; }

		[PropertyOrder(4)]
		[Nest(false, 0)]
		[Description("報酬アイテム")]
		public IReadOnlyList<UserItem> RewardItems { get; }

		[SerializationConstructor]
		public CharacterCollectionRewardMB(long id, bool? isIgnore, string memo, int characterCount, int collectionLevel, CharacterRarityFlags characterRarityFlags, IReadOnlyList<UserItem> rewardItems) : base(id, isIgnore, memo)
		{
			this.CharacterCount = characterCount;
			this.CollectionLevel = collectionLevel;
			this.CharacterRarityFlags = characterRarityFlags;
			this.RewardItems = rewardItems;
		}

		public CharacterCollectionRewardMB() : base(0L, false, "")
		{
		}
	}
}
