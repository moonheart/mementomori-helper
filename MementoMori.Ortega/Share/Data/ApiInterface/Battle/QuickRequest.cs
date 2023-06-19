using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/quick", true, false)]
	public class QuickRequest : ApiRequestBase
	{
		public QuestQuickExecuteType QuestQuickExecuteType
		{
			get;
			set;
		}

		public int QuickCount
		{
			get;
			set;
		}

		public QuickRequest()
		{
		}
	}
}
