/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserCharacterItem } from "./iUserCharacterItem";
import { UserItem } from "./userItem";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class SpecialLimitedLoginBonusItem implements IUserCharacterItem {
    date: number;
    specialRewardItem: UserItem;
    rarityFlags: CharacterRarityFlags;

    //<custom-body>
    get item(): UserItem {
        return this.specialRewardItem;
    }

    set item(value: UserItem) {
        this.specialRewardItem = value;
    }
    //</custom-body>
}
