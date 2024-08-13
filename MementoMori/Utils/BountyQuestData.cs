using MementoMori.Ortega.Share.Data.BountyQuest;

namespace MementoMori.Utils;

public class BountyQuestData
{
    public BountyQuestData(BountyQuestInfo questInfo)
    {
        ElementTypes = new List<ElementType>();
        QuestInfo = questInfo;
        foreach (var conditionInfo in questInfo.BountyQuestConditionInfos)
        {
            if (conditionInfo.BountyQuestConditionType == BountyQuestConditionType.Rarity)
            {
                Rarity = conditionInfo.Rarity;
                RarityRequireCount = conditionInfo.RequireCount;
            }
            else
                ElementTypes.AddRange(Enumerable.Repeat(conditionInfo.ElementType, conditionInfo.RequireCount));
        }
    }

    public BountyQuestInfo QuestInfo { get; }

    /// <summary>
    ///     The rarity of the character required to complete the quest.
    /// </summary>
    public CharacterRarityFlags Rarity { get; }

    /// <summary>
    ///     The number of specific rarity characters required to complete the quest.
    /// </summary>
    public int RarityRequireCount { get; }

    /// <summary>
    ///     The element types of the characters required to complete the quest.
    /// </summary>
    public List<ElementType> ElementTypes { get; }
}