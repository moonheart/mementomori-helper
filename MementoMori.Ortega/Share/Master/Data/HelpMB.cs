using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Help;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("ヘルプ")]
    [MessagePackObject(true)]
    public class HelpMB : MasterBookBase
    {
        [Nest(true, 1)]
        [PropertyOrder(2)]
        [Description("ヘルプパートリスト")]
        public IReadOnlyList<HelpPartInfo> HelpPartInfoList { get; }

        [PropertyOrder(1)]
        [Description("ヘルプタイトル")]
        public string HelpTitle { get; }

        [PropertyOrder(5)]
        [Description("表示フラグ")]
        public bool IsDisplayed { get; }

        [PropertyOrder(3)]
        [Description("チュートリアル説明ID")]
        public long TutorialDescriptionId { get; }

        [PropertyOrder(4)]
        [Description("ダイアログタイプ")]
        public long DialogType { get; }

        [PropertyOrder(6)]
        [Description("表示デバイスタイプ")]
        public IReadOnlyList<int> DisplayDeviceTypes { get; }

        [SerializationConstructor]
        public HelpMB(long id, bool? isIgnore, string memo, string helpTitle, IReadOnlyList<HelpPartInfo> helpPartInfoList, long tutorialDescriptionId, long dialogType, bool isDisplayed, IReadOnlyList<int> displayDeviceTypes)
            : base(id, isIgnore, memo)
        {
            HelpTitle = helpTitle;
            HelpPartInfoList = helpPartInfoList;
            TutorialDescriptionId = tutorialDescriptionId;
            DialogType = dialogType;
            IsDisplayed = isDisplayed;
            DisplayDeviceTypes = displayDeviceTypes;
        }

        public HelpMB() : base(0, false, "")
        {
        }
    }
}