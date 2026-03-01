using System.Text.Json.Serialization;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	[MessagePackObject(true)]
	public class UserCharacter : IUserItem
	{
		public long CharacterId
		{
			get
			{
				return this.ItemId;
			}
		}

		public CharacterRarityFlags CharacterRarityFlags { get; }

		public string Guid { get; }

		public UserCharacter(long itemId, CharacterRarityFlags characterRarityFlags = CharacterRarityFlags.None)
		{
			this.ItemId = itemId;
			this.ItemCount = (long)((ulong)1L);
			string text = StringUtil.CreateGuid();
			this.Guid = text;
			this.CharacterRarityFlags = characterRarityFlags;
		}

		public UserCharacter(long itemId, string guid, CharacterRarityFlags characterRarityFlags)
        {
			this.ItemId = itemId;
			this.ItemCount = (long)((ulong)1L);
			this.Guid = guid;
			this.CharacterRarityFlags = characterRarityFlags;
		}

		public UserCharacter(long itemId, long itemCount, CharacterRarityFlags characterRarityFlags = CharacterRarityFlags.None)
		{
			this.ItemId = itemId;
			this.ItemCount = itemCount;
			string text = StringUtil.CreateGuid();
			this.Guid = text;
			this.CharacterRarityFlags = characterRarityFlags;
		}

		[JsonConstructor]
		public UserCharacter(long itemId, long itemCount, string guid, CharacterRarityFlags characterRarityFlags = CharacterRarityFlags.None)
		{
			this.ItemId = itemId;
			this.ItemCount = itemCount;
			this.Guid = guid;
			this.CharacterRarityFlags = CharacterRarityFlags.None;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)6L);
	}
}
