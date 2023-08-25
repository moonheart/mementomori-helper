using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterCollectionLevelTable : TableBase<CharacterCollectionLevelMB>
	{
		public CharacterCollectionLevelMB GetByCollectionIdAndLevel(long collectionId, int collectionLevel)
		{
			return _datas.FirstOrDefault(d => d.CollectionId == collectionId && d.CollectionLevel == collectionLevel);
		}

		public List<CharacterCollectionLevelMB> GetByCollectionId(long collectionId)
		{
			// List<CharacterCollectionLevelMB> list = new List();
			// int num = 0;
			// if ((long)num == collectionId)
			// {
			// }
			// num++;
			// Comparison<CharacterCollectionLevelMB> comparison;
			// if (CharacterCollectionLevelTable.<>c.<>9__1_0 == 0)
			// {
			// 	comparison = (CharacterCollectionLevelMB a, CharacterCollectionLevelMB b) => a.CollectionLevel;
			// 	CharacterCollectionLevelTable.<>c.<>9__1_0 = comparison;
			// }
			// list.Sort(comparison);
			// return list;
			throw new NullReferenceException();
		}

		public CharacterCollectionLevelTable()
		{
		}
	}
}
