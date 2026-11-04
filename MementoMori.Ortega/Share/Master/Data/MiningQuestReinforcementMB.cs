using MementoMori.Ortega.Share.Data.MiningQuest;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("採掘ミニゲーム能力強化")]
	public class MiningQuestReinforcementMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("強化名称")]
		public string NameTextKey
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("強化内容説明テキスト")]
		public string DescriptionTextKey
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("表示グループ")]
		public MiningQuestReinforcementDisplayType DisplayType
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("強化パラメータ表示タイプ")]
		public MiningQuestReinforcementParameterDisplayType ParameterDisplayType
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("アイコン画像ID")]
		public long IconImageId
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(6)]
		[Description("強化内容リスト")]
		public IReadOnlyList<MiningQuestReinforcementInfo> ReinforcementInfoList
		{
			get;
		}

		[SerializationConstructor]
		public MiningQuestReinforcementMB(long id, bool? isIgnore, string memo, string nameTextKey, string descriptionTextKey, MiningQuestReinforcementDisplayType displayType, MiningQuestReinforcementParameterDisplayType parameterDisplayType, long iconImageId, IReadOnlyList<MiningQuestReinforcementInfo> reinforcementInfoList)
			:base(id, isIgnore, memo)
		{
			NameTextKey = nameTextKey;
			DescriptionTextKey = descriptionTextKey;
			DisplayType = displayType;
			ParameterDisplayType = parameterDisplayType;
			IconImageId = iconImageId;
			ReinforcementInfoList = reinforcementInfoList;
		}

		public MiningQuestReinforcementMB():base(0, false, "")
		{
		}
	}
}
