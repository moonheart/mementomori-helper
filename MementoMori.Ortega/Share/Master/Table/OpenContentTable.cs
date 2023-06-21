using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class OpenContentTable : TableBase<OpenContentMB>
	{
		public List<OpenContentMB> GetListByOpenContentType(OpenContentType type)
		{
			// List<OpenContentMB> list = new List();
			// int num = 0;
			// num++;
			// return list;
			throw new NullReferenceException();
		}

		public OpenContentMB GetByOpenCommandType(OpenCommandType type)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public OpenContentMB GetByOpenContentTypeAndCommandType(OpenContentType contentType, OpenCommandType commandType)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public OpenContentMB GetByTutorialId(long tutorialId)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public OpenContentTable()
		{
		}
	}
}
