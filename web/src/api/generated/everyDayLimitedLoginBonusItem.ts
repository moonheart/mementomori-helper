/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserCharacterItem } from "./iUserCharacterItem";
import { UserItem } from "./userItem";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class EveryDayLimitedLoginBonusItem implements IUserCharacterItem {
    everyDayRewardItem: UserItem;
    rarityFlags: CharacterRarityFlags;

    //<custom-body>
    get item(): UserItem {
        return this.everyDayRewardItem;
    }

    set item(value: UserItem) {
        this.everyDayRewardItem = value;
    }
    //</custom-body>
}
