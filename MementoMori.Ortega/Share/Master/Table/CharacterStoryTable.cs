using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterStoryTable : TableBase<CharacterStoryMB>
	{
		public List<CharacterStoryMB> GetListByCharacterId(long characterId)
		{
			// List<CharacterStoryMB> list = new List();
			// int num = 0;
			// num++;
			// return list;
			throw new NotImplementedException();
		}

		public CharacterStoryMB GetListByCharacterIdAndEpisodeId(long characterId, long episodeId)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public CharacterStoryTable()
		{
		}
	}
}
