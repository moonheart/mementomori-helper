using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ChatMusicPlaylistInfo
	{
		[Key(0)]
		public long FirstMusicId { get; set; }

		[Key(1)]
		public long PlayerIconId { get; set; }

		[Key(2)]
		public string PlaylistName { get; set; }

		[Key(3)]
		public int MusicCount { get; set; }

		[Key(4)]
		public string PlaylistShareCode { get; set; }
	}
}
