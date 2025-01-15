using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Item;

[MessagePackObject(true)]
[OrtegaApi("item/openTreasureChest")]
public class OpenTreasureChestRequest : ApiRequestBase
{
    public int OpenCount { get; set; }

    public long TreasureChestId { get; set; }

    public int SelectedTreasureItemListIndex { get; set; }

    public ItemType SelectedItemType { get; set; }

    public long SelectedItemId { get; set; }

    public long SelectedCharacterId { get; set; }
}