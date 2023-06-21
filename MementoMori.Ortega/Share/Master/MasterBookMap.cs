using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Master
{
	[MessagePackObject(true)]
	public class MasterBookMap
	{
		public Dictionary<long, byte[]> MBMap { get; set; }

		public MasterBookMap()
		{
		}
	}
}
