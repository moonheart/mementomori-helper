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

    public CharacterRarityFlags Rarity { get; }

    public int RarityRequireCount { get; }

    public List<ElementType> ElementTypes { get; }
}