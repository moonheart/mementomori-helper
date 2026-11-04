import { Card, CardContent } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Progress } from '@/components/ui/progress';
import { Button } from '@/components/ui/button';
import { Gift, CheckCircle2, Target, Loader2 } from 'lucide-react';
import { useMasterData } from '@/hooks/useMasterData';
import { useLocalizationStore } from '@/store/localization-store';
import { useTranslation } from '@/hooks/useTranslation';
import { MissionStatusType, MissionMB } from '@/api/generated';

export interface UIStoreMission {
    id: number;
    missionType: number;
    status: MissionStatusType;
    progress: number;
    // 以下通过 Master 数据填充
    title?: string;
    description?: string;
    target?: number;
}

interface MissionRowProps {
    mission: UIStoreMission;
    onClaim: () => void;
    isClaiming: boolean;
}

export function MissionRow({ mission, onClaim, isClaiming }: MissionRowProps) {
    const { data: mb } = useMasterData<MissionMB>('MissionTable', mission.id);
    const localize = useLocalizationStore(state => state.t);
    const { t } = useTranslation();

    const title = mb ? localize(mb.nameKey, [mb.requireValue]) : t('MISSION_FALLBACK_TITLE', [String(mission.id)]);
    const description = mb ? localize(mb.descriptionKey, [mb.requireValue]) : t('MISSION_FALLBACK_TYPE', [String(mission.missionType)]);
    const target = mb ? mb.requireValue : 0;

    const isCompleted = mission.status === MissionStatusType.NotReceived || mission.status === MissionStatusType.Received;
    const isClaimed = mission.status === MissionStatusType.Received;
    const canClaim = mission.status === MissionStatusType.NotReceived;

    const progress = target ? (mission.progress / target) * 100 : 0;

    return (
        <Card className={canClaim ? 'border-2 border-primary shadow-md h-full' : 'h-full'}>
            <CardContent className="flex flex-col gap-3 p-4 h-full">
                <div className="flex items-center gap-3">
                    <div className={`flex h-10 w-10 shrink-0 items-center justify-center rounded-full ${canClaim ? 'bg-primary text-primary-foreground' : isClaimed ? 'bg-green-100 dark:bg-green-950 text-green-600' : 'bg-muted'}`}>
                        {isClaimed ? <CheckCircle2 className="h-6 w-6" /> : <Target className="h-6 w-6" />}
                    </div>
                    <div className="min-w-0 flex-1">
                        <h3 className="font-semibold text-sm truncate">{title}</h3>
                        <p className="text-xs text-muted-foreground truncate">{description}</p>
                    </div>
                </div>

                <div className="flex-1 flex flex-col justify-end space-y-3">
                    <div className="flex items-center justify-between">
                        {isCompleted ? (
                            <Badge variant={canClaim ? 'default' : 'secondary'}>
                                {canClaim ? t('MISSION_CLAIMABLE') : t('MISSION_RECEIVED')}
                            </Badge>
                        ) : mission.status === MissionStatusType.Locked ? (
                            <Badge variant="outline">{t('MISSION_UNLOCKED')}</Badge>
                        ) : (
                            <Badge variant="outline">{t('MISSION_IN_PROGRESS')}</Badge>
                        )}
                    </div>

                    {mission.status === MissionStatusType.Progress && target > 0 && (
                        <div className="space-y-1.5">
                            <div className="flex items-center justify-between text-[10px] text-muted-foreground">
                                <span>{t('MISSION_PROGRESS')}</span>
                                <span>{mission.progress}/{target}</span>
                            </div>
                            <Progress value={progress} className="h-1.5" />
                        </div>
                    )}

                    {canClaim && (
                        <Button className="w-full mt-2" size="sm" onClick={onClaim} disabled={isClaiming}>
                            {isClaiming ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : <Gift className="mr-2 h-4 w-4" />}
                            {t('MISSION_CLAIM_REWARD')}
                        </Button>
                    )}
                </div>
            </CardContent>
        </Card>
    );
}
