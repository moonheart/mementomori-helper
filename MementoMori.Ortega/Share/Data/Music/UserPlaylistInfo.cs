using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Music
{
	[MessagePackObject(true)]
	public class UserPlaylistInfo
	{
		public string Guid { get; set; }

		public string PlaylistName { get; set; }

		public List<UserPlaylistMusicInfo> MusicList { get; set; }

		public void Copy(UserPlaylistInfo userPlaylistInfo)
		{
			// string text = userPlaylistInfo.<Guid>k__BackingField;
			// this.<Guid>k__BackingField = text;
			// string text2 = userPlaylistInfo.<PlaylistName>k__BackingField;
			// this.<PlaylistName>k__BackingField = text2;
			// List<UserPlaylistMusicInfo> list = new List(userPlaylistInfo.<MusicList>k__BackingField);
			// this.<MusicList>k__BackingField = list;
			// throw new NullReferenceException();
		}

		public bool IsIncludeMusicGuid(string musicGuid)
		{
			// for (;;)
			// {
			// 	List<UserPlaylistMusicInfo> list = this.<MusicList>k__BackingField;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		break;
			// 	}
			// 	ulong num;
			// 	if (num == (ulong)0L)
			// 	{
			// 		goto Block_1;
			// 	}
			// }
			// bool flag2;
			// while (!flag2)
			// {
			// }
			// return true;
			// Block_1:
			throw new NullReferenceException();
		}
	}
}
