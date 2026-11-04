using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class CustomTextSimpleLayoutInfo
	{
		public float BannerPositionX { get; set; }

		public float BannerPositionY { get; set; }
	}
}
