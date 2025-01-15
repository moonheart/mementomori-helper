using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.GuildTower;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ギルドツリーイベント")]
    public class GuildTowerEventMB : MasterBookBase, IHasStartEndTime
    {
        [PropertyOrder(9)]
        [Description("コンボ加算時間(分単位)")]
        public int ComboAddTime { get; }

        [PropertyOrder(7)]
        [Description("コンボ数上限")]
        public int ComboMaxCount { get; }

        [PropertyOrder(8)]
        [Description("コンボ上限時間(分単位)")]
        public int ComboMaxTime { get; }

        [DateTimeString]
        [PropertyOrder(4)]
        [Description("バナー表示終了日時")]
        public string DisplayBannerEndTime { get; set; }

        [PropertyOrder(1)]
        [Description("イベント番号")]
        public int EventNo { get; }

        [Nest(false, 0)]
        [PropertyOrder(10)]
        [Description("コンボボーナスリスト")]
        public IReadOnlyList<GuildTowerComboBonus> GuildTowerComboBonusList { get; }

        [Nest(false, 0)]
        [PropertyOrder(11)]
        [Description("回数デバフリスト")]
        public IReadOnlyList<GuildTowerDebuffParameter> GuildTowerDebuffParameterList { get; }

        [PropertyOrder(5)]
        [Description("1日の最大挑戦回数")]
        public int MaxChallengeCount { get; }

        [PropertyOrder(12)]
        [Description("ミッションIdリスト")]
        public IReadOnlyList<long> MissionIdList { get; }

        [PropertyOrder(6)]
        [Description("登録可能キャラ数")]
        public int RegisterCharacterCount { get; }

        [SerializationConstructor]
        public GuildTowerEventMB(long id, bool? isIgnore, string memo, int eventNo, string startTime, string endTime, string displayBannerEndTime, int maxChallengeCount, int registerCharacterCount,
            int comboMaxCount, int comboMaxTime, int comboAddTime, IReadOnlyList<GuildTowerComboBonus> guildTowerComboBonusList, IReadOnlyList<GuildTowerDebuffParameter> guildTowerDebuffParameterList,
            IReadOnlyList<long> missionIdList)
            : base(id, isIgnore, memo)
        {
            EventNo = eventNo;
            StartTime = startTime;
            EndTime = endTime;
            DisplayBannerEndTime = displayBannerEndTime;
            MaxChallengeCount = maxChallengeCount;
            RegisterCharacterCount = registerCharacterCount;
            ComboMaxCount = comboMaxCount;
            ComboMaxTime = comboMaxTime;
            ComboAddTime = comboAddTime;
            GuildTowerComboBonusList = guildTowerComboBonusList;
            GuildTowerDebuffParameterList = guildTowerDebuffParameterList;
            MissionIdList = missionIdList;
        }

        public GuildTowerEventMB() : base(default, default, default)
        {
        }

        [PropertyOrder(2)]
        [Description("開始日時(現地時間)")]
        public string StartTime { get; }

        [PropertyOrder(3)]
        [Description("終了日時(現地時間)")]
        public string EndTime { get; }

        public GuildTowerComboBonus GetComboBonus(int comboCount)
        {
            // IReadOnlyList<GuildTowerComboBonus> readOnlyList = this.<GuildTowerComboBonusList>k__BackingField;
            // int num = 0;
            // IReadOnlyList<GuildTowerComboBonus> readOnlyList2 = this.<GuildTowerComboBonusList>k__BackingField;
            // uint num2;
            // if (num < (int)num2)
            // {
            // 	num += num;
            // 	if (num == (int)num2)
            // 	{
            // 		goto IL_23;
            // 	}
            // 	num++;
            // }
            // int num3 = 0;
            // IL_23:
            // num3 += num3;
            // num3 += 312;
            throw new NullReferenceException();
        }
    }
}