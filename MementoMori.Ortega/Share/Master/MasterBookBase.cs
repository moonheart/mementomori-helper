namespace MementoMori.Ortega.Share.Master
{
	public abstract class MasterBookBase
	{
		public long Id
		{
			get;
		}

		public bool? IsIgnore
		{
			get;
		}

		public string Memo
		{
			get;
		}

		protected MasterBookBase(long id, bool? isIgnore, string memo)
		{
			Id = id;
			IsIgnore = isIgnore;
			Memo = memo;
		}
	}
}
