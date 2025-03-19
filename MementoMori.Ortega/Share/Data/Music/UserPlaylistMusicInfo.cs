using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Music
{
	[MessagePackObject(true)]
	public class UserPlaylistMusicInfo
	{
		public string Guid { get; set; }

		public long MusicId { get; set; }
	}
}
