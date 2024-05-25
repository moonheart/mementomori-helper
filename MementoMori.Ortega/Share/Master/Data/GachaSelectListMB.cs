using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("ガチャセレクトリスト")]
    [MessagePackObject(true)]
    public class GachaSelectListMB : MasterBookBase, IHasJstStartEndTime
    {
        [PropertyOrder(6)]
        [Description("設定可能キャラクターIDリスト")]
        public IReadOnlyList<long> CharacterIdList { get; }

        [PropertyOrder(1)]
        [Description("ガチャセレクトリストタイプ")]
        public GachaSelectListType GachaSelectListType { get; }

        [PropertyOrder(2)]
        [Description("設定キャラリセット有無")]
        public bool IsResetSelectedCharacter { get; set; }

        [PropertyOrder(7)]
        [Description("新規キャラクターIDリスト")]
        public IReadOnlyList<long> NewCharacterIdList { get; }

        [PropertyOrder(5)]
        [Description("優先度")]
        public int OrderNumber { get; set; }

        [SerializationConstructor]
        public GachaSelectListMB(long id, bool? isIgnore, string memo, GachaSelectListType gachaSelectListType, bool isResetSelectedCharacter, string startTimeFixJST, string endTimeFixJST, int orderNumber, IReadOnlyList<long> characterIdList, IReadOnlyList<long> newCharacterIdList)
            : base(id, isIgnore, memo)
        {
            GachaSelectListType = gachaSelectListType;
            IsResetSelectedCharacter = isResetSelectedCharacter;
            StartTimeFixJST = startTimeFixJST;
            EndTimeFixJST = endTimeFixJST;
            OrderNumber = orderNumber;
            CharacterIdList = characterIdList;
            NewCharacterIdList = newCharacterIdList;
        }

        public GachaSelectListMB() : base(0, false, "")
        {
        }

        [PropertyOrder(3)]
        [Description("開始時間(JST)")]
        public string StartTimeFixJST { get; }

        [PropertyOrder(4)]
        [Description("終了時間(JST)")]
        public string EndTimeFixJST { get; }
    }
}