using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
    [MessagePackObject(true)]
    [OrtegaApi("gacha/setSelectList", true, false)]
    public class SetSelectListRequest : ApiRequestBase
    {
        public List<long> CharacterIdList { get; set; }

        public long GachaCaseId { get; set; }

        public GachaSelectListType GachaSelectListType { get; set; }
    }
}