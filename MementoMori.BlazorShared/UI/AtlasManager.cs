using AutoCtor;
using Injectio.Attributes;
using MementoMori.Ortega.Common;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.WebUI.UI
{
    [RegisterSingleton]
    public partial class AtlasManager
    {
        public const string AssetsUrl = "https://list.moonheart.dev/d/public/mmtm";

        public string GetCharacterIcon(long characterId)
        {
            return $"{AssetsUrl}/AddressableConvertAssets/CharacterIcon/CHR_{characterId:000000}/CHR_{characterId:000000}_00_s.png";
        }

        public string GetCharacterFrame(CharacterRarityFlags characterRarityFlags)
        {
            if (characterRarityFlags > CharacterRarityFlags.LR)
            {
                characterRarityFlags = CharacterRarityFlags.LR;
            }

            return characterRarityFlags switch
            {
                CharacterRarityFlags.None => $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_common_watercolor.png",
                < CharacterRarityFlags.LR => $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_common_slice.png",
                _ => $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_common_lr_slice.png"
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
                ItemRarityFlags.None => $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_common_watercolor.png",
                < ItemRarityFlags.LR => $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_common_slice.png",
                _ => $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_common_lr_slice.png"
            };
        }

        public string GetCharacterFrameDecoration(CharacterRarityFlags characterRarityFlags)
        {
            if ((ClientConst.Icon.NeedFrameDecorationCharacterRarityFlags & characterRarityFlags) == characterRarityFlags)
            {
                return $"{AssetsUrl}/AddressableLocalAssets/Atlas/frame_decoration_{characterRarityFlags.ToString().ToLower()}.png";
            }

            return null;
        }

        public string GetIconCharacterElement(ElementType elementType)
        {
            return $"{AssetsUrl}/AddressableLocalAssets/Atlas/icon_element_{(int) elementType}.png";
        }

        public string GetSphereIcon(SphereMB sphereMb, bool isMini = false)
        {
            var filename = $"SPH_{sphereMb.CategoryId:00}{(int) (isMini ? SphereType.EquipmentIcon : sphereMb.SphereType):00}";
            return $"{AssetsUrl}/AddressableLocalAssets/Icon/Sphere/{filename}.png";
        }
    }
}