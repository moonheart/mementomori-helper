using MementoMori.Ortega.Share.Master;

namespace MementoMori.Ortega
{
	public interface IMasterProvider
	{
		M GetById<M>(long id) where M : MasterBookBase;

		List<M> GetAll<M>() where M : MasterBookBase;
	}
}
