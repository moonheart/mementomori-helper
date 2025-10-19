using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Chat;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[MessagePackObject(true)]
[Description("チャットスタンプ")]
public class ChatEmoticonMB : MasterBookBase
{
    [Nest(false, 0)]
    [PropertyOrder(1)]
    [Description("絵文字リスト")]
    public IReadOnlyList<Emoticon> EmoticonList { get; }

    [SerializationConstructor]
    public ChatEmoticonMB(long id, bool? isIgnore, string memo, IReadOnlyList<Emoticon> emoticonList)
        : base(id, isIgnore, memo)
    {
        EmoticonList = emoticonList;
    }

    public ChatEmoticonMB() : base(0, null, "")
    {
    }
}