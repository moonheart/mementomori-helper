using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Mission
{
	[MessagePackObject(true)]
	[Description("ビンゴ報酬情報")]
	public class BingoRewardInfo
	{
		[Description("ビンゴ種別")]
		public BingoType BingoType { get; set; }

		[Nest(true, 2)]
		[Description("報酬アイテムリスト")]
		public List<UserItem> RewardItemList{ get; set; }
        
        [Nest(true, 2)]
		[Description("報酬情報リスト")]
		public List<PanelMissionRewardInfo> RewardInfoList{ get; set; }
	}
}
