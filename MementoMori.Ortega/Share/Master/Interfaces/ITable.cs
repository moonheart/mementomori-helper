namespace MementoMori.Ortega.Share.Master.Interfaces
{
	public interface ITable
	{
		bool Load();

        bool Load(byte[] binaryData);

		string GetMasterBookName();
	}
}
