using MementoMori.Ortega.Common;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.WebUI.UI
{
    public class AtlasManager
    {
        public string GetCharacterIcon(long characterId)
        {
            return $"assets/CharacterIcon/CHR_{characterId:000000}/CHR_{characterId:000000}_00_s.png";
        }
        
        public Sprite GetCharacterFrame(CharacterRarityFlags characterRarityFlags)
        {
            if (characterRarityFlags > CharacterRarityFlags.LR)
            {
                characterRarityFlags = CharacterRarityFlags.LR;
            }
            var sprite = new Sprite();
            GetFrameCharacter(characterRarityFlags, sprite);
            sprite.Color = ClientConst.Icon.CharacterFrameColorDictionary[characterRarityFlags];
            sprite.Filter = characterRarityFlags switch
            {
                CharacterRarityFlags.None => "none",
                CharacterRarityFlags.N => "invert(0%) sepia(70%) saturate(481%) hue-rotate(302deg) brightness(95%) contrast(93%)",
                CharacterRarityFlags.R => "invert(0%) sepia(2%) saturate(318%) hue-rotate(195deg) brightness(114%) contrast(73%)",
                CharacterRarityFlags.RPlus => "invert(0%) sepia(2%) saturate(318%) hue-rotate(195deg) brightness(114%) contrast(73%)",
                CharacterRarityFlags.SR => "invert(0%) sepia(70%) saturate(262%) hue-rotate(343deg) brightness(85%) contrast(101%)",
                CharacterRarityFlags.SRPlus => "invert(0%) sepia(70%) saturate(262%) hue-rotate(343deg) brightness(85%) contrast(101%)",
                CharacterRarityFlags.SSR => "invert(0%) sepia(70%) saturate(450%) hue-rotate(268deg) brightness(62%) contrast(107%)",
                CharacterRarityFlags.SSRPlus => "invert(0%) sepia(70%) saturate(450%) hue-rotate(268deg) brightness(62%) contrast(107%)",
                CharacterRarityFlags.UR => "invert(0%) sepia(75%) saturate(982%) hue-rotate(299deg) brightness(84%) contrast(124%)",
                CharacterRarityFlags.URPlus => "invert(0%) sepia(75%) saturate(982%) hue-rotate(299deg) brightness(84%) contrast(124%)",
                CharacterRarityFlags.LR => "none",
                _ => throw new ArgumentOutOfRangeException(nameof(characterRarityFlags), characterRarityFlags, null)
            };

            return sprite;
        }

        private Sprite GetFrameCharacter(CharacterRarityFlags rarityFlags, Sprite sprite)
        {
            if (rarityFlags == CharacterRarityFlags.None)
            {
                sprite.Width = 146;
                sprite.Height = 150;
                sprite.X = 176;
                sprite.Y = 567;
                sprite.Url = "assets/Atlas/frame_common_watercolor.png";
                return sprite;
            }
            if (rarityFlags < CharacterRarityFlags.LR)
            {
                sprite.Width = 62;
                sprite.Height = 62;
                sprite.X = 694;
                sprite.Y = 738;
                sprite.Url = "assets/Atlas/frame_common_slice.png";
                return sprite;
            }

            sprite.Width = 62;
            sprite.Height = 62;
            sprite.X = 787;
            sprite.Y = 962;
            sprite.Url = "assets/Atlas/frame_common_lr_slice.png";

            return sprite;
        }

        public string GetCharacterFrameDecoration(CharacterRarityFlags characterRarityFlags)
        {
            if ((ClientConst.Icon.NeedFrameDecorationCharacterRarityFlags & characterRarityFlags) == characterRarityFlags)
            {
                return $"assets/Atlas/frame_decoration_{characterRarityFlags.ToString().ToLower()}.png";
            }

            return null;
        }


        public string GetIconCharacterElement(ElementType elementType)
        {
            return $"assets/Atlas/icon_element_{(int) elementType}.png";
        }


    }
}
