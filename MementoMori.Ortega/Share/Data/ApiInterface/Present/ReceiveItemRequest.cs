using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Present
{
    [MessagePackObject(true)]
    [OrtegaApi("present/receiveItem", true, false)]
    public class ReceiveItemRequest : ApiRequestBase
    {
        public LanguageType LanguageType { get; set; }

        public string PresentGuid { get; set; }
    }
}