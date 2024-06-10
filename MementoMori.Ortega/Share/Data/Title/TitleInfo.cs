using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Title
{
    [MessagePackObject(true)]
    public class TitleInfo
    {
        public int BgmNumberJP { get; set; }

        public int BgmNumberUS { get; set; }

        public int MovieNumber { get; set; }

        public int LogoNumber { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Scale { get; set; }

        public float AnchorMinX { get; set; }

        public float AnchorMinY { get; set; }

        public float AnchorMaxX { get; set; }

        public float AnchorMaxY { get; set; }

        public long CharacterId { get; set; }

        public bool IsCharacterLiveMode { get; set; }

        public float LamentStartTimeJP { get; set; }

        public float LamentStartTimeUS { get; set; }

        public int GetBgmNumber(LanguageType languageType)
        {
            if (languageType == LanguageType.jaJP)
            {
                return this.BgmNumberJP;
            }

            return this.BgmNumberUS;
        }
        
        public float GetLamentStartTime(LanguageType languageType)
        {
            if (languageType == LanguageType.jaJP)
            {
                return this.LamentStartTimeJP;
            }
            return this.LamentStartTimeUS;
        }
    }
}