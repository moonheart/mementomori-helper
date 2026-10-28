import { useCallback, useEffect, useMemo, useState } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Input } from '@/components/ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { AlertDialog, AlertDialogAction, AlertDialogCancel, AlertDialogContent, AlertDialogDescription, AlertDialogFooter, AlertDialogHeader, AlertDialogTitle } from '@/components/ui/alert-dialog';
import { Loader2, RefreshCcw, Search } from 'lucide-react';
import { cn } from '@/lib/utils';
import { useAccountStore } from '@/store/accountStore';
import { useToast } from '@/hooks/use-toast';
import { useTranslation } from '@/hooks/useTranslation';
import { CharacterIcon } from '@/components/character/CharacterIcon';
import { autopilotApi, toCharacterInfo, TIERS, type BannerAdvice, type TamamoStageTeams, type TamamoTeam, type TierRow } from '@/api/autopilot-service';

// dark: 也要写, 否则深色模式下 Select 自带的 dark:bg-input 会盖掉颜色
const tierStyle: Record<string, string> = {
    SS: 'bg-rose-500 dark:bg-rose-500 text-white',
    S: 'bg-amber-500 dark:bg-amber-500 text-white',
    A: 'bg-emerald-500 dark:bg-emerald-500 text-white',
    B: 'bg-sky-500 dark:bg-sky-500 text-white',
    C: 'bg-slate-400 dark:bg-slate-500 text-white',
};
const NONE = 'none';

function TierSelect({ row, onChange }: { row: TierRow; onChange: (tier: string | null) => void }) {
    const { t } = useTranslation();
    return (
        <Select value={row.tier ?? NONE} onValueChange={(v) => onChange(v === NONE ? null : v)}>
            <SelectTrigger className={cn('h-8 w-20 border-0 font-semibold', row.tier ? tierStyle[row.tier] : 'bg-muted')} aria-label={t('TT_TIER')}>
                <SelectValue />
            </SelectTrigger>
            <SelectContent>
                {TIERS.map((tier) => <SelectItem key={tier} value={tier}>{tier}</SelectItem>)}
                <SelectItem value={NONE}>—</SelectItem>
            </SelectContent>
        </Select>
    );
}

export function TeamTiersPage() {
    const { t } = useTranslation();
    const { toast } = useToast();
    const userId = useAccountStore((s) => s.currentAccountId);
    const [rows, setRows] = useState<TierRow[] | null>(null);
    const [banners, setBanners] = useState<BannerAdvice[] | null>(null);
    const [tamamo, setTamamo] = useState<TamamoStageTeams | null | undefined>(undefined);
    const [query, setQuery] = useState('');
    const [pending, setPending] = useState<{ banner: BannerAdvice; buttonId: number; diamonds: boolean } | null>(null);
    const [drawing, setDrawing] = useState(false);

    const load = useCallback(async () => {
        if (!userId) return;
        setRows(await autopilotApi.getTiers(userId));
    }, [userId]);

    // 服务器按关卡缓存, 只有点刷新才强制重新请求 tamamo.dev
    const loadTamamo = useCallback(async (refresh = false) => {
        if (!userId) return;
        setTamamo(undefined);
        try {
            setTamamo(await autopilotApi.getTamamo(userId, refresh));
        } catch {
            setTamamo(null);
        }
    }, [userId]);

    const loadBanners = useCallback(async () => {
        if (!userId) return;
        setBanners(null);
        try {
            setBanners(await autopilotApi.getBanners(userId));
        } catch {
            setBanners([]);
        }
    }, [userId]);

    useEffect(() => { load(); loadBanners(); loadTamamo(); }, [load, loadBanners, loadTamamo]);

    const setTier = async (row: TierRow, tier: string | null) => {
        await autopilotApi.setTier(row.characterId, tier);
        await load(); // 推荐队伍可能变了
    };

    const draw = async () => {
        if (!userId || !pending) return;
        setDrawing(true);
        try {
            await autopilotApi.draw(userId, pending.buttonId, pending.banner.name);
            toast({ title: t('TT_DRAW_DONE'), description: t('TT_DRAW_DONE_DESC') });
            await Promise.all([load(), loadBanners()]);
        } catch (e) {
            toast({ title: t('TT_DRAW_FAILED'), description: String(e), variant: 'destructive' });
        } finally {
            setDrawing(false);
            setPending(null);
        }
    };

    const [applying, setApplying] = useState(false);
    const applyRecommended = async () => {
        if (!userId) return;
        setApplying(true);
        try {
            await autopilotApi.run(userId, 'AutoBuildTeam');
            // 任务是后台跑的, 等一下再刷新
            await new Promise((r) => setTimeout(r, 5000));
            await load();
            toast({ title: t('TT_TEAM_APPLIED') });
        } catch (e) {
            toast({ title: t('TT_TEAM_APPLY_FAILED'), description: String(e), variant: 'destructive' });
        } finally {
            setApplying(false);
        }
    };

    const filtered = useMemo(
        () => (rows ?? []).filter((r) => r.name.toLowerCase().includes(query.trim().toLowerCase())),
        [rows, query]
    );
    const team = (rows ?? []).filter((r) => r.inTeam);
    const recommended = (rows ?? []).filter((r) => r.recommended);
    const sameTeam = recommended.length === team.length && recommended.every((r) => r.inTeam);

    if (!rows) {
        return <div className="flex justify-center p-12"><Loader2 className="h-6 w-6 animate-spin text-muted-foreground" /></div>;
    }

    const TeamList = ({ list }: { list: TierRow[] }) => (
        <ul className="flex flex-wrap gap-2">
            {list.map((r) => (
                <li key={r.characterId} className="flex items-center gap-2 rounded-lg border py-1 pl-1 pr-3 text-sm">
                    <CharacterIcon userCharacterInfo={toCharacterInfo(r)} size={40} />
                    <span className={cn('rounded px-1.5 text-xs font-bold', r.tier ? tierStyle[r.tier] : 'bg-muted')}>{r.tier ?? '?'}</span>
                    <span className="font-medium">{r.name}</span>
                    <span className="tabular-nums text-muted-foreground">Lv{r.level}</span>
                </li>
            ))}
        </ul>
    );

    const TamamoRow = ({ team, label }: { team: TamamoTeam; label: string }) => (
        <div className="space-y-1">
            <p className="text-xs text-muted-foreground">{label} · {t('TM_POWER', [team.minPower.toLocaleString()])}</p>
            <ul className="flex flex-wrap gap-2">
                {team.characters.map((c, i) => (
                    <li key={i} className={cn('flex items-center gap-2 rounded-lg border py-1 pl-1 pr-3 text-sm', !c.owned && 'opacity-50')}
                        title={c.owned ? undefined : t('TM_NOT_OWNED')}>
                        <CharacterIcon userCharacterInfo={toCharacterInfo({ characterId: c.characterId, guid: '', rarityFlags: c.rarityFlags, level: 0 })} size={32} />
                        <span className={cn('rounded px-1.5 text-xs font-bold', c.tier ? tierStyle[c.tier] : 'bg-muted')}>{c.tier ?? '?'}</span>
                        <span className="font-medium">{c.name}</span>
                    </li>
                ))}
            </ul>
        </div>
    );

    return (
        <div className="space-y-6">
            {tamamo !== null && (
                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0">
                        <div className="space-y-1">
                            <CardTitle className="text-base">{t('TM_TITLE', [tamamo?.stage ?? '…'])}</CardTitle>
                            <CardDescription>
                                {t('TM_DESC')} <a href={tamamo?.link ?? 'https://tamamo.dev/ClearPartys'} target="_blank" rel="noreferrer" className="underline">tamamo.dev</a>
                                {tamamo && tamamo.stuck > 0 && <> · {t('TM_STUCK', [tamamo.stuck.toLocaleString()])}</>}
                            </CardDescription>
                        </div>
                        <Button variant="ghost" size="icon" onClick={() => loadTamamo(true)} aria-label={t('AP_REFRESH')}>
                            <RefreshCcw className="h-4 w-4" />
                        </Button>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        {tamamo === undefined ? (
                            <div className="flex justify-center py-6"><Loader2 className="h-5 w-5 animate-spin text-muted-foreground" /></div>
                        ) : tamamo.teams.length === 0 ? (
                            <p className="text-sm text-muted-foreground">{t('TM_NONE')}</p>
                        ) : (
                            <>
                                {tamamo.dataStage !== tamamo.stage && (
                                    <p className="text-sm text-amber-600 dark:text-amber-400">{t('TM_CLOSEST', [tamamo.stage, tamamo.dataStage ?? ''])}</p>
                                )}
                                {tamamo.teams.map((team, i) => <TamamoRow key={i} team={team} label={t('TM_CLEARS', [team.clears])} />)}
                                {tamamo.lowest && <TamamoRow team={tamamo.lowest} label={t('TM_LOWEST')} />}
                            </>
                        )}
                    </CardContent>
                </Card>
            )}

            <div className="grid gap-6 lg:grid-cols-2">
                <Card>
                    <CardHeader>
                        <CardTitle className="text-base">{t('TT_TEAMS')}</CardTitle>
                        <CardDescription>{t('TT_TEAMS_DESC')}</CardDescription>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        <div className="space-y-2">
                            <p className="text-xs font-semibold uppercase tracking-wide text-muted-foreground">{t('TT_CURRENT_TEAM')}</p>
                            <TeamList list={team} />
                        </div>
                        <div className="space-y-2">
                            <div className="flex items-center justify-between gap-2">
                                <p className="text-xs font-semibold uppercase tracking-wide text-muted-foreground">{t('TT_RECOMMENDED_TEAM')}</p>
                                <Button size="sm" variant="outline" onClick={applyRecommended} disabled={applying || sameTeam || recommended.length === 0}>
                                    {applying && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                                    {t('TT_USE_TEAM')}
                                </Button>
                            </div>
                            <TeamList list={recommended} />
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0">
                        <div className="space-y-1">
                            <CardTitle className="text-base">{t('TT_BANNERS')}</CardTitle>
                            <CardDescription>{t('TT_BANNERS_DESC')}</CardDescription>
                            {banners?.[0] && <p className="text-xs tabular-nums">💎 {t('TT_FREE_DIAMONDS', [banners[0].freeDiamonds.toLocaleString()])}</p>}
                        </div>
                        <Button variant="ghost" size="icon" onClick={loadBanners} aria-label={t('AP_REFRESH')}>
                            <RefreshCcw className="h-4 w-4" />
                        </Button>
                    </CardHeader>
                    <CardContent>
                        {banners === null ? (
                            <div className="flex justify-center py-6"><Loader2 className="h-5 w-5 animate-spin text-muted-foreground" /></div>
                        ) : banners.length === 0 ? (
                            <p className="py-6 text-center text-sm text-muted-foreground">{t('TT_NO_BANNERS')}</p>
                        ) : (
                            <ul className="divide-y">
                                {banners.map((b) => (
                                    <li key={b.gachaCaseId} className="flex flex-wrap items-center gap-3 py-2">
                                        <div className="min-w-0 flex-1">
                                            <p className="truncate text-sm font-medium">{b.name}</p>
                                            <p className={cn('text-xs', b.worth ? 'text-emerald-600 dark:text-emerald-400' : 'text-muted-foreground')}>{b.verdict}</p>
                                            {b.ticketName && (
                                                <p className="text-xs tabular-nums text-muted-foreground">🎟 {b.ticketName}: {b.ticketsOwned.toLocaleString()} ({t('TT_TICKETS_PER_PULL', [b.ticketCost.toLocaleString()])})</p>
                                            )}
                                        </div>
                                        {b.ticketButtonId != null && (
                                            <Button size="sm" variant="outline" onClick={() => setPending({ banner: b, buttonId: b.ticketButtonId!, diamonds: false })}>
                                                {t('TT_PULL_TICKET')}
                                            </Button>
                                        )}
                                        {b.diamondButtonId != null && (
                                            <Button size="sm" variant={b.worth ? 'default' : 'ghost'} onClick={() => setPending({ banner: b, buttonId: b.diamondButtonId!, diamonds: true })}>
                                                {t('TT_PULL_DIAMONDS', [b.diamondCost.toLocaleString()])}
                                            </Button>
                                        )}
                                    </li>
                                ))}
                            </ul>
                        )}
                    </CardContent>
                </Card>
            </div>

            <Card>
                <CardHeader className="flex flex-row flex-wrap items-center justify-between gap-4 space-y-0">
                    <div className="space-y-1">
                        <CardTitle className="text-base">{t('TT_TIER_LIST')}</CardTitle>
                        <CardDescription>{t('TT_TIER_LIST_DESC')}</CardDescription>
                    </div>
                    <div className="relative w-full sm:w-64">
                        <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
                        <Input value={query} onChange={(e) => setQuery(e.target.value)} placeholder={t('TT_SEARCH')} className="pl-8" aria-label={t('TT_SEARCH')} />
                    </div>
                </CardHeader>
                <CardContent className="overflow-x-auto">
                    <table className="w-full text-sm">
                        <thead>
                            <tr className="border-b text-left text-xs text-muted-foreground">
                                <th className="py-2 pr-3 font-medium">{t('TT_TIER')}</th>
                                <th className="py-2 pr-3 font-medium">{t('TT_CHARACTER')}</th>
                                <th className="py-2 pr-3 font-medium">{t('TT_ROLE')}</th>
                                <th className="py-2 pr-3 font-medium">{t('TT_LEVEL')}</th>
                                <th className="py-2 font-medium">{t('TT_NOTES')}</th>
                            </tr>
                        </thead>
                        <tbody>
                            {filtered.map((r) => (
                                <tr key={r.characterId} className="border-b last:border-0">
                                    <td className="py-1.5 pr-3"><TierSelect row={r} onChange={(tier) => setTier(r, tier)} /></td>
                                    <td className="py-1.5 pr-3">
                                        <span className="inline-flex items-center gap-2 align-middle">
                                            <CharacterIcon userCharacterInfo={toCharacterInfo(r)} size={36} />
                                            <span className="font-medium">{r.name}</span>
                                        </span>
                                        <span className="ml-2 text-xs text-muted-foreground">{r.rarity} · {r.element}</span>
                                        {r.inTeam && <Badge variant="secondary" className="ml-2">{t('TT_IN_TEAM')}</Badge>}
                                        {r.overridden && <Badge variant="outline" className="ml-2">{t('TT_EDITED')}</Badge>}
                                    </td>
                                    <td className="py-1.5 pr-3 capitalize text-muted-foreground">{r.role ?? '—'}</td>
                                    <td className="py-1.5 pr-3 tabular-nums">{r.level}{r.linked && <span className="ml-1 text-xs text-muted-foreground" title={t('TT_LINKED_HINT')}>🔗 {t('TT_LINKED')}</span>}</td>
                                    <td className="py-1.5 text-xs text-muted-foreground">{r.notes ?? ''}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </CardContent>
            </Card>

            <AlertDialog open={pending != null} onOpenChange={(open) => !open && setPending(null)}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{t('TT_CONFIRM_PULL')}</AlertDialogTitle>
                        <AlertDialogDescription>
                            {pending && (pending.diamonds
                                ? t('TT_CONFIRM_PULL_DIAMONDS', [pending.banner.name, pending.banner.diamondCost.toLocaleString(), pending.banner.freeDiamonds.toLocaleString()])
                                : t('TT_CONFIRM_PULL_TICKET', [pending.banner.name, pending.banner.ticketName ?? '', pending.banner.ticketsOwned.toLocaleString()]))}
                        </AlertDialogDescription>
                    </AlertDialogHeader>
                    <AlertDialogFooter>
                        <AlertDialogCancel>{t('[CommonCancelLabel]')}</AlertDialogCancel>
                        <AlertDialogAction onClick={draw} disabled={drawing}>
                            {drawing && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}{t('TT_PULL')}
                        </AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>
        </div>
    );
}
