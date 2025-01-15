using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo;

[MessagePackObject(true)]
public class UserSyncGvgDeckDtoInfo
{
    public long PlayerId { get; set; }

    public long EndIntervalTimestamp { get; set; }

    public DeckUseContentType SelectedDeckType { get; set; }
}