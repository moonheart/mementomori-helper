using MementoMori.Ortega.Common;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.WebUI.UI
{
    public class AtlasManager
    {
        private const string AssetsPath = "_content/MementoMori.BlazorShared/assets";

        public string GetCharacterIcon(long characterId)
        {
            return $"{AssetsPath}/CharacterIcon/CHR_{characterId:000000}/CHR_{characterId:000000}_00_s.png";
        }

        public string GetCharacterFrame(CharacterRarityFlags characterRarityFlags)
        {
            if (characterRarityFlags > CharacterRarityFlags.LR)
            {
                characterRarityFlags = CharacterRarityFlags.LR;
            }

            return characterRarityFlags switch
            {
                CharacterRarityFlags.None => $"{AssetsPath}/Atlas/frame_common_watercolor.png",
                < CharacterRarityFlags.LR => $"{AssetsPath}/Atlas/frame_common_slice.png",
                _ => $"{AssetsPath}/Atlas/frame_common_lr_slice.png"
            };
        }

        public string GetItemFrame(ItemRarityFlags itemRarityFlags)
        {
            if (itemRarityFlags > ItemRarityFlags.LR)
            {
                itemRarityFlags = ItemRarityFlags.LR;
            }

            return itemRarityFlags switch
            {
                ItemRarityFlags.None => $"{AssetsPath}/Atlas/frame_common_watercolor.png",
                < ItemRarityFlags.LR => $"{AssetsPath}/Atlas/frame_common_slice.png",
                _ => $"{AssetsPath}/Atlas/frame_common_lr_slice.png"
            };
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

        public string GetSphereIcon(SphereMB sphereMb, bool isMini = false)
        {
            var filename = $"SPH_{sphereMb.CategoryId:00}{(int) (isMini ? SphereType.EquipmentIcon : sphereMb.SphereType):00}";
            return $"{AssetsPath}/Sphere/{filename}.png";
        }
    }
}