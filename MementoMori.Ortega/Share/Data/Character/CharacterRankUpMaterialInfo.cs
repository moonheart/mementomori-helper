using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
	[MessagePackObject(true)]
	public class CharacterRankUpMaterialInfo
	{
		[Description("ランクアップ対象キャラクター Guid")]
		public string TargetGuid { get; set; }

		[Description("素材キャラクターGuid1")]
		public string MaterialGuid1 { get; set; }

		[Description("素材キャラクター Guid2")]
		public string MaterialGuid2 { get; set; }
	}
}
