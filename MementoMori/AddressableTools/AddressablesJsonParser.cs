using MementoMori.AddressableTools.Catalog;
using MementoMori.AddressableTools.JSON;
using Newtonsoft.Json;

namespace MementoMori.AddressableTools
{
    public static class AddressablesJsonParser
    {
        internal static ContentCatalogDataJson CCDJsonFromString(string data)
        {
            return JsonConvert.DeserializeObject<ContentCatalogDataJson>(data);
        }

        public static ContentCatalogData FromString(string data)
        {
            var ccdJson = CCDJsonFromString(data);

            var catalogData = new ContentCatalogData();
            catalogData.Read(ccdJson);

            return catalogData;
        }
    }
}