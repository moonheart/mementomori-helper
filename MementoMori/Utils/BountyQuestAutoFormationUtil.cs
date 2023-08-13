using DynamicData;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Utils;

public static class BountyQuestAutoFormationUtil
{
    public static List<BountyQuestStartInfo> CalcAutoFormation(GetListResponse getlistResponse, UserSyncData userSyncData)
    {
        var bountyQuestStartInfos = new List<BountyQuestStartInfo>();
        foreach (var bountyQuestInfos in getlistResponse.BountyQuestInfos.GroupBy(d=>d.BountyQuestType))
        {
            var userCharacterDtoInfos = userSyncData.UserCharacterDtoInfos.OrderBy(d=>d.RarityFlags).ToList();
            var selectedCharacterGuids = new List<string>();
            foreach (var getlistResponseBountyQuestInfo in bountyQuestInfos)
            {
                var selectedCharacterId = new List<long>();
                var bountyQuestData = new BountyQuestData(getlistResponseBountyQuestInfo);
                var rarityRequireCount = bountyQuestData.RarityRequireCount;
                var elementTypes = bountyQuestData.ElementTypes.ToList();
                var questMemberInfos = new List<BountyQuestMemberInfo>();
                var bountyQuestStartInfo = new BountyQuestStartInfo(){BountyQuestId = getlistResponseBountyQuestInfo.BountyQuestId, BountyQuestMemberInfos = questMemberInfos};
                // 检查是否联合任务
                if (bountyQuestData.QuestInfo.BountyQuestType == BountyQuestType.Team)
                {
                    var userBountyQuestMemberDtoInfos = GetReadySupportMemberDtoInfos(getlistResponse.FriendAndGuildMemberUserBountyQuestMemberDtoInfos);
                    // 这里假定一定能获取到符合条件的支援角色
                    var supportMember = GetSupportMember(userBountyQuestMemberDtoInfos, bountyQuestData);
                    var characterMb = Masters.CharacterTable.GetById(supportMember.CharacterId);
                    if (rarityRequireCount > 0) rarityRequireCount--;
                    elementTypes.Remove(characterMb.ElementType);
                    selectedCharacterId.Add(supportMember.CharacterId);
                    questMemberInfos.Add(new BountyQuestMemberInfo()
                    {
                        CharacterId = supportMember.CharacterId, CharacterRarityFlags = supportMember.RarityFlags, UserCharacterGuid = supportMember.UserCharacterGuid, PlayerId = supportMember.PlayerId
                    });
                }

                // 所有符合稀有度条件的角色
                var charactersByRarity = GetCharactersByRarity(userCharacterDtoInfos, selectedCharacterGuids, selectedCharacterId, bountyQuestData.Rarity);
                // 匹配元素类型
                do
                {
                    var target = charactersByRarity.FirstOrDefault(d => elementTypes.Contains(Masters.CharacterTable.GetById(d.CharacterId).ElementType));
                    if (target == null)
                    {
                        break;
                    }
                    // 从待匹配元素中移除该元素
                    elementTypes.Remove(Masters.CharacterTable.GetById(target.CharacterId).ElementType);
                    // 从候选卡片里面移除该角色
                    charactersByRarity.RemoveAll(d => d.CharacterId == target.CharacterId);
                    questMemberInfos.Add(new BountyQuestMemberInfo()
                    {
                        CharacterId = target.CharacterId, CharacterRarityFlags = target.RarityFlags, UserCharacterGuid = target.Guid, PlayerId = target.PlayerId
                    });
                   
                } while (elementTypes.Count > 0);

                if (elementTypes.Count > 0)
                {
                    continue;
                }
                
                bountyQuestStartInfos.Add(bountyQuestStartInfo);
                // 排除 GUID
                selectedCharacterGuids.AddRange(questMemberInfos.Select(d=>d.UserCharacterGuid));
            }
        }

        return bountyQuestStartInfos;
    }

    public static List<UserCharacterDtoInfo> GetCharactersByRarity(List<UserCharacterDtoInfo> userCharacterDtoInfos, List<string> guids, List<long> characterIds, CharacterRarityFlags rarityFlags)
    {
        var characterDtoInfos = new List<UserCharacterDtoInfo>();
        foreach (var dtoInfo in userCharacterDtoInfos)
        {
            // 每种类型的赏金任务组, 不能包含同一张卡
            if (guids.Contains(dtoInfo.Guid)) continue;
            // 每个赏金任务, 不能包含同一个角色
            if (characterIds.Contains(dtoInfo.CharacterId)) continue;
            // 稀有度要求
            if (rarityFlags > dtoInfo.RarityFlags) continue;
            
            characterDtoInfos.Add(dtoInfo);
        }

        return characterDtoInfos;
    }

    public static List<UserBountyQuestMemberDtoInfo> GetReadySupportMemberDtoInfos(List<UserBountyQuestMemberDtoInfo> memberDtoInfos)
    {
        return memberDtoInfos.ToList();
    }

    public static UserBountyQuestMemberDtoInfo GetSupportMember(List<UserBountyQuestMemberDtoInfo> supportMemberDtoInfos, BountyQuestData bountyQuestData)
    {
        foreach (var info in supportMemberDtoInfos)
        {
            if (bountyQuestData.RarityRequireCount > 0 && info.RarityFlags < bountyQuestData.Rarity)
            {
                continue;
            }

            var characterMb = Masters.CharacterTable.GetById(info.CharacterId);
            if (bountyQuestData.ElementTypes.Contains(characterMb.ElementType))
            {
                return info;
            }
        }

        throw new Exception("找不到可用的资源角色");
    }
    
    
}