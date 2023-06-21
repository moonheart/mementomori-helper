using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Master
{
	[MessagePackObject(true)]
	public class MasterBookCatalog
	{
		public Dictionary<string, MasterBookInfo> MasterBookInfoMap { get; set; }

		public MasterBookCatalog()
		{
			Dictionary<string, MasterBookInfo> dictionary = new Dictionary<string, MasterBookInfo>();
			this.MasterBookInfoMap = dictionary;
		}
	}
}
