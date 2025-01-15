using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat;

[MessagePackObject(true)]
public class ChatSettingData : IDeepCopy<ChatSettingData>
{
    public long BalloonItemId { get; set; }

    public int FontSize { get; set; }

    public Dictionary<ChatType, ChatBackgroundType> BackgroundTypeDictionary { get; set; }

    public ChatSettingData DeepCopy()
    {
        return null;
    }
}