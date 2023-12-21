using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Ortega.Share;

namespace MementoMori.BlazorShared.Extensions
{
    public static class MasterResourceExtension
    {
        public static string Tr(string? key)
        {
            return Masters.TextResourceTable.Get(key);
        }
        public static string Tr(string? key, params object[] param)
        {
            return Masters.TextResourceTable.Get(key, param);
        }
        public static string Tr<T>(T enumValue) where T : Enum
        {
            return Masters.TextResourceTable.Get(enumValue);
        }
        public static string Tr<T>(T enumValue, params object[] param) where T : Enum
        {
            return Masters.TextResourceTable.Get(enumValue, param);
        }
    }
}
