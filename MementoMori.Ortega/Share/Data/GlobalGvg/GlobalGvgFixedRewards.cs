using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GlobalGvg
{
	[MessagePackObject(true)]
	public class GlobalGvgFixedRewards
	{
		[Nest(true, 1)]
		public IReadOnlyList<UserItem> LowerClass
		{
			get;
			set;
		}

		[Nest(true, 1)]
		public IReadOnlyList<UserItem> MediumClass
		{
			get;
			set;
		}

		[Nest(true, 1)]
		public IReadOnlyList<UserItem> UpperClass
		{
			get;
			set;
		}

		public GlobalGvgFixedRewards()
		{
		}
	}
}
