using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DungeonBattle
{
	[MessagePackObject(true)]
	public class DungeonBattleLayer
	{
		public DungeonBattleDifficultyType DungeonDifficultyType
		{
			get;
			set;
		}

		public List<DungeonBattleGrid> DungeonGrids
		{
			get;
			set;
		}

		public int LayerCount
		{
			get;
			set;
		}

		public DungeonBattleGrid GetGridByDungeonGridGuid(string guid)
		{
			return DungeonGrids.FirstOrDefault(d => d.DungeonGridGuid == guid);
		}

		public int GetColumnCount(int y)
		{
			return DungeonGrids.Count(d => d.Y == y);
		}

		public DungeonBattleLayer()
		{
		}
	}
}
