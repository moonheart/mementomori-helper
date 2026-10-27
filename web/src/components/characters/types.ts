import { CharacterMB, ElementType, JobFlags, UserCharacterDtoInfo } from '@/api/generated';

export interface UICharacter extends UserCharacterDtoInfo {
    master?: CharacterMB;
    nameKey: string;
    element: ElementType;
    job: JobFlags;
}
