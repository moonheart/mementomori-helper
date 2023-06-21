using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GachaDestinyAddCharacterTable : TableBase<GachaDestinyAddCharacterMB>
	{
		public IReadOnlyList<GachaDestinyAddCharacterMB> GetByInTime(DateTime jstNowDateTime)
		{
			// List<GachaDestinyAddCharacterMB> list = new List();
			// int num = 0;
			// GachaSelectListTable GachaSelectListTable = Masters.GachaSelectListTable;
			// GachaSelectListTable GachaSelectListTable2 = Masters.GachaSelectListTable;
			// GachaSelectListMB gachaSelectListMB;
			// GachaSelectListMB gachaSelectListMB2;
			// if (gachaSelectListMB != 0 && gachaSelectListMB2 != 0 && gachaSelectListMB.StartTimeFixJST.ToDateTime() <= jstNowDateTime)
			// {
			// 	DateTime dateTime = gachaSelectListMB2.EndTimeFixJST.ToDateTime();
			// 	if (jstNowDateTime <= dateTime)
			// 	{
			// 	}
			// }
			// num++;
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public bool IsNew(DateTime jstNowDateTime, long characterId)
		{
			// IReadOnlyList<GachaDestinyAddCharacterMB> readOnlyList = this.GetByInTime(jstNowDateTime);
			// int num = 0;
			// uint num2;
			// if (num < (int)num2)
			// {
			// 	num += num;
			// 	if (num == (int)num2)
			// 	{
			// 		goto IL_1C;
			// 	}
			// 	num++;
			// }
			// int num3 = 0;
			// IL_1C:
			// readOnlyList += readOnlyList;
			// num3 += 312;
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public GachaDestinyAddCharacterTable()
		{
		}
	}
}
