using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    public enum BookSortAssistanceQuestStatusType
    {
        [Description("不明")]
        None,
        [Description("報酬未受取")]
        NotReceived,
        [Description("未派遣")]
        NotDispatched,
        [Description("未解放")]
        Locked,
        [Description("派遣中")]
        Dispatched,
        [Description("報酬受取済み")]
        Received
    }
}