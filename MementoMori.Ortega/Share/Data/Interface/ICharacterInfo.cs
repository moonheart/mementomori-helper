using System;
using MementoMori.Ortega.Share.Enums;

namespace Ortega.Share.Data.Interface
{
	public interface ICharacterInfo
	{
		long CharacterId { get; set; }

		string Guid { get; set; }

		CharacterRarityFlags RarityFlags { get; set; }

		long Level { get; set; }

		bool IsGuest();

		bool IsShare();
	}
}
