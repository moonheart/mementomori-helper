using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class CharacterPotentialCoefficientTable : TableBase<CharacterPotentialCoefficientMB>
	{
		public CharacterPotentialCoefficientMB GetByInitialRarityAndNowRarity(CharacterRarityFlags initialRarityFlags, CharacterRarityFlags nowRarityFlags)
		{
			foreach (var mb in _datas)
			{
				if (mb.InitialRarityFlags == initialRarityFlags && mb.RarityFlags == nowRarityFlags)
				{
					return mb;
				}
			}

			string text = string.Format("CharacterPotentialCoefficientMB is null. initialRarity:{0} nowRarity:{1}", initialRarityFlags, nowRarityFlags);
			throw new Exception(text);
			// int num = 0;
			// if (num < (int)nowRarityFlags)
			// {
			// 	num++;
			// }
			// CharacterPotentialCoefficientMB characterPotentialCoefficientMB;
			// return characterPotentialCoefficientMB;
			throw new NotImplementedException();
		}

		public CharacterPotentialCoefficientTable()
		{
		}
	}
}
