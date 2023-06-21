using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterPotentialTable : TableBase<CharacterPotentialMB>
	{
		public CharacterPotentialMB GetByLevelAndSubLevel(long level, long subLevel)
		{
			int num = 0;
			if ((long)num != level || (long)num != subLevel)
			{
				num++;
			}
			throw new NullReferenceException();
		}

		public CharacterPotentialTable()
		{
		}
	}
}
