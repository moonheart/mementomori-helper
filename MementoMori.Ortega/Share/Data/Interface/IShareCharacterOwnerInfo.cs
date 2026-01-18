namespace MementoMori.Ortega.Share.Data.Interface
{
	public interface IShareCharacterOwnerInfo
	{
		IPlayerIconInfo PlayerIconInfo { get; }

		string PlayerName { get; }

		long WorldId { get; }
	}
}
