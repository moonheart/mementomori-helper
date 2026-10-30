import { useState, useEffect, useCallback } from 'react';
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Badge } from '@/components/ui/badge';
import { Loader2, Gift, Package, Trash2, CheckCheck, RefreshCw } from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { UserPresentDtoInfo } from '@/api/generated/userPresentDtoInfo';
import { PresentGetListResponse } from '@/api/generated/PresentgetListResponse';
import { PresentReceiveItemResponse } from '@/api/generated/PresentreceiveItemResponse';
import { LanguageType } from '@/api/generated/languageType';
import { ItemMB } from '@/api/generated/itemMB';
import { getItemIconUrl, getItemName } from '@/lib/itemUtils';
import { useMasterTable } from '@/hooks/useMasterData';
import { useLocalizationStore } from '@/store/localization-store';
import { useTranslation } from '@/hooks/useTranslation';
import { cn } from '@/lib/utils';

interface PresentBoxDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
}

/** 将毫秒时间戳格式化为日期字符串 */
function formatDate(unixMs: number): string {
    if (!unixMs) return '-';
    const d = new Date(unixMs);
    const yyyy = d.getFullYear();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const hh = String(d.getHours()).padStart(2, '0');
    const min = String(d.getMinutes()).padStart(2, '0');
    return `${yyyy}/${mm}/${dd} ${hh}:${min}`;
}

/** 计算剩余天数 */
function getRemainingDays(receiveLimitDate: number): number | null {
    if (!receiveLimitDate) return null;
    const nowMs = Date.now();
    const diffMs = receiveLimitDate - nowMs;
    return Math.ceil(diffMs / (1000 * 60 * 60 * 24));
}

export function PresentBoxDialog({ open, onOpenChange }: PresentBoxDialogProps) {
    const [loading, setLoading] = useState(false);
    const [actionLoading, setActionLoading] = useState<string | null>(null); // guid 或 'all' 或 'delete'
    const [error, setError] = useState<string | null>(null);
    const [presents, setPresents] = useState<UserPresentDtoInfo[]>([]);
    const [resultItems, setResultItems] = useState<PresentReceiveItemResponse['resultItems'] | null>(null);
    const { t: localize } = useLocalizationStore();
    const { t } = useTranslation();
    const currentLanguage = useLocalizationStore(state => state.currentLanguage);
    const { data: itemTable } = useMasterTable<ItemMB>('ItemTable');
    const masterTables = { ItemTable: itemTable ?? undefined };

    const getLanguageType = useCallback(() => {
        return LanguageType[currentLanguage as keyof typeof LanguageType] ?? LanguageType.zhCN;
    }, [currentLanguage]);

    const fetchPresents = useCallback(async () => {
        try {
            setLoading(true);
            setError(null);
            const resp = await ortegaApi.present.getList({
                languageType: getLanguageType(),
            }) as PresentGetListResponse;
            setPresents(resp?.userPresentDtoInfos ?? []);
        } catch (err) {
            console.error('Failed to fetch presents:', err);
            setError(t('PRESENT_BOX_LOAD_FAILED'));
        } finally {
            setLoading(false);
        }
    }, [t]);

    useEffect(() => {
        if (open) {
            fetchPresents();
            setResultItems(null);
        } else {
            setPresents([]);
            setError(null);
            setResultItems(null);
        }
    }, [open, fetchPresents]);

    /** 领取单个礼物 */
    const handleReceive = async (present: UserPresentDtoInfo) => {
        if (present.isReceived) return;
        try {
            setActionLoading(present.guid);
            const resp = await ortegaApi.present.receiveItem({
                languageType: getLanguageType(),
                presentGuid: present.guid,
            }) as PresentReceiveItemResponse;

            if (resp?.resultItems?.length) {
                setResultItems(resp.resultItems);
            }
            // 刷新列表
            await fetchPresents();
        } catch (err) {
            console.error('Failed to receive present:', err);
        } finally {
            setActionLoading(null);
        }
    };

    /** 全部领取（传 null 一次性领取所有未领取的礼物） */
    const handleReceiveAll = async () => {
        if (pendingCount === 0) return;
        try {
            setActionLoading('all');
            const resp = await ortegaApi.present.receiveItem({
                languageType: getLanguageType(),
                presentGuid: null!,
            }) as PresentReceiveItemResponse;
            if (resp?.resultItems?.length) {
                setResultItems(resp.resultItems);
            }
            await fetchPresents();
        } catch (err) {
            console.error('Failed to receive all presents:', err);
        } finally {
            setActionLoading(null);
        }
    };

    /** 删除已领取的礼物 */
    const handleDeleteReceived = async () => {
        const received = presents.filter(p => p.isReceived);
        if (received.length === 0) return;
        try {
            setActionLoading('delete');
            await ortegaApi.present.deletePresent({
                presentGuidList: received.map(p => p.guid),
            });
            await fetchPresents();
        } catch (err) {
            console.error('Failed to delete presents:', err);
        } finally {
            setActionLoading(null);
        }
    };

    const pendingCount = presents.filter(p => !p.isReceived).length;
    const receivedCount = presents.filter(p => p.isReceived).length;

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-2xl h-[80vh] flex flex-col p-0 gap-0 overflow-hidden">
                {/* 顶部标题 */}
                <DialogHeader className="px-6 pt-5 pb-3 border-b shrink-0">
                    <DialogTitle className="flex items-center gap-2 text-base">
                        <Gift className="h-4 w-4 text-red-500" />
                        {t('[MyPagePresentBoxButtonTitle]')}
                        {presents.length > 0 && (
                            <Badge variant="secondary" className="ml-1 text-xs">
                                {t('PRESENT_BOX_ITEM_COUNT', [String(presents.length)])}
                            </Badge>
                        )}
                    </DialogTitle>
                </DialogHeader>

                {/* 内容区 */}
                <div className="flex-1 min-h-0 flex flex-col">
                    {loading ? (
                        <div className="flex items-center justify-center flex-1 gap-2">
                            <Loader2 className="h-6 w-6 animate-spin text-primary" />
                            <span className="text-sm text-muted-foreground">{t('COMMON_LOADING')}</span>
                        </div>
                    ) : error ? (
                        <div className="flex flex-col items-center justify-center flex-1 gap-3">
                            <p className="text-sm text-destructive">{error}</p>
                            <Button variant="outline" size="sm" onClick={fetchPresents}>
                                <RefreshCw className="h-3.5 w-3.5 mr-1.5" />
                                {t('COMMON_RETRY')}
                            </Button>
                        </div>
                    ) : presents.length === 0 ? (
                        <div className="flex flex-col items-center justify-center flex-1 gap-3 text-muted-foreground">
                            <Package className="h-12 w-12 opacity-30" />
                            <p className="text-sm">{t('PRESENT_BOX_EMPTY')}</p>
                        </div>
                    ) : (
                        <>
                            {/* 领取结果提示 */}
                            {resultItems && resultItems.length > 0 && (
                                <div className="mx-4 mt-3 p-3 rounded-lg bg-green-50 dark:bg-green-950 border border-green-200 dark:border-green-800 shrink-0">
                                    <p className="text-xs font-medium text-green-700 dark:text-green-400 mb-2">✓ {t('PRESENT_BOX_RECEIVED_ITEMS')}</p>
                                    <div className="flex flex-wrap gap-2">
                                        {resultItems.map((ri, idx) => (
                                            <div key={idx} className="flex items-center gap-1.5 bg-white dark:bg-green-900/50 rounded px-2 py-1 text-xs shadow-sm">
                                                <img
                                                    src={getItemIconUrl(ri.item.itemType, ri.item.itemId, masterTables)}
                                                    alt={`item-${ri.item.itemId}`}
                                                    className="w-6 h-6 object-contain"
                                                    onError={(e) => { (e.target as HTMLImageElement).style.display = 'none'; }}
                                                />
                                                <span className="font-medium">×{ri.item.itemCount.toLocaleString()}</span>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            )}

                            {/* 注脚提示 */}
                            <p className="px-4 pt-3 text-[11px] text-red-500 shrink-0">
                                {t('[MyPagePresentBoxWarning]')}
                            </p>

                            {/* 礼物列表 */}
                            <ScrollArea className="flex-1 min-h-0 px-4 pb-2">
                                <div className="space-y-2 py-2">
                                    {[...presents]
                                        .sort((a, b) => {
                                            // 未领取排前面，同状态按创建时间降序
                                            if (a.isReceived !== b.isReceived) return a.isReceived ? 1 : -1;
                                            return b.createAt - a.createAt;
                                        })
                                        .map((present) => {
                                            const remaining = getRemainingDays(present.receiveLimitDate);
                                            const isExpiringSoon = remaining !== null && remaining <= 3;
                                            const isReceiving = actionLoading === present.guid;
                                            const isAllLoading = actionLoading === 'all';

                                            return (
                                                <div
                                                    key={present.guid}
                                                    className={cn(
                                                        'rounded-lg border p-3 transition-colors',
                                                        present.isReceived
                                                            ? 'bg-muted/40 border-muted opacity-60'
                                                            : 'bg-card border-border hover:border-primary/30'
                                                    )}
                                                >
                                                    <div className="flex items-start gap-3">
                                                        {/* 礼物图标 */}
                                                        <div className="shrink-0 w-10 h-10 rounded-md overflow-hidden bg-muted flex items-center justify-center">
                                                            <Gift className="w-6 h-6 text-red-400" />
                                                        </div>

                                                        {/* 内容 */}
                                                        <div className="flex-1 min-w-0">
                                                            {/* 标题行 */}
                                                            <div className="flex items-center gap-2 mb-1 flex-wrap">
                                                                <span className="text-sm font-medium leading-snug truncate">
                                                                    {present.title || t('[MyPageMenuButtonPresentBoxLabel]')}
                                                                </span>
                                                                {present.isReceived && (
                                                                    <Badge variant="outline" className="text-[10px] h-4 px-1 text-muted-foreground shrink-0">
                                                                        {t('[GuildTowerFloorRewardStatusReceived]')}
                                                                    </Badge>
                                                                )}
                                                            </div>

                                                            {/* 道具图标行 */}
                                                            {present.itemList?.length > 0 && (
                                                                <div className="flex flex-wrap gap-1.5 mb-2">
                                                                    {present.itemList.map((pItem, idx) => (
                                                                        <div
                                                                            key={idx}
                                                                            className="relative flex items-center justify-center w-10 h-10 rounded border border-border bg-muted/50"
                                                                            title={`${getItemName(pItem.item.itemType, pItem.item.itemId, masterTables, localize)} × ${pItem.item.itemCount.toLocaleString()}`}
                                                                        >
                                                                            <img
                                                                                src={getItemIconUrl(pItem.item.itemType, pItem.item.itemId, masterTables)}
                                                                                alt={`item-${pItem.item.itemId}`}
                                                                                className="w-8 h-8 object-contain"
                                                                                onError={(e) => { (e.target as HTMLImageElement).style.display = 'none'; }}
                                                                            />
                                                                            <span className="absolute bottom-0 right-0 text-[9px] font-bold text-white leading-none px-0.5 py-0.5"
                                                                                style={{ textShadow: '0 0 2px #000, 0 0 2px #000' }}
                                                                            >
                                                                                {pItem.item.itemCount >= 1000
                                                                                    ? `${(pItem.item.itemCount / 1000).toFixed(0)}K`
                                                                                    : pItem.item.itemCount}
                                                                            </span>
                                                                        </div>
                                                                    ))}
                                                                </div>
                                                            )}

                                                            {/* 期限信息行 */}
                                                            <div className="flex items-center gap-4 text-[11px] text-muted-foreground">
                                                                <span>
                                                                    {t('PRESENT_BOX_DEADLINE')}
                                                                    <span className={cn('font-medium', isExpiringSoon ? 'text-red-500' : 'text-foreground/70')}>
                                                                        {remaining !== null ? t('PRESENT_BOX_REMAINING_DAYS', [String(remaining)]) : '-'}
                                                                    </span>
                                                                </span>
                                                                <span>
                                                                    {t('PRESENT_BOX_RECEIVED_DATE', [formatDate(present.createAt)])}
                                                                </span>
                                                            </div>
                                                        </div>

                                                        {/* 领取按钮 */}
                                                        {!present.isReceived && (
                                                            <Button
                                                                size="sm"
                                                                variant="outline"
                                                                className="shrink-0 h-8 px-3 text-xs"
                                                                disabled={isReceiving || isAllLoading}
                                                                onClick={() => handleReceive(present)}
                                                            >
                                                                {isReceiving ? (
                                                                    <Loader2 className="h-3 w-3 animate-spin" />
                                                                ) : (
                                                                    t('[MyPagePresentBoxButtonReceive]')
                                                                )}
                                                            </Button>
                                                        )}
                                                    </div>
                                                </div>
                                            );
                                        })}
                                </div>
                            </ScrollArea>
                        </>
                    )}
                </div>

                {/* 底部操作栏 */}
                <div className="px-6 py-3 border-t shrink-0 flex items-center justify-between gap-2">
                    <div className="flex items-center gap-2">
                        <Button
                            variant="outline"
                            size="sm"
                            disabled={receivedCount === 0 || actionLoading === 'delete'}
                            onClick={handleDeleteReceived}
                            className="h-8 text-xs"
                        >
                            {actionLoading === 'delete' ? (
                                <Loader2 className="h-3 w-3 animate-spin mr-1.5" />
                            ) : (
                                <Trash2 className="h-3 w-3 mr-1.5" />
                            )}
                            {t('[MyPagePresentBoxButtonAllDelete]')}
                            {receivedCount > 0 && <span className="ml-1 text-muted-foreground">({receivedCount})</span>}
                        </Button>
                        <Button
                            variant="default"
                            size="sm"
                            disabled={pendingCount === 0 || actionLoading === 'all'}
                            onClick={handleReceiveAll}
                            className="h-8 text-xs"
                        >
                            {actionLoading === 'all' ? (
                                <Loader2 className="h-3 w-3 animate-spin mr-1.5" />
                            ) : (
                                <CheckCheck className="h-3 w-3 mr-1.5" />
                            )}
                            {t('[PopularityVoteRewardBulkReceiveLabel]')}
                            {pendingCount > 0 && <span className="ml-1 opacity-80">({pendingCount})</span>}
                        </Button>
                    </div>
                    <Button variant="outline" size="sm" className="h-8 text-xs" onClick={() => onOpenChange(false)}>
                        {t('COMMON_CLOSE')}
                    </Button>
                </div>
            </DialogContent>
        </Dialog>
    );
}
