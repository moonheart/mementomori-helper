using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
	[MessagePackObject(true)]
	public class CharacterDetailInfo
	{
		public List<UserEquipmentDtoInfo> UserEquipmentDtoInfos{ get; set; }
		public BaseParameter BaseParameter{ get; set; }

		public BattleParameter BattleParameter{ get; set; }

		public long BattlePower { get; set; }

		public long Level { get; set; }

		public CharacterRarityFlags RarityFlags { get; set; }
	}
}
