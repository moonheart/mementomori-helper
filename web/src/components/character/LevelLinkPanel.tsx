import { useEffect, useState } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { AlertDialog, AlertDialogAction, AlertDialogCancel, AlertDialogContent, AlertDialogDescription, AlertDialogFooter, AlertDialogHeader, AlertDialogTitle } from '@/components/ui/alert-dialog';
import { ArrowDownUp, Loader2, X } from 'lucide-react';
import { rpcClient } from '@/api/rpc-client';
import { useAccountStore } from '@/store/accountStore';
import { useTranslation } from '@/hooks/useTranslation';
import { useMasterTable } from '@/hooks/useMasterData';
import { toast } from '@/hooks/use-toast';
import { parseOrtegaError } from '@/lib/errorHandler';
import { getUserItemCount } from '@/lib/itemUtils';
import { CharacterIcon } from '@/components/character/CharacterIcon';
import { toCharacterInfo } from '@/api/autopilot-service';
import { CharacterSortType } from '@/api/generated/characterSortType';
import { ItemType } from '@/api/generated/itemType';
import type { CharacterMB } from '@/api/generated/characterMB';

// OrtegaConst.LevelLink.ResetCoolTimeCurrency in the client (diamonds per slot reset).
const RESET_COOLTIME_DIAMONDS = 100;
const SORT_TYPES = [CharacterSortType.BattlePower, CharacterSortType.Rarity, CharacterSortType.CharacterLevel, CharacterSortType.CharacterAttribute];

type PendingAction = { kind: 'remove' | 'reset'; cellNo: number; name: string };

export function LevelLinkPanel() {
    const { t } = useTranslation();
    const sync = useAccountStore((s) => s.getCurrentUserSyncData());
    const { data: characterTable } = useMasterTable<CharacterMB>('CharacterTable');
    const [now, setNow] = useState(() => Date.now());
    const [pending, setPending] = useState<PendingAction | null>(null);
    const [busy, setBusy] = useState(false);
    const [sortType, setSortType] = useState(CharacterSortType.BattlePower);
    const [ascending, setAscending] = useState(false);

    useEffect(() => {
        const id = setInterval(() => setNow(Date.now()), 30_000);
        return () => clearInterval(id);
    }, []);

    const info = sync?.userLevelLinkDtoInfo;
    if (!info || info.memberMaxCount < 1) return null;

    const diamonds = getUserItemCount(sync?.userItemDtoInfo, ItemType.CurrencyFree, 1, true);
    const members = [...(sync?.userLevelLinkMemberDtoInfos ?? [])].sort((a, b) => a.cellNo - b.cellNo);
    const characterName = (characterId: number) => {
        const mb = characterTable?.find((c) => c.id === characterId);
        return mb ? t(mb.nameKey) : `#${characterId}`;
    };
    const formatLeft = (ms: number) => {
        const m = Math.ceil(ms / 60_000);
        return `${Math.floor(m / 60)}h ${m % 60}m`;
    };

    const run = async (uri: string, body: object) => {
        setBusy(true);
        try {
            await rpcClient.call(uri, body);
            toast({ title: t('SPEND_DONE') });
            setPending(null);
        } catch (e) {
            toast({ title: t('SPEND_FAILED'), description: parseOrtegaError(e).message, variant: 'destructive' });
        } finally {
            setBusy(false);
        }
    };

    const confirm = () => {
        if (!pending) return;
        run(pending.kind === 'remove' ? 'levelLink/unSetMember' : 'levelLink/resetCoolTime', { levelLinkCellNo: pending.cellNo });
    };

    return (
        <Card>
            <CardHeader>
                <div className="flex flex-wrap items-center justify-between gap-2">
                    <div>
                        <CardTitle>{t('LL_LINK_TITLE')}</CardTitle>
                        <CardDescription>{t('LL_LINK_DESC', [info.partyLevel, members.filter((m) => m.userCharacterGuid).length, info.memberMaxCount])}</CardDescription>
                    </div>
                    <div className="flex items-center gap-2">
                        <Select value={String(sortType)} onValueChange={(v) => setSortType(Number(v))}>
                            <SelectTrigger className="w-40"><SelectValue /></SelectTrigger>
                            <SelectContent>
                                {SORT_TYPES.map((s) => (
                                    <SelectItem key={s} value={String(s)}>{t(`[CharacterSortType${CharacterSortType[s]}]`)}</SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                        <Button variant="outline" size="sm" onClick={() => setAscending(!ascending)}>
                            {ascending ? t('LL_LINK_ASC') : t('LL_LINK_DESC_ORDER')}
                        </Button>
                        <Button variant="outline" size="sm" disabled={busy}
                            onClick={() => run('levelLink/sortMember', { characterSortType: sortType, isAscending: ascending })}>
                            <ArrowDownUp className="mr-1 h-4 w-4" />{t('LL_LINK_SORT')}
                        </Button>
                    </div>
                </div>
            </CardHeader>
            <CardContent>
                <div className="grid gap-2 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5">
                    {members.map((m) => {
                        const char = m.userCharacterGuid ? sync?.userCharacterDtoInfos?.find((c) => c.guid === m.userCharacterGuid) : undefined;
                        const coolLeft = m.unavailableTime - now;
                        return (
                            <div key={m.cellNo} className="flex items-center gap-2 rounded-lg border bg-card p-2 text-sm">
                                <span className="w-6 text-center text-xs text-muted-foreground">{m.cellNo}</span>
                                {char ? (
                                    <>
                                        <CharacterIcon userCharacterInfo={toCharacterInfo(char)} size={40} userSyncData={sync} />
                                        <span className="flex-1 truncate font-medium">{characterName(char.characterId)}</span>
                                        <Button variant="ghost" size="icon" className="h-7 w-7" title={t('LL_LINK_REMOVE')}
                                            onClick={() => setPending({ kind: 'remove', cellNo: m.cellNo, name: characterName(char.characterId) })}>
                                            <X className="h-4 w-4" />
                                        </Button>
                                    </>
                                ) : coolLeft > 0 ? (
                                    <>
                                        <span className="flex-1 text-muted-foreground">{t('LL_LINK_COOLDOWN', [formatLeft(coolLeft)])}</span>
                                        <Button variant="outline" size="sm" onClick={() => setPending({ kind: 'reset', cellNo: m.cellNo, name: '' })}>
                                            💎{RESET_COOLTIME_DIAMONDS}
                                        </Button>
                                    </>
                                ) : (
                                    <span className="flex-1 text-muted-foreground">{t('LL_LINK_EMPTY')}</span>
                                )}
                            </div>
                        );
                    })}
                </div>
            </CardContent>

            <AlertDialog open={pending != null} onOpenChange={(open) => !open && setPending(null)}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{pending?.kind === 'reset' ? t('SPEND_CONFIRM_TITLE') : t('LL_LINK_REMOVE')}</AlertDialogTitle>
                        <AlertDialogDescription>
                            {pending && (pending.kind === 'remove'
                                ? t('LL_LINK_REMOVE_CONFIRM', [pending.name, pending.cellNo])
                                : t('LL_LINK_RESET_CONFIRM', [pending.cellNo, RESET_COOLTIME_DIAMONDS, diamonds.toLocaleString()]))}
                        </AlertDialogDescription>
                    </AlertDialogHeader>
                    <AlertDialogFooter>
                        <AlertDialogCancel>{t('[CommonCancelLabel]')}</AlertDialogCancel>
                        <AlertDialogAction onClick={(e) => { e.preventDefault(); confirm(); }}
                            disabled={busy || (pending?.kind === 'reset' && diamonds < RESET_COOLTIME_DIAMONDS)}>
                            {busy && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                            {pending?.kind === 'reset' ? t('LL_LINK_RESET') : t('LL_LINK_REMOVE')}
                        </AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>
        </Card>
    );
}
