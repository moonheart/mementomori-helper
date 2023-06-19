namespace MementoMori.Ortega.Share.Data.ApiInterface
{
    public class OrtegaApiAttribute : Attribute
    {
        public bool IsIgnoreMaintenance { get; }

        public bool IsRequiredLogin { get; }

        public string Uri { get; }

        public OrtegaApiAttribute(string uri, bool isRequiredLogin = true, bool isIgnoreMaintenance = false)
        {
            this.Uri = uri;
            this.IsRequiredLogin = isRequiredLogin;
            this.IsIgnoreMaintenance = isIgnoreMaintenance;
        }
    }
}