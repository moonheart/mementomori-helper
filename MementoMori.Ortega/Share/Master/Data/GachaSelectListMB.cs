using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ガチャセレクトリスト")]
	[MessagePackObject(true)]
	public class GachaSelectListMB : MasterBookBase, IHasJstStartEndTime
	{
		[Description("設定可能キャラクターIDリスト")]
		[PropertyOrder(5)]
		public IReadOnlyList<long> CharacterIdList
		{
			get;
		}

		[Description("ガチャセレクトリストタイプ")]
		[PropertyOrder(1)]
		public GachaSelectListType GachaSelectListType
		{
			get;
		}

		[Description("新規キャラクターIDリスト")]
		[PropertyOrder(6)]
		public IReadOnlyList<long> NewCharacterIdList
		{
			get;
		}

		[Description("優先度")]
		[PropertyOrder(4)]
		public int OrderNumber
		{
			get;
			set;
		}

		[SerializationConstructor]
		public GachaSelectListMB(long id, bool? isIgnore, string memo, GachaSelectListType gachaSelectListType, string startTimeFixJST, string endTimeFixJST, int orderNumber, IReadOnlyList<long> characterIdList, IReadOnlyList<long> newCharacterIdList)
			:base(id, isIgnore, memo)
		{
			GachaSelectListType = gachaSelectListType;
			StartTimeFixJST = startTimeFixJST;
			EndTimeFixJST = endTimeFixJST;
			OrderNumber = orderNumber;
			CharacterIdList = characterIdList;
			NewCharacterIdList = newCharacterIdList;
		}

		public GachaSelectListMB() : base(0, false, "")
		{
		}

		[Description("開始時間(JST)")]
		[PropertyOrder(2)]
		public string StartTimeFixJST
		{
			get;
		}

		[Description("終了時間(JST)")]
		[PropertyOrder(3)]
		public string EndTimeFixJST
		{
			get;
		}
	}
}
