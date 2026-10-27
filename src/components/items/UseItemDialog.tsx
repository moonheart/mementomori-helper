import { useState, useEffect } from 'react';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Loader2 } from 'lucide-react';
import { UserItemDtoInfo, UserItem, ItemMB } from '@/api/generated';
import { ortegaApi } from '@/api/ortega-client';
import { useToast } from '@/hooks/use-toast';
import { useLocalizationStore } from '@/store/localization-store';
import { useMasterStore } from '@/store/masterStore';
import { getItemName } from '@/lib/itemUtils';
import { useTranslation } from '@/hooks/useTranslation';

interface UseItemDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
    item: UserItemDtoInfo | null;
    itemName: string;
    onSuccess?: () => void;
}

interface RewardItemDisplay {
    name: string;
    count: number;
}

export function UseItemDialog({ open, onOpenChange, item, itemName, onSuccess }: UseItemDialogProps) {
    const [useCount, setUseCount] = useState(1);
    const [isUsing, setIsUsing] = useState(false);
    const [rewards, setRewards] = useState<RewardItemDisplay[]>([]);
    const [showRewards, setShowRewards] = useState(false);
    const { toast } = useToast();
    const t = useLocalizationStore((state) => state.t);
    const { t: translate } = useTranslation();
    const getTable = useMasterStore((state) => state.getTable);

    const handleUse = async () => {
        if (!item || useCount <= 0 || useCount > item.itemCount) {
            toast({
                variant: 'destructive',
                title: t('ACCOUNT_ERROR'),
                description: translate('USE_ITEM_INVALID_COUNT'),
            });
            return;
        }

        try {
            setIsUsing(true);

            const response = await ortegaApi.item.useAutoBattleRewardItem({
                itemType: item.itemId,
                useCount: useCount,
            });

            // 格式化奖励显示
            const itemTable = await getTable<ItemMB>('ItemTable');
            const masterTables = { ItemTable: itemTable };

            const formattedRewards = (response.rewardItemList || []).map((reward) => ({
                name: getItemName(reward.itemType, reward.itemId, masterTables, t),
                count: reward.itemCount,
            }));

            setRewards(formattedRewards);
            setShowRewards(true);

            toast({
                title: t('ACCOUNT_SUCCESS'),
                description: translate('ITEMS_USE_SUCCESS', [String(useCount), itemName]),
            });

            // 通知父组件刷新数据
            onSuccess?.();
        } catch (error) {
            console.error('Failed to use item:', error);
            toast({
                variant: 'destructive',
                title: t('ACCOUNT_ERROR'),
                description: translate('ITEMS_USE_FAILED'),
            });
        } finally {
            setIsUsing(false);
        }
    };

    const handleClose = () => {
        setUseCount(1);
        setShowRewards(false);
        setRewards([]);
        onOpenChange(false);
    };

    // 重置状态当对话框打开时
    useEffect(() => {
        if (open) {
            setUseCount(1);
            setShowRewards(false);
            setRewards([]);
        }
    }, [open]);

    if (!item) return null;

    return (
        <Dialog open={open} onOpenChange={handleClose}>
            <DialogContent className="sm:max-w-[425px]">
                {!showRewards ? (
                    <>
                        <DialogHeader>
                            <DialogTitle>{t('[ItemBoxItemUseTitle]') || '使用道具'}</DialogTitle>
                            <DialogDescription>
                                {translate('USE_ITEM_SELECT_COUNT', [itemName])}
                            </DialogDescription>
                        </DialogHeader>

                        <div className="grid gap-4 py-4">
                            <div className="grid grid-cols-4 items-center gap-4">
                                <Label htmlFor="count" className="text-right">
                                    {translate('USE_ITEM_QUANTITY')}
                                </Label>
                                <div className="col-span-3 space-y-2">
                                    <Input
                                        id="count"
                                        type="number"
                                        min={1}
                                        max={item.itemCount}
                                        value={useCount}
                                        onChange={(e) => setUseCount(parseInt(e.target.value) || 1)}
                                        disabled={isUsing}
                                    />
                                    <p className="text-sm text-muted-foreground">
                                        {translate('USE_ITEM_OWNED', [String(item.itemCount)])}
                                    </p>
                                </div>
                            </div>

                            <div className="flex gap-2">
                                <Button
                                    type="button"
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setUseCount(1)}
                                    disabled={isUsing}
                                >
                                    {translate('USE_ITEM_MIN')}
                                </Button>
                                <Button
                                    type="button"
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setUseCount(Math.max(1, Math.floor(item.itemCount / 2)))}
                                    disabled={isUsing}
                                >
                                    {translate('USE_ITEM_HALF')}
                                </Button>
                                <Button
                                    type="button"
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setUseCount(item.itemCount)}
                                    disabled={isUsing}
                                >
                                    {translate('USE_ITEM_MAX')}
                                </Button>
                            </div>
                        </div>

                        <DialogFooter>
                            <Button variant="outline" onClick={handleClose} disabled={isUsing}>
                                {translate('COMMON_CANCEL')}
                            </Button>
                            <Button onClick={handleUse} disabled={isUsing}>
                                {isUsing && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                                {t('[ItemBoxButtonUse]') || '使用'}
                            </Button>
                        </DialogFooter>
                    </>
                ) : (
                    <>
                        <DialogHeader>
                            <DialogTitle>{translate('USE_ITEM_REWARDS_TITLE')}</DialogTitle>
                            <DialogDescription>
                                {translate('USE_ITEM_REWARDS_DESC', [String(useCount), itemName])}
                            </DialogDescription>
                        </DialogHeader>

                        <div className="max-h-[400px] overflow-y-auto">
                            {rewards.length > 0 ? (
                                <div className="space-y-2">
                                    {rewards.map((reward, index) => (
                                        <div
                                            key={index}
                                            className="flex items-center justify-between p-3 bg-muted rounded-lg"
                                        >
                                            <span className="font-medium">{reward.name}</span>
                                            <span className="text-primary font-bold">×{reward.count.toLocaleString()}</span>
                                        </div>
                                    ))}
                                </div>
                            ) : (
                                <div className="text-center py-8 text-muted-foreground">
                                    {translate('USE_ITEM_NO_REWARDS')}
                                </div>
                            )}
                        </div>

                        <DialogFooter>
                            <Button onClick={handleClose}>{translate('COMMON_CONFIRM')}</Button>
                        </DialogFooter>
                    </>
                )}
            </DialogContent>
        </Dialog>
    );
}
