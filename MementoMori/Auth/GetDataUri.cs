namespace MementoMori;

public class GetDataUri
{
    public class Req
    {
        public string CountryCode { get; set; }
        public long UserId { get; set; }
    }

    public class Resp
    {
        public AppAssetVersionInfo AppAssetVersionInfo { get; set; }
        public WorldInfos[] WorldInfos { get; set; }
        public MaintenanceDebugUserInfos[] MaintenanceDebugUserInfos { get; set; }
        public MaintenanceInfos[] MaintenanceInfos { get; set; }
        public ManagementNewUserInfos[] ManagementNewUserInfos { get; set; }
        public string AssetCatalogUriFormat { get; set; }
        public string AssetCatalogFixedUriFormat { get; set; }
        public string MasterUriFormat { get; set; }
        public string NoticeBannerImageUriFormat { get; set; }
        public TitleInfo TitleInfo { get; set; }
    }

    public class AppAssetVersionInfo
    {
        public int EnvType { get; set; }
        public bool IsSkipAssetDownload { get; set; }
        public string Version { get; set; }
    }

    public class WorldInfos
    {
        public int GameServerId { get; set; }
        public int Id { get; set; }
        public string StartTime { get; set; }
    }

    public class MaintenanceDebugUserInfos
    {
        public long UserId { get; set; }
        public long PlayerId { get; set; }
        public bool IsDebugUser { get; set; }
    }

    public class MaintenanceInfos
    {
        public int MaintenanceServerType { get; set; }
        public string StartTimeFixJST { get; set; }
        public string EndTimeFixJST { get; set; }
        public int[] MaintenancePlatformTypes { get; set; }
        public int MaintenanceAreaType { get; set; }
        public int[] AreaIds { get; set; }
        public int[] MaintenanceFunctionTypes { get; set; }
    }

    public class ManagementNewUserInfos
    {
        public string EndTimeFixJST { get; set; }
        public bool IsUnableToCreateUser { get; set; }
        public int ManagementNewUserType { get; set; }
        public string StartTimeFixJST { get; set; }
        public int[] TargetIds { get; set; }
    }

    public class TitleInfo
    {
        public int BgmNumberJP { get; set; }
        public int BgmNumberUS { get; set; }
        public int MovieNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Scale { get; set; }
    }
}