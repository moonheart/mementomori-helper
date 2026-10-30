/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { LocalNotificationSendType } from "./localNotificationSendType";
import { LocalNotificationType } from "./localNotificationType";

export class LocalPushNotificationMB extends MasterBookBase implements IHasStartEndTime {
    groupId: number;
    localNotificationSendType: LocalNotificationSendType;
    localNotificationType: LocalNotificationType;
    messageKey: string;
    priority: number;
    sendTime: string;
    titleKey: string;
    endTime: string;
    startTime: string;
}
