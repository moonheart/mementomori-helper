import { useCallback, useEffect, useState } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Input } from '@/components/ui/input';
import { AlertDialog, AlertDialogAction, AlertDialogCancel, AlertDialogContent, AlertDialogDescription, AlertDialogFooter, AlertDialogHeader, AlertDialogTitle } from '@/components/ui/alert-dialog';
import { Loader2, RefreshCcw } from 'lucide-react';
import { rpcClient } from '@/api/rpc-client';
import { useAccountStore } from '@/store/accountStore';
import { useTranslation } from '@/hooks/useTranslation';
import { useItemName } from '@/hooks/useItemName';
import { toast } from '@/hooks/use-toast';
import { getItemIconUrl, getUserItemCount } from '@/lib/itemUtils';
import { parseOrtegaError } from '@/lib/errorHandler';
import { ItemType } from '@/api/generated/itemType';
import type { UserItem } from '@/api/generated/userItem';

// Hand-written: these endpoints are not in the generated ortega client yet.
interface LegendLeagueIconReward {
    id: number;
    consumeItemCount: number;
    giveItem: UserItem;
    limitTradeType: number; // 1 Permanent, 2 Monthly
    tradeCount: number;
    limitTradeCount: number;
    order: number;
    endLocalTime: number;
}
interface IconRewardInfoResponse {
    legendLeagueIconRewardList: LegendLeagueIconReward[] | null;
}
interface StarsGuidanceConsumeItemInfo {
    consumeItemType: ItemType;
    consumeItemId: number;
    consumeItemCount: number;
    giveItemCount: number;
}
interface StarsGuidanceInfoResponse {
    starsGuidanceTradeShopId: number;
    endLocalTimestamp: number;
    giveItemType: ItemType;
    giveItemId: number;
    todayTradeCount: number;
    limitTradeCount: number;
    starsGuidanceConsumeItemInfoList: StarsGuidanceConsumeItemInfo[] | null;
}

// Legend League icon rewards are paid with Icon Stamps (ItemType.IconFragment, id 1).
const ICON_STAMP = { itemType: ItemType.IconFragment, itemId: 1 };

type ItemNames = ReturnType<typeof useItemName>;

function ItemChip({ itemType, itemId, count, names }: { itemType: ItemType; itemId: number; count?: number; names: ItemNames }) {
    const { getItemName, masterTables } = names;
    return (
        <span className="inline-flex items-center gap-1.5">
            <img
                src={getItemIconUrl(itemType, itemId, masterTables)}
                alt=""
                className="w-7 h-7 object-contain"
                onError={(e) => { (e.target as HTMLImageElement).style.display = 'none'; }}
            />
            <span className="font-medium">{getItemName(itemType, itemId)}</span>
            {count != null && <span className="text-muted-foreground">×{count.toLocaleString()}</span>}
        </span>
    );
}

export function LegendLeagueIconShop() {
    const { t } = useTranslation();
    const names = useItemName();
    const { getItemName } = names;
    const items = useAccountStore((s) => s.getCurrentUserSyncData())?.userItemDtoInfo;
    const stamps = getUserItemCount(items, ICON_STAMP.itemType, ICON_STAMP.itemId);

    const [rewards, setRewards] = useState<LegendLeagueIconReward[] | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [pending, setPending] = useState<{ reward: LegendLeagueIconReward; count: number } | null>(null);
    const [buying, setBuying] = useState(false);

    const load = useCallback(async () => {
        setLoading(true);
        try {
            const res = await rpcClient.call<IconRewardInfoResponse>('legendLeague/getLegendLeagueIconRewardInfo', {});
            setRewards([...(res.legendLeagueIconRewardList ?? [])].sort((a, b) => b.order - a.order));
            setError(null);
        } catch (e) {
            setError(parseOrtegaError(e).message);
        } finally {
            setLoading(false);
        }
    }, []);
    useEffect(() => { load(); }, [load]);

    const maxBuy = (r: LegendLeagueIconReward) =>
        Math.max(0, Math.min(r.limitTradeCount - r.tradeCount, Math.floor(stamps / r.consumeItemCount)));

    const buy = async () => {
        if (!pending) return;
        setBuying(true);
        try {
            await rpcClient.call('legendLeague/buyLegendLeagueIconReward', { iconRewardId: pending.reward.id, buyCount: pending.count });
            toast({ title: t('SPEND_DONE') });
            setPending(null);
            load();
        } catch (e) {
            toast({ title: t('SPEND_FAILED'), description: parseOrtegaError(e).message, variant: 'destructive' });
        } finally {
            setBuying(false);
        }
    };

    return (
        <Card>
            <CardHeader>
                <div className="flex items-center justify-between gap-2">
                    <div>
                        <CardTitle>{t('LL_ICON_SHOP_TITLE')}</CardTitle>
                        <CardDescription className="flex items-center gap-1">
                            {t('SPEND_YOU_HAVE')} <ItemChip {...ICON_STAMP} count={stamps} names={names} />
                        </CardDescription>
                    </div>
                    <Button variant="outline" size="sm" onClick={load} disabled={loading}>
                        <RefreshCcw className={`h-4 w-4 ${loading ? 'animate-spin' : ''}`} />
                    </Button>
                </div>
            </CardHeader>
            <CardContent>
                {error ? (
                    <p className="text-sm text-muted-foreground">{error}</p>
                ) : !rewards ? (
                    <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />
                ) : (
                    <div className="grid gap-3 md:grid-cols-2 xl:grid-cols-3">
                        {rewards.map((r) => {
                            const left = r.limitTradeCount - r.tradeCount;
                            const max = maxBuy(r);
                            return (
                                <div key={r.id} className="rounded-lg border bg-card p-3 space-y-2">
                                    <ItemChip itemType={r.giveItem.itemType} itemId={r.giveItem.itemId} count={r.giveItem.itemCount} names={names} />
                                    <div className="flex items-center justify-between text-sm">
                                        <span className="flex items-center gap-1 text-muted-foreground">
                                            {t('SPEND_COST')} {r.consumeItemCount.toLocaleString()}
                                        </span>
                                        <Badge variant="outline">
                                            {t(r.limitTradeType === 2 ? 'SPEND_LIMIT_MONTHLY' : 'SPEND_LIMIT', [r.tradeCount, r.limitTradeCount])}
                                        </Badge>
                                    </div>
                                    <Button size="sm" className="w-full" disabled={max < 1}
                                        onClick={() => setPending({ reward: r, count: 1 })}>
                                        {left < 1 ? t('SPEND_SOLD_OUT') : max < 1 ? t('SPEND_NOT_ENOUGH') : t('SPEND_BUY')}
                                    </Button>
                                </div>
                            );
                        })}
                    </div>
                )}
            </CardContent>

            <AlertDialog open={pending != null} onOpenChange={(open) => !open && setPending(null)}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{t('SPEND_CONFIRM_TITLE')}</AlertDialogTitle>
                        <AlertDialogDescription>
                            {pending && t('LL_ICON_CONFIRM', [
                                getItemName(pending.reward.giveItem.itemType, pending.reward.giveItem.itemId),
                                pending.count,
                                (pending.count * pending.reward.consumeItemCount).toLocaleString(),
                                getItemName(ICON_STAMP.itemType, ICON_STAMP.itemId),
                                stamps.toLocaleString(),
                            ])}
                        </AlertDialogDescription>
                    </AlertDialogHeader>
                    {pending && maxBuy(pending.reward) > 1 && (
                        <div className="flex items-center gap-2 text-sm">
                            {t('SPEND_QUANTITY')}
                            <Input type="number" className="w-24" min={1} max={maxBuy(pending.reward)} value={pending.count}
                                onChange={(e) => setPending({ ...pending, count: Math.max(1, Math.min(maxBuy(pending.reward), Number(e.target.value) || 1)) })} />
                            <span className="text-muted-foreground">/ {maxBuy(pending.reward)}</span>
                        </div>
                    )}
                    <AlertDialogFooter>
                        <AlertDialogCancel>{t('[CommonCancelLabel]')}</AlertDialogCancel>
                        <AlertDialogAction onClick={(e) => { e.preventDefault(); buy(); }} disabled={buying}>
                            {buying && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}{t('SPEND_BUY')}
                        </AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>
        </Card>
    );
}

export function StarsGuidanceShop() {
    const { t } = useTranslation();
    const names = useItemName();
    const { getItemName } = names;
    const items = useAccountStore((s) => s.getCurrentUserSyncData())?.userItemDtoInfo;

    const [info, setInfo] = useState<StarsGuidanceInfoResponse | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [pending, setPending] = useState<StarsGuidanceConsumeItemInfo | null>(null);
    const [busy, setBusy] = useState(false);

    const load = useCallback(async () => {
        setLoading(true);
        try {
            setInfo(await rpcClient.call<StarsGuidanceInfoResponse>('starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo', {}));
            setError(null);
        } catch (e) {
            setError(parseOrtegaError(e).message);
        } finally {
            setLoading(false);
        }
    }, []);
    useEffect(() => { load(); }, [load]);

    const exchange = async () => {
        if (!pending || !info) return;
        setBusy(true);
        try {
            await rpcClient.call('starsGuidanceTradeShop/exchangeItem', {
                starsGuidanceTradeShopId: info.starsGuidanceTradeShopId,
                consumeItem: { itemType: pending.consumeItemType, itemId: pending.consumeItemId, itemCount: pending.consumeItemCount },
            });
            toast({ title: t('SPEND_DONE') });
            setPending(null);
            load();
        } catch (e) {
            toast({ title: t('SPEND_FAILED'), description: parseOrtegaError(e).message, variant: 'destructive' });
        } finally {
            setBusy(false);
        }
    };

    const limitReached = info ? info.todayTradeCount >= info.limitTradeCount : true;

    return (
        <Card>
            <CardHeader>
                <div className="flex items-center justify-between gap-2">
                    <div>
                        <CardTitle>{t('SG_SHOP_TITLE')}</CardTitle>
                        {info && (
                            <CardDescription>
                                {t('SG_TODAY', [info.todayTradeCount, info.limitTradeCount])}
                                {' · '}{t('SG_ENDS', [new Date(info.endLocalTimestamp).toLocaleString()])}
                            </CardDescription>
                        )}
                    </div>
                    <Button variant="outline" size="sm" onClick={load} disabled={loading}>
                        <RefreshCcw className={`h-4 w-4 ${loading ? 'animate-spin' : ''}`} />
                    </Button>
                </div>
            </CardHeader>
            <CardContent>
                {error ? (
                    <p className="text-sm text-muted-foreground">{error}</p>
                ) : !info ? (
                    <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />
                ) : (
                    <div className="space-y-2">
                        {(info.starsGuidanceConsumeItemInfoList ?? []).map((c, i) => {
                            const owned = getUserItemCount(items, c.consumeItemType, c.consumeItemId);
                            return (
                                <div key={i} className="flex flex-wrap items-center gap-3 rounded-lg border bg-card p-3 text-sm">
                                    <ItemChip itemType={c.consumeItemType} itemId={c.consumeItemId} count={c.consumeItemCount} names={names} />
                                    <span className="text-muted-foreground">→</span>
                                    <ItemChip itemType={info.giveItemType} itemId={info.giveItemId} count={c.giveItemCount} names={names} />
                                    <span className="text-muted-foreground">{t('SPEND_OWNED', [owned.toLocaleString()])}</span>
                                    <Button size="sm" className="ml-auto" disabled={limitReached || owned < c.consumeItemCount}
                                        onClick={() => setPending(c)}>
                                        {limitReached ? t('SPEND_SOLD_OUT') : owned < c.consumeItemCount ? t('SPEND_NOT_ENOUGH') : t('SG_EXCHANGE')}
                                    </Button>
                                </div>
                            );
                        })}
                    </div>
                )}
            </CardContent>

            <AlertDialog open={pending != null} onOpenChange={(open) => !open && setPending(null)}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{t('SPEND_CONFIRM_TITLE')}</AlertDialogTitle>
                        <AlertDialogDescription>
                            {pending && info && t('SG_CONFIRM', [
                                pending.consumeItemCount.toLocaleString(),
                                getItemName(pending.consumeItemType, pending.consumeItemId),
                                pending.giveItemCount.toLocaleString(),
                                getItemName(info.giveItemType, info.giveItemId),
                            ])}
                        </AlertDialogDescription>
                    </AlertDialogHeader>
                    <AlertDialogFooter>
                        <AlertDialogCancel>{t('[CommonCancelLabel]')}</AlertDialogCancel>
                        <AlertDialogAction onClick={(e) => { e.preventDefault(); exchange(); }} disabled={busy}>
                            {busy && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}{t('SG_EXCHANGE')}
                        </AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>
        </Card>
    );
}
