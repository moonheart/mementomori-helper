using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class BattleField
    {
        public BattleType BattleType { get; set; }

        public List<BattleFieldCharacter> Characters { get; set; }

        public List<long> AttackTeamPassiveSkillIds { get; set; }

        public List<long> ReceiveTeamPassiveSkillIds { get; set; }

        public int AttackTeamTotalKillCount { get; set; }

        public int ReceiveTeamTotalKillCount { get; set; }

        public List<DamageBar> DamageBarList { get; set; }

        // [Obsolete("TODO 2019-12-05 takeda 使わないなら削除、使うならLinq排除")]
        // public List<long> JoinPlayerIds
        // {
        // 	get
        // 	{
        // 		List<BattleFieldCharacter> list = this.Characters;
        // 		Func<BattleFieldCharacter, long> <>9__25_ = BattleField.<>c.<>9__25_0;
        // 		if (<>9__25_ == 0)
        // 		{
        // 			Func<BattleFieldCharacter, long> func;
        // 			BattleField.<>c.<>9__25_0 = func;
        // 		}
        // 		return Enumerable.ToList<long>(Enumerable.Distinct<long>(Enumerable.Select<BattleFieldCharacter, long>(list, <>9__25_)));
        // 	}
        // }
        //
        // public List<long> GetAllUniqueCharacterIdView(long playerId)
        // {
        // 	List<long> list;
        // 	ulong num;
        // 	do
        // 	{
        // 		list = new List();
        // 		List<BattleFieldCharacter> list2 = this.Characters;
        // 		bool flag;
        // 		if (flag)
        // 		{
        // 			bool flag2;
        // 			while (flag2)
        // 			{
        // 			}
        // 		}
        // 	}
        // 	while (num != (ulong)0L);
        // 	return list;
        // }
        //
        public BattleFieldCharacter GetCharacterByGuid(int guid)
        {
            for (var i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].Guid == guid)
                {
                    return Characters[i];
                }
            }

            return null;
        }
        
        public List<BattleFieldCharacter> GetOffenseCharacters()
        {
        	return Characters.FindAll(x => x.DefaultPosition.IsAttacker);
        }
        
        public List<BattleFieldCharacter> GetDefenseCharacters()
        {
        	return Characters.FindAll(x => !x.DefaultPosition.IsAttacker);
        }
        
        // public List<long> GetOffenseCharacterIds()
        // {
        // 	return Characters.FindAll(x => x.DefaultPosition.IsAttacker).ConvertAll(x => x.);
        // }
        //
        // public List<long> GetDefenseCharacterIds()
        // {
        // 	List<long> list;
        // 	ulong num;
        // 	do
        // 	{
        // 		list = new List();
        // 		List<BattleFieldCharacter> list2 = this.Characters;
        // 		bool flag;
        // 		if (flag)
        // 		{
        // 		}
        // 	}
        // 	while (num != (ulong)0L);
        // 	return list;
        // }
        //
        // public bool IsSameGroup(int fromGuid, int toGuid)
        // {
        // 	int groupType = (int)this.GetGroupType(fromGuid);
        // 	BattleFieldCharacterGroupType groupType2 = this.GetGroupType(fromGuid);
        // 	return groupType == (int)groupType2;
        // }
        //
        // public BattleFieldCharacterGroupType GetGroupType(int guid)
        // {
        // 	ulong num2;
        // 	do
        // 	{
        // 		List<BattleFieldCharacter> list = this.Characters;
        // 		bool flag;
        // 		if (flag)
        // 		{
        // 			int num = 0;
        // 			if ((("{il2cpp field on {'GetEnumerator' (constant value of type Cpp2IL.Core.Analysis.ResultModels.GenericMethodReference)}, offset 0xX}" == num) ? 1 : 0) != guid)
        // 			{
        // 				continue;
        // 			}
        // 		}
        // 	}
        // 	while (num2 != (ulong)0L);
        // 	throw new NullReferenceException();
        // }

        public BattleField()
        {
            this.Characters = new List<BattleFieldCharacter>();
            this.AttackTeamPassiveSkillIds = new List<long>();
            this.ReceiveTeamPassiveSkillIds = new List<long>();
        }
    }
}