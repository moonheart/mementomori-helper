using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ギルドツリー挑戦報酬")]
	public class GuildTowerChallengeRewardMB : MasterBookBase
	{
		[PropertyOrder(2)]
		[Description("難易度")]
		public int Difficulty { get; }

		[Nest(false, 0)]
		[PropertyOrder(3)]
		[Description("ドロップ報酬リスト")]
		public IReadOnlyList<UserItem> DropItemList { get; }

		[PropertyOrder(1)]
		[Description("階層数")]
		public long Floor { get; }

		[SerializationConstructor]
		public GuildTowerChallengeRewardMB(long id, bool? isIgnore, string memo, long floor, int difficulty, IReadOnlyList<UserItem> dropItemList)
            :base(id, isIgnore, memo)
		{
			Floor = floor;
			Difficulty = difficulty;
			DropItemList = dropItemList;
		}

		public GuildTowerChallengeRewardMB(): base(default, default, default)
		{
		}
	}
}
