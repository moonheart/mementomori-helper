using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class CloseDialogRequest
	{
		[Key(0)]
		public GvgDialogType DialogType { get; set; }
	}
}
