import { useCallback, useEffect, useState } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Loader2, RefreshCw, Swords, Shield } from 'lucide-react';
import { rpcClient } from '@/api/rpc-client';
import { useTranslation } from '@/hooks/useTranslation';
import { toast } from '@/hooks/use-toast';
import { parseOrtegaError } from '@/lib/errorHandler';
import { BattleLogModal } from '@/components/battle-log';
import type { BattleSimulationResult } from '@/api/generated/battleSimulationResult';
import type { PlayerInfo } from '@/api/generated/playerInfo';

// Hand-written: battle/getPvpBattleLogs, getLegendLeagueLogs and their detail/top-ranker calls aren't generated yet.
interface BattleLogInfo {
    attackBattlePower: number;
    defenseBattlePower: number;
    battleEndInfo: { winPlayerIdSet: number[] | null };
    battleTime: number;
    battleToken: string;
    rivalPlayerInfo: PlayerInfo;
    oldRank?: number;
    newRank?: number;
    getPoint?: number;
}
interface TopRanker {
    currentRank: number;
    currentPoint?: number;
    playerInfo: PlayerInfo;
}

const API = {
    pvp: { logs: 'battle/getPvpBattleLogs', attack: 'attackPvpBattleLogInfoList', defense: 'defensePvpBattleLogInfoList', detail: 'battle/getPvpBattleResultDetail', top: 'battle/getPvpTopRanker' },
    legend: { logs: 'battle/getLegendLeagueLogs', attack: 'attackLegendLeagueLogInfoList', defense: 'defenseLegendLeagueLogInfoList', detail: 'battle/getLegendLeagueResultDetail', top: 'battle/getLegendLeagueTopRanker' },
} as const;

type Row = BattleLogInfo & { isAttack: boolean };

/** Read-only recent PvP / Legend League fights, with the game's own result detail on click. */
export function PvpBattleLogs({ kind, playerId }: { kind: 'pvp' | 'legend'; playerId?: number }) {
    const { t } = useTranslation();
    const api = API[kind];
    const [rows, setRows] = useState<Row[] | null>(null);
    const [top, setTop] = useState<TopRanker[] | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [opening, setOpening] = useState<string | null>(null);
    const [detail, setDetail] = useState<BattleSimulationResult | null>(null);

    const load = useCallback(async () => {
        setLoading(true);
        try {
            const res = await rpcClient.call<Record<string, BattleLogInfo[] | null>>(api.logs, {});
            const all = [
                ...(res[api.attack] ?? []).map((l) => ({ ...l, isAttack: true })),
                ...(res[api.defense] ?? []).map((l) => ({ ...l, isAttack: false })),
            ].sort((a, b) => b.battleTime - a.battleTime);
            setRows(all);
            setError(null);
        } catch (e) {
            setError(parseOrtegaError(e).message);
        } finally {
            setLoading(false);
        }
        try {
            const res = await rpcClient.call<{ topRankerList: TopRanker[] | null }>(api.top, {});
            setTop(res.topRankerList ?? []);
        } catch {
            setTop(null); // top rankers are a bonus; the logs error already covers "not unlocked"
        }
    }, [api]);
    useEffect(() => { load(); }, [load]);

    const open = async (token: string) => {
        setOpening(token);
        try {
            const res = await rpcClient.call<{ battleResult: { simulationResult: BattleSimulationResult } }>(api.detail, { battleToken: token });
            setDetail(res.battleResult?.simulationResult ?? null);
        } catch (e) {
            toast({ title: t('PVP_LOG_DETAIL_FAILED'), description: parseOrtegaError(e).message, variant: 'destructive' });
        } finally {
            setOpening(null);
        }
    };

    // The winner set holds player ids; when we don't know ours, the rival's absence means we won.
    const isWin = (l: Row) => {
        const winners = l.battleEndInfo?.winPlayerIdSet ?? [];
        return playerId ? winners.includes(playerId) : !winners.includes(l.rivalPlayerInfo?.playerId);
    };

    return (
        <div className="space-y-4">
            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0">
                    <CardTitle className="text-base">{t('PVP_HISTORY')}</CardTitle>
                    <Button variant="outline" size="sm" onClick={load} disabled={loading}>
                        <RefreshCw className={`h-4 w-4 ${loading ? 'animate-spin' : ''}`} />
                    </Button>
                </CardHeader>
                <CardContent>
                    {error ? (
                        <p className="text-sm text-muted-foreground">{error}</p>
                    ) : !rows ? (
                        <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />
                    ) : rows.length === 0 ? (
                        <p className="py-8 text-center text-muted-foreground">{t('PVP_NO_HISTORY')}</p>
                    ) : (
                        <div className="space-y-2">
                            {rows.map((l) => {
                                const win = isWin(l);
                                const rank = kind === 'pvp' && l.oldRank !== l.newRank ? `${l.oldRank} → ${l.newRank}` : null;
                                const points = kind === 'legend' && l.getPoint ? `${l.getPoint > 0 ? '+' : ''}${l.getPoint}` : null;
                                return (
                                    <button key={l.battleToken} type="button" onClick={() => open(l.battleToken)} disabled={opening != null}
                                        className="flex w-full flex-wrap items-center gap-3 rounded-lg border bg-card p-3 text-left text-sm hover:bg-muted/50">
                                        <Badge className={win ? 'bg-green-600 hover:bg-green-600' : 'bg-red-600 hover:bg-red-600'}>
                                            {win ? t('PVP_LOG_WIN') : t('PVP_LOG_LOSE')}
                                        </Badge>
                                        <span className="flex items-center gap-1 text-muted-foreground" title={l.isAttack ? t('PVP_LOG_ATTACK') : t('PVP_LOG_DEFENSE')}>
                                            {l.isAttack ? <Swords className="h-4 w-4" /> : <Shield className="h-4 w-4" />}
                                        </span>
                                        <span className="min-w-0 flex-1 truncate font-medium">{l.rivalPlayerInfo?.playerName}</span>
                                        <span className="tabular-nums text-muted-foreground">
                                            {t('PVP_LOG_POWER', [l.attackBattlePower.toLocaleString(), l.defenseBattlePower.toLocaleString()])}
                                        </span>
                                        {rank && <span className="tabular-nums">{rank}</span>}
                                        {points && <span className="tabular-nums">{points}</span>}
                                        <span className="text-xs text-muted-foreground">{new Date(l.battleTime).toLocaleString()}</span>
                                        {opening === l.battleToken && <Loader2 className="h-4 w-4 animate-spin" />}
                                    </button>
                                );
                            })}
                        </div>
                    )}
                </CardContent>
            </Card>

            {top && top.length > 0 && (
                <Card>
                    <CardHeader><CardTitle className="text-base">{t('PVP_LOG_TOP')}</CardTitle></CardHeader>
                    <CardContent className="space-y-1 text-sm">
                        {top.map((r) => (
                            <div key={r.playerInfo.playerId} className="flex items-center gap-3">
                                <span className="w-8 tabular-nums text-muted-foreground">#{r.currentRank}</span>
                                <span className="flex-1 truncate">{r.playerInfo.playerName}</span>
                                {r.currentPoint != null && <span className="tabular-nums">{r.currentPoint.toLocaleString()}</span>}
                                <span className="tabular-nums text-muted-foreground">{r.playerInfo.battlePower.toLocaleString()}</span>
                            </div>
                        ))}
                    </CardContent>
                </Card>
            )}

            <BattleLogModal isOpen={detail != null} onClose={() => setDetail(null)} battleData={detail} />
        </div>
    );
}
