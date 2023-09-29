namespace MementoMori.Ortega.Share.Master
{
	public static class MasterBookExtensions
	{
		public static M FindById<M>(this IEnumerable<M> source, long id) where M : MasterBookBase
		{
			return source.First(x => x.Id == id);
		}

		public static M FindByIdOrDefault<M>(this IEnumerable<M> source, long id) where M : MasterBookBase
		{
			return source.FirstOrDefault(x => x.Id == id);
		}

		public static List<M> FindManyByIds<M>(this IEnumerable<M> source, List<long> ids) where M : MasterBookBase
		{
			return source.Where(x => ids.Contains(x.Id)).ToList();
		}

		public static Dictionary<long, M> GetAllMap<M>(this IMasterProvider master) where M : MasterBookBase
		{
			// int num = 0;
			// uint num2;
			// if (num < (int)num2)
			// {
			// 	num += num;
			// 	if (num == (int)num2)
			// 	{
			// 		goto IL_16;
			// 	}
			// 	num++;
			// }
			// if (master == 0)
			// {
			// }
			// IL_16:
			throw new NullReferenceException();
		}

		public static TMasterBook TryGetMB<TMasterBook>(this Dictionary<long, TMasterBook> source, long id) where TMasterBook : MasterBookBase
		{
			throw new NullReferenceException();
		}
	}
}
