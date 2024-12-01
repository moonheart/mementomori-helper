using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[Description("コンテンツ開放")]
[MessagePackObject(true)]
public class OpenContentMB : MasterBookBase
{
    [SerializationConstructor]
    public OpenContentMB(long id, bool? isIgnore, string memo, string assetPath, string descriptionKey, OpenContentType openContentType, long openContentValue, OpenCommandType openCommandType,
        bool isActiveGuide, int guideOrderNumber, string guideDescriptionKey, long guideStartValue, string toastKey, long tutorialId)
        : base(id, isIgnore, memo)
    {
        AssetPath = assetPath;
        DescriptionKey = descriptionKey;
        GuideDescriptionKey = guideDescriptionKey;
        GuideOrderNumber = guideOrderNumber;
        GuideStartValue = guideStartValue;
        IsActiveGuide = isActiveGuide;
        OpenCommandType = openCommandType;
        OpenContentType = openContentType;
        OpenContentValue = openContentValue;
        ToastKey = toastKey;
        TutorialId = tutorialId;
    }

    public OpenContentMB() : base(0L, false, "")
    {
    }

    [PropertyOrder(5)]
    [Description("演出パス")]
    public string AssetPath { get; }

    [PropertyOrder(4)]
    [Description("開放内容キー")]
    public string DescriptionKey { get; }

    [PropertyOrder(9)]
    [Description("ガイド説明文")]
    public string GuideDescriptionKey { get; }

    [PropertyOrder(8)]
    [Description("ガイド表示優先順")]
    public int GuideOrderNumber { get; }

    [PropertyOrder(2)]
    [Description("ガイド表示開始値")]
    public long GuideStartValue { get; }

    [PropertyOrder(7)]
    [Description("ガイド表示タイプ")]
    public bool IsActiveGuide { get; }

    [PropertyOrder(6)]
    [Description("解放されるコマンドの種類")]
    public OpenCommandType OpenCommandType { get; }

    [PropertyOrder(1)]
    [Description("コンテンツ開放タイプ")]
    public OpenContentType OpenContentType { get; }

    [PropertyOrder(3)]
    [Description("コンテンツ開放値")]
    public long OpenContentValue { get; }

    [PropertyOrder(10)]
    [Description("トースト")]
    public string ToastKey { get; }

    [PropertyOrder(11)]
    [Description("チュートリアルID")]
    public long TutorialId { get; }
}