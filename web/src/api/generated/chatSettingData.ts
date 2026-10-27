/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IDeepCopy } from "./iDeepCopy";
import { ChatType } from "./chatType";
import { ChatBackgroundType } from "./chatBackgroundType";

export class ChatSettingData implements IDeepCopy<ChatSettingData> {
    balloonItemId: number;
    fontSize: number;
    backgroundTypeDictionary: { [key in ChatType]?: ChatBackgroundType; };
}
