using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[MessagePackObject(true)]
	[Description("パーティー")]
	public class ClearPartyCharacterInfo
	{
		[Description("Character固有キー")]
		public string CharacterGuid { get; set; }

		[Description("CharacterMBのID")]
		public long CharacterId { get; set; }

		[Description("レアリティ")]
		public CharacterRarityFlags RarityFlags { get; set; }

		[Description("レベル")]
		public long Level { get; set; }

		[Description("レベルリンク可否")]
		public bool IsLevelLink { get; set; }

		[Description("キャラクターが装着している武具情報")]
		public List<UserEquipmentDtoInfo> UserEquipmentDtoInfos { get; set; }
		[Description("ベースパラメータ")]
		public BaseParameter BaseParameter { get; set; }

		[Description("バトルパラメータ")]
		public BattleParameter BattleParameter { get; set; }

		[Description("戦闘力")]
		public long BattlePower { get; set; }
	}
}
