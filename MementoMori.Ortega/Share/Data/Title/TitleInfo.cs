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

        public float X { get; set; }

        public float Y { get; set; }

        public float Scale { get; set; }

        public int GetBgmNumber(LanguageType languageType)
        {
            if (languageType == LanguageType.jaJP)
            {
                return this.BgmNumberJP;
            }

            return this.BgmNumberUS;
        }

        public TitleInfo()
        {
        }
    }
}