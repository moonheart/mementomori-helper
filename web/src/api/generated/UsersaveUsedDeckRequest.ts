/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { DeckUseContentType } from "./deckUseContentType";

export class UserSaveUsedDeckRequest extends ApiRequestBase {
    copyPlayerId: number;
    deckUseContentType: DeckUseContentType;
    userCharacterGuids: string[];
    deckNo: number;
    useLockEquipmentInFriendBattle: boolean;
}
