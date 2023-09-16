using System.ComponentModel;
using System.Runtime.InteropServices;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Master.Table;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("キャラクター")]
    public class CharacterMB : MasterBookBase, IHasJstStartEndTime
    {
        [PropertyOrder(11)]
        [Description("アクティブスキルIDのリスト")]
        public IReadOnlyList<long> ActiveSkillIds { get; }

        [Description("潜在能力配分係数")]
        [Nest(false, 0)]
        [PropertyOrder(8)]
        public BaseParameter BaseParameterCoefficient { get; }

        [Description("潜在能力配分合計係数")]
        [PropertyOrder(9)]
        public int BaseParameterGrossCoefficient { get; }

        [Description("二つ名タイプ")]
        [PropertyOrder(3)]
        public CharacterType CharacterType { get; }

        [PropertyOrder(4)]
        [Description("属性")]
        public ElementType ElementType { get; }

        [Nest(false, 0)]
        [PropertyOrder(7)]
        [Description("初期バトルパラメータ")]
        public BattleParameter InitialBattleParameter { get; }

        [PropertyOrder(15)]
        [Description("アイテム時レアリティ")]
        public ItemRarityFlags ItemRarityFlags { get; }

        [PropertyOrder(5)]
        [Description("職業")]
        public JobFlags JobFlags { get; }

        [PropertyOrder(2)]
        [Description("名称(二つ名)キー")]
        public string Name2Key { get; }

        [PropertyOrder(1)]
        [Description("名称キー")]
        public string NameKey { get; }

        [Description("通常攻撃のID")]
        [PropertyOrder(10)]
        public long NormalSkillId { get; }

        [PropertyOrder(12)]
        [Description("パッシブスキルIDのリスト")]
        public IReadOnlyList<long> PassiveSkillIds { get; }

        [Description("レアリティ")]
        [PropertyOrder(6)]
        public CharacterRarityFlags RarityFlags { get; }

        [PropertyOrder(13)]
        [Description("解放に必要な絆の数")]
        public long RequireFragmentCount { get; }

        [Description("新キャラの図鑑表示終了時間(JST)")]
        [PropertyOrder(17)]
        public string EndTimeFixJST { get; }

        [Description("新キャラの図鑑表示開始時間(JST)")]
        [PropertyOrder(16)]
        public string StartTimeFixJST { get; }

        [SerializationConstructor]
        public CharacterMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> activeSkillIds, BaseParameter baseParameterCoefficient, int baseParameterGrossCoefficient,
            CharacterType characterType, ElementType elementType, BattleParameter initialBattleParameter, ItemRarityFlags itemRarityFlags, JobFlags jobFlags, string nameKey, string name2Key,
            long normalSkillId, IReadOnlyList<long> passiveSkillIds, CharacterRarityFlags rarityFlags, long requireFragmentCount, string endTimeFixJST, string startTimeFixJST)
            : base(id, isIgnore, memo)
        {
            this.ActiveSkillIds = activeSkillIds;
            this.BaseParameterCoefficient = baseParameterCoefficient;
            this.BaseParameterGrossCoefficient = baseParameterGrossCoefficient;
            this.CharacterType = characterType;
            this.ElementType = elementType;
            this.InitialBattleParameter = initialBattleParameter;
            this.ItemRarityFlags = itemRarityFlags;
            this.JobFlags = jobFlags;
            this.NameKey = nameKey;
            this.Name2Key = name2Key;
            this.NormalSkillId = normalSkillId;
            this.PassiveSkillIds = passiveSkillIds;
            this.RarityFlags = rarityFlags;
            this.RequireFragmentCount = requireFragmentCount;
            this.EndTimeFixJST = endTimeFixJST;
            this.StartTimeFixJST = startTimeFixJST;
        }

        public CharacterMB() : base(0L, false, "")
        {
        }
        
        public void GetCharacterName(out string name1, out string name2)
        {
            name1 = Masters.TextResourceTable.Get(this.NameKey);
            name2 = Masters.TextResourceTable.Get(this.Name2Key);
        }

        public string GetCombinedName([Optional] string delimiter)
        {
            GetCharacterName(out var name1, out var name2);
            if (string.IsNullOrEmpty(name2))
            {
                return name1;
            }
            return $"{name1}{delimiter}{name2}";
        }

    }
}