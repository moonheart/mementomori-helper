import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';
import { Progress } from '@/components/ui/progress';
import { Badge } from '@/components/ui/badge';
import { Sparkles, Key, Gift, Lock, Zap } from 'lucide-react';
import { BookSortSyncData } from '@/api/generated/bookSortSyncData';
import { BookSortBonusFloorRewardMB } from '@/api/generated/bookSortBonusFloorRewardMB';
import { useMasterStore } from '@/store/masterStore';
import { useItemName } from '@/hooks/useItemName';
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
} from "@/components/ui/alert-dialog"
import { useLocalizationStore } from '@/store/localization-store';
import { useTranslation } from '@/hooks/useTranslation';

interface BookSortGridProps {
    syncData: BookSortSyncData | null;
    maxClearQuestId: number;
    onUnlockCell: (index: number) => void;
    onBulkUnlock: () => void;
    onUpFloor: () => void;
    onSelectBonusReward: (index: number) => void;
    isUnlocking: boolean;
}

export function BookSortGrid({ syncData, maxClearQuestId, onUnlockCell, onBulkUnlock, onUpFloor, onSelectBonusReward, isUnlocking }: BookSortGridProps) {
    const { t } = useLocalizationStore();
    const { t: translate } = useTranslation();
    const [confirmOpen, setConfirmOpen] = useState(false);
    // Master data
    const getTable = useMasterStore(state => state.getTable);
    const [bonusFloorRewardList, setBonusFloorRewardList] = useState<BookSortBonusFloorRewardMB[]>([]);
    const { getItemName } = useItemName();

    useEffect(() => {
        getTable<BookSortBonusFloorRewardMB>('BookSortBonusFloorRewardTable').then(setBonusFloorRewardList);
    }, [getTable]);

    // 7x5 grid = 35 cells
    const totalCells = 35;
    const currentFloor = syncData?.currentFloor || 1;

    const isBonusFloor = currentFloor % 5 === 0;
    const isNeedSelectBonusReward = isBonusFloor && syncData?.selectedBonusFloorRewardIndex === -1;
    const currentBonusFloorReward = bonusFloorRewardList.find(r => r.startFloor <= currentFloor && r.endFloor >= currentFloor);

    // Convert array to a map for easy lookup
    const unlockedCellsMap = new Map(
        (syncData?.unlockedGridCellInfoList || []).map(cell => [cell.gridCellIndex, cell])
    );

    const isKeyFound = Array.from(unlockedCellsMap.values()).some(cell => cell.isKey);

    const renderCell = (index: number) => {
        const cellData = unlockedCellsMap.get(index);
        const isUnlocked = !!cellData;

        return (
            <div
                key={index}
                className={`
                relative aspect-square border rounded-lg overflow-hidden transition-all duration-300 flex items-center justify-center
                ${isUnlocked
                        ? (cellData?.isWin ? 'border-purple-500 bg-purple-500/20 hover:border-purple-400 cursor-pointer shadow-[inset_0_0_10px_rgba(168,85,247,0.3)]' : 'border-primary/30 bg-background/80')
                        : 'border-white/30 bg-black/20 hover:border-primary/50 cursor-pointer'}
            `}
                onClick={() => {
                    if (!isUnlocked && !isUnlocking) {
                        onUnlockCell(index);
                    } else if (isUnlocked && cellData?.isWin && isKeyFound) {
                        setConfirmOpen(true);
                    }
                }}
            >
                {!isUnlocked ? (
                    <Lock className="w-5 h-5 text-white/50" strokeWidth={1.5} />
                ) : (
                    <div className="w-full h-full flex items-center justify-center">
                        {cellData.isKey && (
                            <Key className="w-8 h-8 text-yellow-500 animate-pulse drop-shadow-[0_0_8px_rgba(234,179,8,0.5)]" />
                        )}
                        {cellData.isWin && !cellData.isKey && (
                            <Gift className="w-8 h-8 text-purple-500 animate-bounce drop-shadow-[0_0_8px_rgba(168,85,247,0.5)]" />
                        )}
                        {!cellData.isKey && !cellData.isWin && (
                            <span className="text-xs text-muted-foreground">{translate('BOOKSORT_GRID_CLEANED')}</span>
                        )}
                    </div>
                )}
            </div>
        );
    };

    return (
        <Card className="h-full flex flex-col border-none shadow-none bg-transparent">
            <CardContent className="p-0 flex-1 flex flex-col">
                {/* Header info */}
                <div className="flex items-center justify-between mb-4 bg-background/50 p-3 rounded-lg border">
                    <div className="flex items-center gap-3">
                        <span className="text-xl font-bold tracking-widest text-primary drop-shadow-sm">
                            {translate('[DialogCommonFormationTowerBattleQuestNameFormat]', [String(currentFloor)])}
                        </span>
                        {/* Floor progression visual (simplified) */}
                        <div className="flex items-center gap-1 ml-4 opacity-80">
                            {[1, 2, 3, 4, 5].map((step) => (
                                <div key={step} className="flex items-center">
                                    <Sparkles className={`w-4 h-4 ${step === ((currentFloor - 1) % 5) + 1 ? 'text-primary animate-pulse' : 'text-muted-foreground'}`} />
                                    {step < 5 && <div className="h-[2px] w-6 bg-muted-foreground/30 mx-1 rounded-full" />}
                                </div>
                            ))}
                            <Gift className="w-5 h-5 text-pink-500 ml-2 animate-bounce" />
                        </div>
                    </div>
                    <Button
                        onClick={onBulkUnlock}
                        disabled={isUnlocking}
                        className="bg-primary/20 hover:bg-primary/40 text-primary border border-primary/30 shadow-[0_0_15px_rgba(var(--primary),0.2)] transition-all"
                    >
                        <Zap className="w-4 h-4 mr-2 text-yellow-400" />
                        {translate('BOOKSORT_GRID_BULK_USE')}
                    </Button>
                </div>

                {/* Grid */}
                <div className="bg-secondary/50 p-4 sm:p-6 rounded-2xl border shadow-sm relative">
                    <div className="grid grid-cols-7 gap-2 sm:gap-3">
                        {Array.from({ length: totalCells }).map((_, i) => renderCell(i))}
                    </div>
                    {isUnlocking && (
                        <div className="absolute inset-0 bg-background/20 rounded-xl flex items-center justify-center z-10">
                            <Sparkles className="w-8 h-8 text-primary animate-spin" />
                        </div>
                    )}
                    {isNeedSelectBonusReward && (
                        <div className="absolute inset-0 bg-background/95 z-30 p-4 sm:p-6 flex flex-col items-center justify-center rounded-2xl w-full h-full">
                            <h3 className="text-xl sm:text-2xl font-bold mb-4 text-primary drop-shadow-[0_0_8px_rgba(var(--primary),0.5)] flex items-center">
                                <Gift className="w-6 h-6 mr-2" />
                                {t('[BookSortBonusFloorSelectItemTitle]')}
                            </h3>
                            <p className="text-sm text-muted-foreground mb-6 text-center max-w-sm" dangerouslySetInnerHTML={{ __html: t('[BookSortBonusFloorSelectItemMessage]') }} />
                            <div className="grid gap-3 w-full max-w-lg mb-4 overflow-y-auto pr-2 custom-scrollbar" style={{ maxHeight: '60%' }}>
                                {currentBonusFloorReward?.selectItemsList?.map((selectItem, index) => {
                                    const isAvailable = selectItem.startMaxClearQuestId <= maxClearQuestId && selectItem.endMaxClearQuestId >= maxClearQuestId;
                                    if (!isAvailable) return null;

                                    return (
                                        <Card key={index} className="hover:border-primary/50 cursor-pointer transition-colors shadow-none bg-background/60 border-primary/20 hover:bg-primary/5 shrink-0" onClick={() => onSelectBonusReward(index)}>
                                            <CardContent className="p-3 sm:p-4 flex items-center justify-between gap-4">
                                                <div className="flex flex-wrap gap-2 flex-1">
                                                    {selectItem.itemList?.map((item, idx) => (
                                                        <Badge key={idx} variant="secondary" className="px-2 py-1 text-xs border-primary/20 bg-background">
                                                            {getItemName(item.itemType, item.itemId)} x{item.itemCount}
                                                        </Badge>
                                                    ))}
                                                </div>
                                                <Button size="sm" variant="default" className="shrink-0 shadow-[0_0_10px_rgba(var(--primary),0.3)] bg-gradient-to-r from-primary to-primary/80">
                                                    {translate('[ItemBoxButtonSelect]')}
                                                </Button>
                                            </CardContent>
                                        </Card>
                                    );
                                })}
                            </div>
                        </div>
                    )}
                </div>

                <AlertDialog open={confirmOpen} onOpenChange={setConfirmOpen}>
                    <AlertDialogContent>
                        <AlertDialogHeader>
                            <AlertDialogTitle>{t('[BookSortFloorMoveConfirmTitle]')}</AlertDialogTitle>
                            <AlertDialogDescription>
                                {t('[BookSortFloorMoveConfirmMessage]')}
                            </AlertDialogDescription>
                        </AlertDialogHeader>
                        <AlertDialogFooter>
                            <AlertDialogCancel>{translate('COMMON_CANCEL')}</AlertDialogCancel>
                            <AlertDialogAction onClick={() => {
                                setConfirmOpen(false);
                                onUpFloor();
                            }}>
                                {translate('COMMON_CONFIRM')}
                            </AlertDialogAction>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialog>
            </CardContent>
        </Card>
    );
}
