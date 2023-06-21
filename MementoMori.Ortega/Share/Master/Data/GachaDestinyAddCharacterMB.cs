using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("運命ガチャ追加キャラクター")]
	public class GachaDestinyAddCharacterMB : MasterBookBase
	{
		[Description("キャラクターID")]
		[PropertyOrder(1)]
		public long CharacterId
		{
			get;
		}

		[Description("セレクトリスト追加タイプ")]
		[PropertyOrder(2)]
		public GachaAddCharacterType GachaAddCharacterType
		{
			get;
		}

		[Description("開始時間参照用GachaSelectListMBのID")]
		[PropertyOrder(3)]
		public long StartGachaSelectListId
		{
			get;
		}

		[Description("終了時間参照用GachaSelectListMBのID")]
		[PropertyOrder(4)]
		public long EndGachaSelectListId
		{
			get;
		}

		[SerializationConstructor]
		public GachaDestinyAddCharacterMB(long id, bool? isIgnore, string memo, long characterId, GachaAddCharacterType gachaAddCharacterType, long startGachaSelectListId, long endGachaSelectListId)
			:base(id, isIgnore, memo)
		{
			CharacterId = characterId;
			GachaAddCharacterType = gachaAddCharacterType;
			StartGachaSelectListId = startGachaSelectListId;
			EndGachaSelectListId = endGachaSelectListId;
		}

		public GachaDestinyAddCharacterMB() : base(0, false, "")
		{
		}
	}
}
