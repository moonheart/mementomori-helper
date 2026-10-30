import { useState, useEffect } from 'react';
import { Dialog, DialogContent, DialogDescription, DialogTitle } from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Loader2, X } from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { BookSortGrid } from './BookSortGrid';
import { BookSortSidebar } from './BookSortSidebar';
import { BookSortSyncData } from '@/api/generated/bookSortSyncData';
import { BookSortBookSortGetInfoResponse } from '@/api/generated/BookSortbookSortGetInfoResponse';
import { BookSortGridCellUnlockItemType } from '@/api/generated/bookSortGridCellUnlockItemType';
import { useLocalizationStore } from '@/store/localization-store';
import { ItemType } from '@/api/generated/itemType';
import { useToast } from '@/hooks/use-toast';
import { useItemName } from '@/hooks/useItemName';
import { BookSortBookSortUnlockGridCellResponse } from '@/api/generated/BookSortbookSortUnlockGridCellResponse';
import { BookSortBookSortUpFloorResponse } from '@/api/generated/BookSortbookSortUpFloorResponse';
import { BookSortBookSortBulkUnlockGridCellResponse } from '@/api/generated/BookSortbookSortBulkUnlockGridCellResponse';
import { useTranslation } from '@/hooks/useTranslation';

interface BookSortDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
}

export function BookSortDialog({ open, onOpenChange }: BookSortDialogProps) {
    const { t } = useLocalizationStore();
    const { t: translate } = useTranslation();
    const [isLoading, setIsLoading] = useState(false);
    const [isUnlocking, setIsUnlocking] = useState(false);
    const [syncData, setSyncData] = useState<BookSortSyncData | null>(null);
    const [selectedItemId, setSelectedItemId] = useState<number | null>(null);
    const [maxClearQuestId, setMaxClearQuestId] = useState<number>(0);
    const { toast } = useToast();
    const { getItemName, masterTables } = useItemName();

    const getUnlockItemDescription = (type: number) => {
        if (!masterTables.ItemTable) return translate('[BookSortItemListHeaderLabel]');
        const itemMb = masterTables.ItemTable.find(m => m.itemType === ItemType.BookSortGridCellUnlockItem && m.itemId === type);
        return itemMb ? (t(itemMb.descriptionKey) || itemMb.memo) : translate('[BookSortItemListHeaderLabel]');
    };

    const getUnlockItemName = (type: number) => {
        return getItemName(ItemType.BookSortGridCellUnlockItem, type);
    };

    const [items, setItems] = useState<{ id: number, count: number, name: string, desc: string }[]>([]);

    useEffect(() => {
        if (open) {
            fetchData();
        }
    }, [open]);

    useEffect(() => {
        if (items.length > 0 && (!selectedItemId || !items.find(i => i.id === selectedItemId))) {
            setSelectedItemId(items[0].id);
        } else if (items.length === 0 && selectedItemId !== null) {
            setSelectedItemId(null);
        }
    }, [items, selectedItemId]);

    const fetchData = async () => {
        try {
            setIsLoading(true);
            const [infoResponse, userResponse] = await Promise.all([
                ortegaApi.bookSort.getInfo({}),
                ortegaApi.user.getUserData({})
            ]) as [BookSortBookSortGetInfoResponse, any];

            if (infoResponse && infoResponse.bookSortSyncData) {
                setSyncData(infoResponse.bookSortSyncData);
            }

            if (userResponse && userResponse.userSyncData) {
                if (userResponse.userSyncData.userBattleBossDtoInfo?.bossClearMaxQuestId) {
                    setMaxClearQuestId(userResponse.userSyncData.userBattleBossDtoInfo.bossClearMaxQuestId);
                }

                const userItems = userResponse.userSyncData.userItemDtoInfo?.filter((x: any) => x.itemType === ItemType.BookSortGridCellUnlockItem) || [];
                const newItems = userItems.map((x: any) => ({
                    id: x.itemId,
                    count: x.itemCount,
                    name: getUnlockItemName(x.itemId),
                    desc: getUnlockItemDescription(x.itemId)
                })).filter((i: any) => i.count > 0);

                setItems(newItems);
            }
        } catch (error) {
            console.error('Failed to fetch BookSort info:', error);
        } finally {
            setIsLoading(false);
        }
    };

    const handleUnlockCell = async (index: number) => {
        if (!selectedItemId) return;

        try {
            setIsUnlocking(true);
            const response = await ortegaApi.bookSort.unlockGridCell({
                gridCellIndex: index,
                gridCellUnlockItemId: selectedItemId,
            }) as BookSortBookSortUnlockGridCellResponse;

            if (response && response.unlockGridCellResultList) {
                response.unlockGridCellResultList.forEach((result) => {
                    if (result.rewardUserItemList && result.rewardUserItemList.length > 0) {
                        result.rewardUserItemList.forEach((item) => {
                            toast({
                                title: translate('[DialogItemAcquisitionTitle]'),
                                description: translate('BOOKSORT_DIALOG_OBTAINED_ITEM_DESC', [getItemName(item.itemType, item.itemId), String(item.itemCount)]),
                            });
                        });
                    }
                });
            }

            if (response && response.bookSortSyncData) {
                setSyncData(response.bookSortSyncData);
            }

            if (response && response.userSyncData) {
                const updatedItemsDelta = response.userSyncData.userItemDtoInfo?.filter((x: any) => x.itemType === ItemType.BookSortGridCellUnlockItem) || [];
                setItems(prevItems => {
                    const nextItems = prevItems.map(item => ({ ...item }));
                    updatedItemsDelta.forEach((x: any) => {
                        const existingInfo = nextItems.find(i => i.id === x.itemId);
                        if (existingInfo) {
                            existingInfo.count = x.itemCount;
                        } else {
                            nextItems.push({
                                id: x.itemId,
                                count: x.itemCount,
                                name: getUnlockItemName(x.itemId),
                                desc: getUnlockItemDescription(x.itemId)
                            });
                        }
                    });
                    return nextItems.filter(i => i.count > 0);
                });
            }
        } catch (error) {
            console.error('Failed to unlock cell:', error);
        } finally {
            setIsUnlocking(false);
        }
    };

    const handleSelectBonusReward = async (index: number) => {
        try {
            setIsLoading(true);
            const response = await ortegaApi.bookSort.selectBonusFloorReward({
                bonusFloorRewardIndex: index
            });
            if (response && response.bookSortSyncData) {
                setSyncData(response.bookSortSyncData);
            }
        } catch (error) {
            console.error('Failed to select bonus reward:', error);
        } finally {
            setIsLoading(false);
        }
    };

    const handleUpFloor = async () => {
        try {
            setIsLoading(true);
            const response = await ortegaApi.bookSort.upFloor({}) as BookSortBookSortUpFloorResponse;
            if (response && response.bookSortSyncData) {
                setSyncData(response.bookSortSyncData);
            }
        } catch (error) {
            console.error('Failed to up floor:', error);
        } finally {
            setIsLoading(false);
        }
    };

    const handleBulkUnlockCell = async () => {
        try {
            setIsUnlocking(true);
            const response = await ortegaApi.bookSort.bulkUnlockGridCell({}) as BookSortBookSortBulkUnlockGridCellResponse;

            if (response && response.unlockGridCellResultList) {
                const rewardSummary: Record<string, { type: number, id: number, count: number, name: string }> = {};

                response.unlockGridCellResultList.forEach((result) => {
                    if (result.rewardUserItemList && result.rewardUserItemList.length > 0) {
                        result.rewardUserItemList.forEach((item) => {
                            const key = `${item.itemType}_${item.itemId}`;
                            if (!rewardSummary[key]) {
                                rewardSummary[key] = {
                                    type: item.itemType,
                                    id: item.itemId,
                                    count: 0,
                                    name: getItemName(item.itemType, item.itemId)
                                };
                            }
                            rewardSummary[key].count += item.itemCount;
                        });
                    }
                });

                Object.values(rewardSummary).forEach((item) => {
                    toast({
                        title: translate('[DialogItemAcquisitionTitle]'),
                        description: translate('BOOKSORT_DIALOG_OBTAINED_ITEM_DESC', [item.name, String(item.count)]),
                    });
                });
            }

            if (response && response.bookSortSyncData) {
                setSyncData(response.bookSortSyncData);
            }

            if (response && response.userSyncData) {
                const updatedItemsDelta = response.userSyncData.userItemDtoInfo?.filter((x: any) => x.itemType === ItemType.BookSortGridCellUnlockItem) || [];
                setItems(prevItems => {
                    const nextItems = prevItems.map(item => ({ ...item }));
                    updatedItemsDelta.forEach((x: any) => {
                        const existingInfo = nextItems.find(i => i.id === x.itemId);
                        if (existingInfo) {
                            existingInfo.count = x.itemCount;
                        } else {
                            nextItems.push({
                                id: x.itemId,
                                count: x.itemCount,
                                name: getUnlockItemName(x.itemId),
                                desc: getUnlockItemDescription(x.itemId)
                            });
                        }
                    });
                    return nextItems.filter(i => i.count > 0);
                });
            }
        } catch (error) {
            console.error('Failed to bulk unlock cells:', error);
        } finally {
            setIsUnlocking(false);
        }
    };

    if (!open) return null;

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-5xl h-[80vh] flex flex-col p-0 overflow-hidden bg-[url('/assets/textures/booksort-bg.jpg')] bg-cover bg-center border-primary/20">
                <DialogTitle className="sr-only">{translate('[BookSortMainViewControllerTitle]')}</DialogTitle>
                <DialogDescription className="sr-only">{translate('BOOKSORT_DIALOG_DESCRIPTION')}</DialogDescription>

                {/* Header */}
                <div className="flex items-center justify-between p-4 bg-background/80 border-b">
                    <div className="flex items-center gap-3">
                        <div className="h-8 w-1 bg-primary rounded-full" />
                        <h2 className="text-xl font-bold tracking-wider">{translate('[BookSortMainViewControllerTitle]')}</h2>
                    </div>
                </div>

                {/* Main Content Area */}
                <div className="flex-1 flex overflow-hidden">
                    {isLoading ? (
                        <div className="flex-1 flex items-center justify-center bg-background/50">
                            <div className="text-center space-y-4">
                                <Loader2 className="w-10 h-10 animate-spin text-primary mx-auto" />
                                <p className="text-sm text-muted-foreground animate-pulse tracking-widest">{translate('BOOKSORT_DIALOG_ENTERING')}</p>
                            </div>
                        </div>
                    ) : (
                        <div className="flex-1 flex">
                            {/* Left Panel: Character illustration & Sidebar */}
                            <div className="w-1/3 flex flex-col relative bg-gradient-to-t from-background/90 to-background/20 mix-blend-luminosity">


                                <div className="relative z-10 flex-1 flex flex-col h-full">
                                    <BookSortSidebar
                                        syncData={syncData}
                                        items={items}
                                        selectedItemId={selectedItemId}
                                        onSelectItem={setSelectedItemId}
                                        onOpenDispatch={() => { }} // TODO: Dispatch Dialog
                                        onOpenShop={() => { }} // TODO: Shop Dialog
                                    />
                                </div>
                            </div>

                            {/* Right Panel: The Grid */}
                            <div className="w-2/3 p-6 flex flex-col items-center justify-center">
                                <div className="w-full h-full max-w-3xl">
                                    <BookSortGrid
                                        syncData={syncData}
                                        maxClearQuestId={maxClearQuestId}
                                        onUnlockCell={handleUnlockCell}
                                        onBulkUnlock={handleBulkUnlockCell}
                                        onUpFloor={handleUpFloor}
                                        onSelectBonusReward={handleSelectBonusReward}
                                        isUnlocking={isUnlocking}
                                    />
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </DialogContent>
        </Dialog>
    );
}
