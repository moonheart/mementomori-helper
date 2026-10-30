/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { CharacterVoiceCategory } from "./characterVoiceCategory";
import { CharacterVoicePath } from "./characterVoicePath";
import { UnlockCharacterDetailVoiceType } from "./unlockCharacterDetailVoiceType";

export class CharacterDetailVoiceMB extends MasterBookBase {
    characterId: number;
    characterVoiceCategory: CharacterVoiceCategory;
    path: CharacterVoicePath;
    sortOrder: number;
    subtitleKey: string;
    unlockCondition: UnlockCharacterDetailVoiceType;
    unlockedVoiceButtonTextKey: string;
    unlockQuestId: number;
}
