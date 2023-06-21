using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class CustomTextLayoutInfo
	{
		public int BannerAlignment
		{
			get;
			set;
		}

		public int BannerFontSize
		{
			get;
			set;
		}

		public float BannerLetterSpacing
		{
			get;
			set;
		}

		public float BannerLineSpacing
		{
			get;
			set;
		}

		public string BannerOutlineColor
		{
			get;
			set;
		}

		public float BannerPositionX
		{
			get;
			set;
		}

		public float BannerPositionY
		{
			get;
			set;
		}

		public int TitleFontSize
		{
			get;
			set;
		}

		public CustomTextLayoutInfo()
		{
		}
	}
}
