using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Utils;

public static class BountyQuestAutoFormationUtil
{
    public static List<BountyQuestStartInfo> CalcAutoFormation(
        GetListResponse getListResponse,
        UserSyncData userSyncData,
        GameConfig.BountyQuestAutoModel bountyQuestConfig,
        bool force = false,
        bool diamondQuestOnly = false)
    {
        var bountyQuestStartInfos = new List<BountyQuestStartInfo>();
        // ongoing quests
        var ongoingQuest = getListResponse.UserBountyQuestDtoInfos.Where(d => d.BountyQuestEndTime > 0).Select(d => d.BountyQuestId).ToList();
        foreach (var bountyQuestInfos in getListResponse.BountyQuestInfos
                     .Where(d => !ongoingQuest.Contains(d.BountyQuestId)) // exclude ongoing quests
                     .Where(d => !diamondQuestOnly || (d.BountyQuestType == BountyQuestType.Solo && d.RewardItems.Exists(x => x.IsCurrency()))) // diamond quests only
                     .GroupBy(d => d.BountyQuestType))
        {
            // already used character guids
            var usedCharacterGuids = getListResponse.UserBountyQuestDtoInfos.Where(d => d.BountyQuestType == bountyQuestInfos.Key).SelectMany(d =>
                d.StartMembers.Select(x => x.UserCharacterGuid));

            // characters to be used
            var userCharacterDtoInfos = userSyncData.UserCharacterDtoInfos
                .Where(d => !usedCharacterGuids.Contains(d.Guid)) // exclude used characters
                .OrderBy(d => d.RarityFlags) // sort by rarity
                .ToList();
            // if guerrilla, sort by rarity and element type
            if (bountyQuestInfos.Key == BountyQuestType.Guerrilla)
                userCharacterDtoInfos = SortGuerrilla(userCharacterDtoInfos);

            // stored characters to be used, for checking if the character is already used
            var selectedCharacterGuids = new List<string>();
            foreach (var bountyQuestInfo in bountyQuestInfos)
            {
                // for solo quests, check if the reward item is included in the target items
                if (bountyQuestInfos.Key == BountyQuestType.Solo && // solo quest 
                    !force && // not force dispatch
                    bountyQuestConfig.TargetItems.Count > 0) // target items are specified
                {
                    // if the target items are not included in the reward items, skip
                    var found = bountyQuestConfig.TargetItems
                        .Any(includeItem => bountyQuestInfo.RewardItems.Exists(d => d.IsEqual(includeItem.ItemType, includeItem.ItemId)));

                    if (!found) continue;
                }

                // each character can only be used once in each bounty quest
                var selectedCharacterId = new List<long>();
                var bountyQuestData = new BountyQuestData(bountyQuestInfo);
                var requiredRarityCount = bountyQuestData.RarityRequireCount;
                var requiredElementTypes = bountyQuestData.ElementTypes.ToList();

                var questMemberInfos = new List<BountyQuestMemberInfo>();
                var bountyQuestStartInfo = new BountyQuestStartInfo {BountyQuestId = bountyQuestInfo.BountyQuestId, BountyQuestMemberInfos = questMemberInfos};
                // if team quest, get support members
                if (bountyQuestData.QuestInfo.BountyQuestType == BountyQuestType.Team)
                {
                    var userBountyQuestMemberDtoInfos = GetReadySupportMemberDtoInfos(getListResponse);
                    var supportMember = GetSupportMember(userBountyQuestMemberDtoInfos, bountyQuestData);
                    // if no support member found, skip  
                    if (supportMember != null)
                    {
                        var characterMb = CharacterTable.GetById(supportMember.CharacterId);
                        if (requiredRarityCount > 0) requiredRarityCount--;
                        // remove the element type from the list
                        requiredElementTypes.Remove(characterMb.ElementType);
                        selectedCharacterId.Add(supportMember.CharacterId);
                        questMemberInfos.Add(new BountyQuestMemberInfo
                        {
                            CharacterId = supportMember.CharacterId, CharacterRarityFlags = supportMember.RarityFlags, UserCharacterGuid = supportMember.UserCharacterGuid,
                            PlayerId = supportMember.PlayerId
                        });
                    }
                    else
                        continue;
                }

                // match the required element types and rarity
                do
                {
                    UserCharacterDtoInfo target = null;
                    if (requiredRarityCount > 0)
                        target = GetCharacterByRarity(userCharacterDtoInfos, selectedCharacterGuids, selectedCharacterId, bountyQuestData.Rarity, requiredElementTypes);
                    else
                        target = GetCharacterByRarity(userCharacterDtoInfos, selectedCharacterGuids, selectedCharacterId, CharacterRarityFlags.None, requiredElementTypes);

                    if (target == null) break;
                    // 从待匹配元素中移除该元素
                    requiredElementTypes.Remove(CharacterTable.GetById(target.CharacterId).ElementType);
                    // 从候选卡片里面移除该角色
                    if (requiredRarityCount > 0) requiredRarityCount--;
                    selectedCharacterId.Add(target.CharacterId);
                    questMemberInfos.Add(new BountyQuestMemberInfo
                    {
                        CharacterId = target.CharacterId, CharacterRarityFlags = target.RarityFlags, UserCharacterGuid = target.Guid, PlayerId = target.PlayerId
                    });
                } while (requiredElementTypes.Count > 0);

                while (requiredRarityCount > 0)
                {
                    var target = GetCharacterByRarity(userCharacterDtoInfos, selectedCharacterGuids, selectedCharacterId, bountyQuestData.Rarity);
                    if (target != null)
                    {
                        selectedCharacterId.Add(target.CharacterId);
                        questMemberInfos.Add(new BountyQuestMemberInfo
                        {
                            CharacterId = target.CharacterId, CharacterRarityFlags = target.RarityFlags, UserCharacterGuid = target.Guid, PlayerId = target.PlayerId
                        });
                        requiredRarityCount--;
                    }
                    else
                        break;
                }

                if (requiredElementTypes.Count > 0 || requiredRarityCount > 0) continue;

                bountyQuestStartInfos.Add(bountyQuestStartInfo);
                // 排除 GUID
                selectedCharacterGuids.AddRange(questMemberInfos.Select(d => d.UserCharacterGuid));
            }
        }

        return bountyQuestStartInfos;
    }

    public static UserCharacterDtoInfo GetCharacterByRarity(
        List<UserCharacterDtoInfo> userCharacterDtoInfos,
        List<string> guids,
        List<long> characterIds,
        CharacterRarityFlags rarityFlags = CharacterRarityFlags.None,
        List<ElementType> elementTypes = null)
    {
        foreach (var dtoInfo in userCharacterDtoInfos)
        {
            // 每种类型的赏金任务组, 不能包含同一张卡
            if (guids.Contains(dtoInfo.Guid)) continue;
            // 每个赏金任务, 不能包含同一个角色
            if (characterIds.Contains(dtoInfo.CharacterId)) continue;
            // 稀有度要求
            if (rarityFlags > dtoInfo.RarityFlags) continue;
            // 元素要求
            if (elementTypes != null && !elementTypes.Contains(CharacterTable.GetById(dtoInfo.CharacterId).ElementType)) continue;

            return dtoInfo;
        }

        return null;
    }

    public static List<UserBountyQuestMemberDtoInfo> GetReadySupportMemberDtoInfos(GetListResponse getListResponse)
    {
        var ongoingGuids = getListResponse.UserBountyQuestDtoInfos.SelectMany(d => d.StartMembers).Select(d => d.UserCharacterGuid).ToHashSet();
        var memberDtoInfos = getListResponse.FriendAndGuildMemberUserBountyQuestMemberDtoInfos.Where(d => !ongoingGuids.Contains(d.UserCharacterGuid));
        return memberDtoInfos.ToList();
    }

    public static UserBountyQuestMemberDtoInfo? GetSupportMember(List<UserBountyQuestMemberDtoInfo> supportMemberDtoInfos, BountyQuestData bountyQuestData)
    {
        foreach (var info in supportMemberDtoInfos)
        {
            if (bountyQuestData.RarityRequireCount > 0 && info.RarityFlags < bountyQuestData.Rarity) continue;

            var characterMb = CharacterTable.GetById(info.CharacterId);
            if (bountyQuestData.ElementTypes.Contains(characterMb.ElementType)) return info;
        }

        // throw new Exception(ResourceStrings.No_available_support_characters_found_);
        return null;
    }

    private static List<UserCharacterDtoInfo> SortGuerrilla(List<UserCharacterDtoInfo> characterDtoInfos)
    {
        var maxRarityCharacterCountDictionary = CreateMaxRarityCharacterCountDictionary(characterDtoInfos);
        var characterMbCache = new Dictionary<long, CharacterMB>();

        foreach (var characterDtoInfo in characterDtoInfos)
        {
            if (!characterMbCache.ContainsKey(characterDtoInfo.CharacterId))
                characterMbCache.Add(characterDtoInfo.CharacterId, CharacterTable.GetById(characterDtoInfo.CharacterId));
        }

        return characterDtoInfos
            .OrderByDescending(d => d.RarityFlags < CharacterRarityFlags.LR ? d.RarityFlags : CharacterRarityFlags.LR) // 按稀有度排序
            .ThenByDescending(d => maxRarityCharacterCountDictionary[characterMbCache[d.CharacterId].ElementType]) // 然后按元素的最高稀有度的角色数量排序
            .ThenBy(d => characterMbCache[d.CharacterId].ElementType)
            .ThenBy(d => d.CharacterId)
            .ToList();
    }


    private static Dictionary<ElementType, int> CreateMaxRarityCharacterCountDictionary(List<UserCharacterDtoInfo> characterDtoInfos)
    {
        var characterRarityFlagsMap = new Dictionary<ElementType, CharacterRarityFlags>();
        var characterCountDictionary = new Dictionary<ElementType, int>();
        foreach (var characterDtoInfo in characterDtoInfos)
        {
            var characterMb = CharacterTable.GetById(characterDtoInfo.CharacterId);
            var characterRarityFlags = characterDtoInfo.RarityFlags < CharacterRarityFlags.LR ? characterDtoInfo.RarityFlags : CharacterRarityFlags.LR;
            if (characterRarityFlagsMap.TryGetValue(characterMb.ElementType, out var rarityFlags))
            {
                if (rarityFlags < characterRarityFlags)
                {
                    characterRarityFlagsMap[characterMb.ElementType] = characterRarityFlags;
                    characterCountDictionary[characterMb.ElementType] = 1;
                }
                else
                    characterCountDictionary[characterMb.ElementType]++;
            }
            else
            {
                characterRarityFlagsMap[characterMb.ElementType] = characterRarityFlags;
                characterCountDictionary[characterMb.ElementType] = 1;
            }
        }

        return characterCountDictionary;
    }
}