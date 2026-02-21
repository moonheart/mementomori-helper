using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Present
{
	[MessagePackObject(true)]
	[OrtegaApi("present/deletePresent", true, false)]
	public class DeletePresentRequest : ApiRequestBase
	{
        public List<string> PresentGuidList { get; set; }
    }
}
