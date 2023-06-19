namespace MementoMori.Ortega.Share.Data.ApiInterface
{
	public class OrtegaAuthAttribute : Attribute
	{
		public bool IsIgnoreMaintenance
		{
			get;
		}

		public bool IsRequiredLogin
		{
			get;
		}

		public string Uri
		{
			get;
		}

		public OrtegaAuthAttribute(string uri, bool isRequiredLogin = true, bool isIgnoreMaintenance = false)
		{
			this.Uri = uri;
			this.IsRequiredLogin = isRequiredLogin;
			this.IsIgnoreMaintenance = isIgnoreMaintenance;
		}
	}
}
