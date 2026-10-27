/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasJstStartEndTime } from "./iHasJstStartEndTime";
import { PopularityVoteVotingType } from "./popularityVoteVotingType";
import { PopularityVoteSortType } from "./popularityVoteSortType";
import { ResultPresentationSetting } from "./resultPresentationSetting";
import { EntryCharacter } from "./entryCharacter";
import { PastChampionCharacter } from "./pastChampionCharacter";
import { EntryGroup } from "./entryGroup";
import { PopularityVoteReward } from "./popularityVoteReward";

export class PopularityVoteMB extends MasterBookBase implements IHasJstStartEndTime {
    votingType: PopularityVoteVotingType;
    sortType: PopularityVoteSortType;
    startTimeFixJST: string;
    endTimeFixJST: string;
    preliminaryStartTimeFixJst: string;
    preliminaryEndTimeFixJst: string;
    finalStartTimeFixJst: string;
    finalEndTimeFixJst: string;
    finalResultStartTimeFixJst: string;
    preliminaryInterimStartTimeFixJst: string;
    finalInterimStartTimeFixJst: string;
    missionStartTime: string;
    missionEndTime: string;
    missionResetTime: string;
    targetMissionIdList: number[];
    finalCharacterCount: number;
    resultPresentationSettingList: ResultPresentationSetting[];
    entryCharacterList: EntryCharacter[];
    pastChampionCharacterList: PastChampionCharacter[];
    groupList: EntryGroup[];
    popularityVoteRewardList: PopularityVoteReward[];
}
