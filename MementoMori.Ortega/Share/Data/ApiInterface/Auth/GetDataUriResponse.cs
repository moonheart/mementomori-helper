using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Data.Title;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth
{
	[MessagePackObject(true)]
	public class GetDataUriResponse : ApiResponseBase
	{
		public AppAssetVersionInfo AppAssetVersionInfo { get; set; }

		public List<WorldInfo> WorldInfos { get; set; }

		public List<MaintenanceDebugUserInfo> MaintenanceDebugUserInfos { get; set; }

		public List<MaintenanceInfo> MaintenanceInfos { get; set; }

		public List<ManagementNewUserInfo> ManagementNewUserInfos { get; set; }

		public string AssetCatalogUriFormat { get; set; }

		public string AssetCatalogFixedUriFormat { get; set; }

		public string MasterUriFormat { get; set; }

		public string NoticeBannerImageUriFormat { get; set; }
        
        public string RawDataUriFormat { get; set; }

		public TitleInfo TitleInfo { get; set; }

		public GetDataUriResponse()
		{
		}
	}
}
