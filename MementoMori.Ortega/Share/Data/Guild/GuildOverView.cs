using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class GuildOverView
	{
        public GuildCommunicationPolicyType CommunicationPolicyType { get; set; }

        public GuildEventPolicyType EventPolicyType { get; set; }

        public GuildBattlePolicyType GuildBattlePolicyType { get; set; }

		public string GuildDescription{ get; set; }

		public string GuildName{ get; set; }

		public bool IsFreeJoin { get; set; }

		public long RequireBattlePower { get; set; }
	}
}
