using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class MonologueTable : TableBase<MonologueMB>
	{
		public MonologueMB GetByCharacterId(long characterId)
		{
			return Array.Find(_datas, x => x.CharacterId == characterId);
		}
	}
}
