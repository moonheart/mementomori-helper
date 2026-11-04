using MementoMori.Ortega.Share.Data.MiningQuest;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("採掘ミニゲーム基礎パラメータ")]
	public class MiningQuestBaseParametersMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("基礎制限時間（秒）")]
		public int BaseLimitTimeSeconds
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("基礎ブロック生成上限数")]
		public int BaseBlockGenerateLimit
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("基礎攻撃力")]
		public int BaseAttackPower
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("基礎攻撃範囲")]
		public int BaseAttackRange
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("基礎攻撃頻度")]
		public int BaseAttackFrequency
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("基礎攻撃移動速度")]
		public int BaseAttackSpeed
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("ブロック接触時移動速度倍率")]
		public int ContactBlockAttackSpeedRatePercent
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("ブロック再生成抽頻度")]
		public int RespawnBlockIntervalMilliseconds
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(9)]
		[Description("ブロック再生成抽選枠リスト")]
		public IReadOnlyList<MiningQuestBlockRespawn> BlockRespawnList
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(10)]
		[Description("特殊攻撃種類リスト")]
		public IReadOnlyList<MiningQuestSpecialAttack> SpecialAttackList
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(11)]
		[Description("出現ブロックリスト")]
		public IReadOnlyList<MiningQuestBlock> BlockList
		{
			get;
		}

		[PropertyOrder(12)]
		[Description("バーチャルスティックサイズリスト")]
		public IReadOnlyList<int> VirtualStickSizeList
		{
			get;
		}

		[PropertyOrder(13)]
		[Description("レイヤーグループ別ブロックサイズ倍率")]
		public IReadOnlyList<int> LayerBlockSizePercentList
		{
			get;
		}

		[PropertyOrder(14)]
		[Description("レイヤーグループ別ブロック定員割合weight")]
		public IReadOnlyList<int> LayerBlockCapacityWeightList
		{
			get;
		}

		[PropertyOrder(15)]
		[Description("基礎通貨アイテム固定獲得量")]
		public int BaseFixedRewardCount
		{
			get;
		}

		[SerializationConstructor]
		public MiningQuestBaseParametersMB(long id, bool? isIgnore, string memo, int baseLimitTimeSeconds, int baseBlockGenerateLimit, int baseAttackPower, int baseAttackRange, int baseAttackFrequency, int baseAttackSpeed, int contactBlockAttackSpeedRatePercent, int respawnBlockIntervalMilliseconds, IReadOnlyList<MiningQuestBlockRespawn> blockRespawnList, IReadOnlyList<MiningQuestSpecialAttack> specialAttackList, IReadOnlyList<MiningQuestBlock> blockList, IReadOnlyList<int> virtualStickSizeList, IReadOnlyList<int> layerBlockSizePercentList, IReadOnlyList<int> layerBlockCapacityWeightList, int baseFixedRewardCount)
			:base(id, isIgnore, memo)
		{
			BaseLimitTimeSeconds = baseLimitTimeSeconds;
			BaseBlockGenerateLimit = baseBlockGenerateLimit;
			BaseAttackPower = baseAttackPower;
			BaseAttackRange = baseAttackRange;
			BaseAttackFrequency = baseAttackFrequency;
			BaseAttackSpeed = baseAttackSpeed;
			ContactBlockAttackSpeedRatePercent = contactBlockAttackSpeedRatePercent;
			RespawnBlockIntervalMilliseconds = respawnBlockIntervalMilliseconds;
			BlockRespawnList = blockRespawnList;
			SpecialAttackList = specialAttackList;
			BlockList = blockList;
			VirtualStickSizeList = virtualStickSizeList;
			LayerBlockSizePercentList = layerBlockSizePercentList;
			LayerBlockCapacityWeightList = layerBlockCapacityWeightList;
			BaseFixedRewardCount = baseFixedRewardCount;
		}

		public MiningQuestBaseParametersMB():base(0, false, "")
		{
		}
	}
}
