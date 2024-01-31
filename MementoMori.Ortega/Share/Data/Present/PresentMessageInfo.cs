using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Present
{
    [MessagePackObject(true)]
    public class PresentMessageInfo
    {
        public long Damage { get; set; }

        public int GlobalGvgGroupType { get; set; }

        public int Group { get; set; }

        public long Ranking { get; set; }

        public string CharacterCollectionNameKey { get; set; }

        public long CharacterCollectionLevel { get; set; }

        public string GuildRaidEventNameKey { get; set; }

        public string RelatedPlayerName { get; set; }
    }
}