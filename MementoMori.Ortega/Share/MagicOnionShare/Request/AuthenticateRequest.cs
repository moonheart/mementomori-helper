using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class AuthenticateRequest
	{
		[Key(1)]
		public string AuthToken { get; set; }

		[Key(0)]
		public long PlayerId { get; set; }

		[Key(2)]
		public DeviceType DeviceType { get; set; }
	}
}
