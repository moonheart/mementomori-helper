using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserDeckDtoInfo
    {
        public int DeckNo { get; set; }

        public DeckUseContentType DeckUseContentType { get; set; }

        public long DeckBattlePower { get; set; }

        public string UserCharacterGuid1 { get; set; }

        public long CharacterId1 { get; set; }

        public string UserCharacterGuid2 { get; set; }

        public long CharacterId2 { get; set; }

        public string UserCharacterGuid3 { get; set; }

        public long CharacterId3 { get; set; }

        public string UserCharacterGuid4 { get; set; }

        public long CharacterId4 { get; set; }

        public string UserCharacterGuid5 { get; set; }

        public long CharacterId5 { get; set; }

        public List<string> GetUserCharacterGuids()
        {
            return new List<string>()
            {
                UserCharacterGuid1, UserCharacterGuid2, UserCharacterGuid3, UserCharacterGuid4, UserCharacterGuid5
            }.Where(d => !string.IsNullOrEmpty(d)).ToList();
        }

        public List<long> GetCharacterIds()
        {
            return new List<long>()
            {
                CharacterId1, CharacterId2, CharacterId3, CharacterId4, CharacterId5
            }.Where(d => d > 0).ToList();
        }

        public List<long> GetOpponentCharacterIds()
        {
            return new List<long>()
            {
                CharacterId1, CharacterId2, CharacterId3, CharacterId4, CharacterId5
            }.Where(d => d > 0).ToList();
        }

        public UserDeckDtoInfo()
        {
        }
    }
}