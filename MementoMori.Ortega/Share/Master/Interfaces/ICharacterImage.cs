using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Interfaces
{
	public interface ICharacterImage
	{
		[Description("キャラ画像Id")]
		long CharacterImageId
		{
			get;
		}

		[Description("キャラ画像座標X")]
		float CharacterImageX
		{
			get;
		}

		[Description("キャラ画像座標Y")]
		float CharacterImageY
		{
			get;
		}

		[Description("キャラ画像サイズ")]
		float CharacterImageSize
		{
			get;
		}
	}
}
