using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Share.Calculators
{
	public static class CharacterCollectionCalculator
	{
		public static List<IUserItem> CalcCharacterCollectionReward(CharacterCollectionMB characterCollectionMB, CharacterCollectionLevelMB characterCollectionLevelMB)
		{
			throw new NotImplementedException();
			// IReadOnlyList<long> <RequiredCharacterIds>k__BackingField = characterCollectionMB.<RequiredCharacterIds>k__BackingField;
			// List<IUserItem> list = new List();
			// CharacterCollectionRewardTable <CharacterCollectionRewardTable>k__BackingField = Masters.<CharacterCollectionRewardTable>k__BackingField;
			// CharacterCollectionRewardMB characterCollectionRewardMB;
			// if (characterCollectionRewardMB != 0 && !characterCollectionRewardMB.<RewardItems>k__BackingField.IsNullOrEmpty<UserItem>())
			// {
			// 	IReadOnlyList<UserItem> <RewardItems>k__BackingField = characterCollectionRewardMB.<RewardItems>k__BackingField;
			// 	list.AddRange(<RewardItems>k__BackingField);
			// }
			// return list;
		}

		public static List<IUserItem> CalcCharacterCollectionReward(int characterCount, int collectionLevel)
		{
			throw new NotImplementedException();
			// List<IUserItem> list = new List();
			// CharacterCollectionRewardMB byCharacterCountAndCollectionLevel = Masters.<CharacterCollectionRewardTable>k__BackingField.GetByCharacterCountAndCollectionLevel(characterCount, collectionLevel);
			// if (byCharacterCountAndCollectionLevel != 0 && !byCharacterCountAndCollectionLevel.<RewardItems>k__BackingField.IsNullOrEmpty<UserItem>())
			// {
			// 	IReadOnlyList<UserItem> <RewardItems>k__BackingField = byCharacterCountAndCollectionLevel.<RewardItems>k__BackingField;
			// 	list.AddRange(<RewardItems>k__BackingField);
			// }
			// return list;
		}
	}
}
