using System;
using TypeGen.Core.Converters;

namespace MementoMori.Api.TypeGen
{
    public class OrtegaTypeNameConverter : ITypeNameConverter
    {
        public string Convert(string name, Type type)
        {
            const string apiNsBase = "MementoMori.Ortega.Share.Data.ApiInterface";
            const string commonEnumBase = "MementoMori.Ortega.Common.Enums";
            
            if (type.Namespace != null && type.Namespace.StartsWith(apiNsBase) && type.Namespace.Length > apiNsBase.Length)
            {
                var subNs = type.Namespace.Substring(apiNsBase.Length).TrimStart('.');
                if (!string.IsNullOrEmpty(subNs))
                {
                    var parts = subNs.Split('.');
                    return parts[0] + name;
                }
            }
            else if (type.Namespace != null && type.Namespace.StartsWith(commonEnumBase))
            {
                return $"Common{name}";
            }
                
            return name;
        }
    }
}
