using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserOpenContentDtoInfo
	{
		public long OpenContentId
		{
			get;
			set;
		}

		public UserOpenContentDtoInfo()
		{
		}
	}
}
