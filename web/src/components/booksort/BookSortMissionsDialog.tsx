import { useState, useMemo } from 'react';
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Progress } from '@/components/ui/progress';
import { Gift, CheckCircle2, Target, Loader2 } from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { MissionGroupType, MissionStatusType, MissionGetMissionInfoResponse, MissionInfo, MissionMB } from '@/api/generated';
import { useMasterData } from '@/hooks/useMasterData';
import { useLocalizationStore } from '@/store/localization-store';
import { useTranslation } from '@/hooks/useTranslation';

import { MissionRow, UIStoreMission } from '@/components/mission/MissionRow';
import { mapMissionsFromGroupInfo } from '@/components/mission/mission-utils';

interface BookSortMissionsDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
    missionData: MissionGetMissionInfoResponse | null;
    onFetchNeeded: () => void;
}

export function BookSortMissionsDialog({ open, onOpenChange, missionData, onFetchNeeded }: BookSortMissionsDialogProps) {
    const { t } = useTranslation();
    const [loading, setLoading] = useState(false);
    const [claiming, setClaiming] = useState<Set<number>>(new Set());

    const getGroupInfo = (): MissionInfo | undefined => {
        if (!missionData?.missionInfoDict) return undefined;
        const dict = missionData.missionInfoDict as Record<string | number, MissionInfo>;
        return dict[MissionGroupType.BookSort] || dict[MissionGroupType[MissionGroupType.BookSort]];
    };

    const mapMissions = (): UIStoreMission[] =>
        mapMissionsFromGroupInfo(getGroupInfo(), true);

    const missions = useMemo(() => mapMissions(), [missionData]);

    const handleClaimMission = async (missionId: number) => {
        setClaiming(prev => new Set(prev).add(missionId));
        try {
            await ortegaApi.mission.rewardMission({
                targetMissionIdList: [missionId]
            });
            onFetchNeeded();
        } catch (error) {
            console.error('Failed to claim mission:', error);
        } finally {
            setClaiming(prev => {
                const newSet = new Set(prev);
                newSet.delete(missionId);
                return newSet;
            });
        }
    };

    const handleClaimAll = async () => {
        const allClaimableMissions = missions
            .filter(m => m.status === MissionStatusType.NotReceived)
            .map(m => m.id);

        if (allClaimableMissions.length === 0) return;

        setLoading(true);
        try {
            await ortegaApi.mission.rewardMission({
                targetMissionIdList: allClaimableMissions
            });
            onFetchNeeded();
        } catch (error) {
            console.error('Failed to claim all rewards:', error);
        } finally {
            setLoading(false);
        }
    };

    const claimableCount = missions.filter(m => m.status === MissionStatusType.NotReceived).length;

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-4xl max-h-[85vh] flex flex-col pt-8">
                <DialogHeader className="flex flex-row items-center justify-between border-b pb-4 mb-2">
                    <div>
                        <DialogTitle className="text-2xl font-bold">{t('[BookSortMissionTitle]')}</DialogTitle>
                        <p className="text-sm text-muted-foreground mt-1">{t('BOOKSORT_DIALOG_MISSIONS_DESC')}</p>
                    </div>
                    {claimableCount > 0 && (
                        <Button onClick={handleClaimAll} disabled={loading} className="mr-8">
                            {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                            {t('BOOKSORT_DIALOG_CLAIM_ALL')}
                        </Button>
                    )}
                </DialogHeader>

                <div className="flex-1 overflow-y-auto px-1 py-4">
                    <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3">
                        {missions.length === 0 ? (
                            <div className="col-span-full text-center py-8 text-muted-foreground">{t('BOOKSORT_NO_MISSIONS')}</div>
                        ) : (
                            missions.map(m => (
                                <MissionRow
                                    key={m.id}
                                    mission={m}
                                    onClaim={() => handleClaimMission(m.id)}
                                    isClaiming={claiming.has(m.id)}
                                />
                            ))
                        )}
                    </div>
                </div>
            </DialogContent>
        </Dialog>
    );
}
