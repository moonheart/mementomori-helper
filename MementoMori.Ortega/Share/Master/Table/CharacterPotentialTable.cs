using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterPotentialTable : TableBase<CharacterPotentialMB>
	{
		public CharacterPotentialMB GetByLevelAndSubLevel(long level, long subLevel)
		{
			foreach (var data in _datas)
			{
				if (data.CharacterLevel == level && data.CharacterSubLevel == subLevel)
				{
					return data;
				}
			}

			return null;
		}

		public CharacterPotentialTable()
		{
		}
	}
}
