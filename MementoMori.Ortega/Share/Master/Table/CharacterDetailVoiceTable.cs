using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterDetailVoiceTable : TableBase<CharacterDetailVoiceMB>
	{
		// public List<CharacterDetailVoiceMB> GetListByCharacterId(long characterId)
		// {
		// 	List<CharacterDetailVoiceMB> list = new List();
		// 	int num = 0;
		// 	num++;
		// 	Comparison<CharacterDetailVoiceMB> comparison;
		// 	if (CharacterDetailVoiceTable.<>c.<>9__0_0 == 0)
		// 	{
		// 		comparison = (CharacterDetailVoiceMB a, CharacterDetailVoiceMB b) => a.SortOrder;
		// 		CharacterDetailVoiceTable.<>c.<>9__0_0 = comparison;
		// 	}
		// 	list.Sort(comparison);
		// 	return list;
		// }

		public CharacterDetailVoiceMB GetByCharacterIdAndBattleResult(long characterId, bool isWin)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public CharacterDetailVoiceTable()
		{
		}
	}
}
