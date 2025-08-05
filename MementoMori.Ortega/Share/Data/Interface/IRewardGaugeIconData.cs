namespace MementoMori.Ortega.Share.Data.Interface
{
	public interface IRewardGaugeIconData
	{
		long GetRequireCount();

		int GetDisplayCount();

		bool IsReceived();
	}
}
