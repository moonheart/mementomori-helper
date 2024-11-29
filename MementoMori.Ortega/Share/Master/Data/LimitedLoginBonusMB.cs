using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[MessagePackObject(true)]
[Description("期間限定ログインボーナス")]
public class LimitedLoginBonusMB : MasterBookBase, IHasStartEndTime
{
    [SerializationConstructor]
    public LimitedLoginBonusMB(long id, bool? isIgnore, string memo, string startTime, string endTime, int delayDays, long rewardListId, int characterImageId, int rewardBackgroundImageId,
        int specialRewardBackgroundImageId, string titleTextKey, string appealTextKey, string specialRewardAppealTextKey, string specialRewardLabelTextColor,
        string specialRewardLabelTextOutlineColor, string specialRewardCountTextColor, MypageIconDisplayLocationType mypageIconDisplayLocationType, float characterImageX, float characterImageY,
        float characterImageSize)
        : base(id, isIgnore, memo)
    {
        StartTime = startTime;
        EndTime = endTime;
        DelayDays = delayDays;
        RewardListId = rewardListId;
        CharacterImageId = characterImageId;
        RewardBackgroundImageId = rewardBackgroundImageId;
        SpecialRewardBackgroundImageId = specialRewardBackgroundImageId;
        TitleTextKey = titleTextKey;
        AppealTextKey = appealTextKey;
        SpecialRewardAppealTextKey = specialRewardAppealTextKey;
        SpecialRewardLabelTextColor = specialRewardLabelTextColor;
        SpecialRewardLabelTextOutlineColor = specialRewardLabelTextOutlineColor;
        SpecialRewardCountTextColor = specialRewardCountTextColor;
        MypageIconDisplayLocationType = mypageIconDisplayLocationType;
        CharacterImageX = characterImageX;
        CharacterImageY = characterImageY;
        CharacterImageSize = characterImageSize;
    }

    public LimitedLoginBonusMB() : base(0L, false, "")
    {
    }

    [PropertyOrder(9)]
    [Description("訴求文言")]
    public string AppealTextKey { get; }

    [PropertyOrder(5)]
    [Description("キャラ画像Id")]
    public int CharacterImageId { get; }

    [PropertyOrder(3)]
    [Description("猶予日数")]
    public int DelayDays { get; }

    [PropertyOrder(6)]
    [Description("報酬背景画像ID")]
    public int RewardBackgroundImageId { get; }

    [PropertyOrder(4)]
    [Description("報酬リストID")]
    public long RewardListId { get; }

    [PropertyOrder(10)]
    [Description("特別報酬訴求文言")]
    public string SpecialRewardAppealTextKey { get; }

    [PropertyOrder(7)]
    [Description("特別報酬背景画像ID")]
    public int SpecialRewardBackgroundImageId { get; }

    [PropertyOrder(13)]
    [Description("特別報酬カウントテキスト色")]
    public string SpecialRewardCountTextColor { get; }

    [PropertyOrder(11)]
    [Description("特別報酬ラベル色")]
    public string SpecialRewardLabelTextColor { get; }

    [PropertyOrder(12)]
    [Description("特別報酬ラベルアウトライン色")]
    public string SpecialRewardLabelTextOutlineColor { get; }

    [PropertyOrder(8)]
    [Description("タイトル")]
    public string TitleTextKey { get; }

    [PropertyOrder(14)]
    [Description("アイコン表示箇所")]
    public MypageIconDisplayLocationType MypageIconDisplayLocationType { get; }

    [PropertyOrder(15)]
    [Description("キャラ画像座標X")]
    public float CharacterImageX { get; }

    [PropertyOrder(16)]
    [Description("キャラ画像座標Y")]
    public float CharacterImageY { get; }

    [PropertyOrder(17)]
    [Description("キャラ画像サイズ")]
    public float CharacterImageSize { get; }

    [PropertyOrder(1)]
    [Description("開始日時")]
    public string StartTime { get; }

    [Description("終了日時")]
    [PropertyOrder(2)]
    public string EndTime { get; }
}