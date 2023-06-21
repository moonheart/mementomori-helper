using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
	[MessagePackObject(true)]
	public class CharacterRarityCoefficientInfo
	{
		public double m
		{
			get;
			set;
		}

		public long b
		{
			get;
			set;
		}

		public CharacterRarityCoefficientInfo()
		{
		}
	}
}
