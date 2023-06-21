using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterRankUpMaterialTable : TableBase<CharacterRankUpMaterialMB>
	{
		public CharacterRankUpMaterialMB GetByUserCharacterDtoInfo(UserCharacterDtoInfo userCharacterDtoInfo)
		{
			// CharacterTable CharacterTable = Masters.CharacterTable;
			// long CharacterId = userCharacterDtoInfo.CharacterId;
			// ElementType ElementType = CharacterTable.GetById(CharacterId).ElementType;
			// int num = 0;
			// num++;
			// CharacterRarityFlags RarityFlags = userCharacterDtoInfo.RarityFlags;
			// num++;
			// long CharacterId2 = userCharacterDtoInfo.CharacterId;
			// CharacterRarityFlags RarityFlags2 = userCharacterDtoInfo.RarityFlags;
			// string text = string.Format("CharacterRankUpMaterialTable (CharacterId : {0} RarityFlags：{1})が存在しません", CharacterId2, CharacterId2);
			throw new NullReferenceException();
		}

		public CharacterRankUpMaterialTable()
		{
		}
	}
}
