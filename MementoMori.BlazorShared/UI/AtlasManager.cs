using MementoMori.Ortega.Common;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.WebUI.UI
{
    public class AtlasManager
    {
        private const string AssetsPath = "_content/MementoMori.BlazorShared/assets";
        public string GetCharacterIcon(long characterId)
        {
            return $"{AssetsPath}/CharacterIcon/CHR_{characterId:000000}/CHR_{characterId:000000}_00_s.png";
        }
        
        public Sprite GetCharacterFrame(CharacterRarityFlags characterRarityFlags)
        {
            if (characterRarityFlags > CharacterRarityFlags.LR)
            {
                characterRarityFlags = CharacterRarityFlags.LR;
            }
            var sprite = new Sprite();
            GetFrameCharacter(characterRarityFlags, sprite);
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
                sprite.Url = $"{AssetsPath}/Atlas/frame_common_watercolor.png";
                return sprite;
            }
            if (rarityFlags < CharacterRarityFlags.LR)
            {
                sprite.Width = 62;
                sprite.Height = 62;
                sprite.X = 694;
                sprite.Y = 738;
                sprite.Url = $"{AssetsPath}/Atlas/frame_common_slice.png";
                return sprite;
            }

            sprite.Width = 62;
            sprite.Height = 62;
            sprite.X = 787;
            sprite.Y = 962;
            sprite.Url = $"{AssetsPath}/Atlas/frame_common_lr_slice.png";

            return sprite;
        }

        public string GetCharacterFrameDecoration(CharacterRarityFlags characterRarityFlags)
        {
            if ((ClientConst.Icon.NeedFrameDecorationCharacterRarityFlags & characterRarityFlags) == characterRarityFlags)
            {
                return $"{AssetsPath}/Atlas/frame_decoration_{characterRarityFlags.ToString().ToLower()}.png";
            }

            return null;
        }


        public string GetIconCharacterElement(ElementType elementType)
        {
            return $"_content/MementoMori.BlazorShared/assets/Atlas/icon_element_{(int) elementType}.png";
        }


    }
}
