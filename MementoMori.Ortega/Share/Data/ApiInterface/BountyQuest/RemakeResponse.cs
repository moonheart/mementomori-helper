using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
	[MessagePackObject(true)]
	public class RemakeResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<BountyQuestInfo> BountyQuestInfos { get; set; }

		public List<UserBountyQuestDtoInfo> UserBountyQuestDtoInfos { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
