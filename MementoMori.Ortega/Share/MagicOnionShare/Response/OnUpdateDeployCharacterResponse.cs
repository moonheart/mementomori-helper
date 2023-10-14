using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnUpdateDeployCharacterResponse
	{
		[Key(2)]
		public int MatchingNumber { get; set; }

		[Key(1)]
		public int MinCharacterNum { get; set; }

		[Key(0)]
		public List<PartyCharacterInfo> PartyCharacterInfos { get; set; }
	}
}
