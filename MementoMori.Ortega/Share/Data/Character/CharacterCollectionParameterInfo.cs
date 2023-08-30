using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
	[MessagePackObject(true)]
	public class CharacterCollectionParameterInfo
	{
		[Description("キャラー全てのベースパラメータ変化")]
		public List<BaseParameterChangeInfo> AllCharacterBaseParameterChangeInfos { get; set; }
		[Description("キャラー全てのバトルパラメータ変化")]
		public List<BattleParameterChangeInfo> AllCharacterBattleParameterChangeInfos { get; set; }

		[Description("キャラーごとのベースパラメータ変化")]
		public Dictionary<long, List<BaseParameterChangeInfo>> EachCharacterBaseParameterChangeInfoDict { get; set; }

		[Description("キャラーごとのバトルパラメータ変化")]
		public Dictionary<long, List<BattleParameterChangeInfo>> EachCharacterBattleParameterChangeInfoDict { get; set; }

		public void Init()
		{
			AllCharacterBaseParameterChangeInfos = new List<BaseParameterChangeInfo>();
			AllCharacterBattleParameterChangeInfos = new List<BattleParameterChangeInfo>();
			EachCharacterBaseParameterChangeInfoDict = new Dictionary<long, List<BaseParameterChangeInfo>>();
			EachCharacterBattleParameterChangeInfoDict = new Dictionary<long, List<BattleParameterChangeInfo>>();
		}

		public void Clear()
		{
			AllCharacterBaseParameterChangeInfos.Clear();
			AllCharacterBattleParameterChangeInfos.Clear();
			EachCharacterBaseParameterChangeInfoDict.Clear();
			EachCharacterBattleParameterChangeInfoDict.Clear();
		}

		public void Setup(List<UserCharacterCollectionDtoInfo> userCharacterCollectionDtoInfos)
		{
			CharacterCollectionTable CharacterCollectionTable = Masters.CharacterCollectionTable;
			CharacterCollectionLevelTable CharacterCollectionLevelTable = Masters.CharacterCollectionLevelTable;
			
			foreach (var collectionDtoInfo in userCharacterCollectionDtoInfos)
			{
				var characterCollectionMb = CharacterCollectionTable.GetById(collectionDtoInfo.CharacterCollectionId);
				var characterCollectionLevelMb = CharacterCollectionLevelTable.GetByCollectionIdAndLevel(collectionDtoInfo.CharacterCollectionId, collectionDtoInfo.CollectionLevel);

				if (characterCollectionMb != null && characterCollectionLevelMb != null)
				{
					if (collectionDtoInfo.CollectionLevel >= 3)
					{
						AllCharacterBaseParameterChangeInfos.AddRange(characterCollectionLevelMb.BaseParameterChangeInfos ?? Array.Empty<BaseParameterChangeInfo>());
						AllCharacterBattleParameterChangeInfos.AddRange(characterCollectionLevelMb.BattleParameterChangeInfos ?? Array.Empty<BattleParameterChangeInfo>());
					}
					else
					{
						foreach (var requiredCharacterId in characterCollectionMb.RequiredCharacterIds)
						{
							if(EachCharacterBaseParameterChangeInfoDict.TryGetValue(requiredCharacterId, out var baseParameterChangeInfos))
							{
								baseParameterChangeInfos.AddRange(characterCollectionLevelMb.BaseParameterChangeInfos ?? Array.Empty<BaseParameterChangeInfo>());
							}
							else if (!characterCollectionLevelMb.BaseParameterChangeInfos.IsNullOrEmpty())
							{
								EachCharacterBaseParameterChangeInfoDict.Add(requiredCharacterId, characterCollectionLevelMb.BaseParameterChangeInfos.ToList());
							}

							if (EachCharacterBattleParameterChangeInfoDict.TryGetValue(requiredCharacterId, out var battleParameterChangeInfos))
							{
								battleParameterChangeInfos.AddRange(characterCollectionLevelMb.BattleParameterChangeInfos ?? Array.Empty<BattleParameterChangeInfo>());
							}
							else if (!characterCollectionLevelMb.BattleParameterChangeInfos.IsNullOrEmpty())
							{
								EachCharacterBattleParameterChangeInfoDict.Add(requiredCharacterId, characterCollectionLevelMb.BattleParameterChangeInfos.ToList());
							}
						}
					}
				}
			}
		}
	}
}
