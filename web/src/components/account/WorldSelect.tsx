import { useState } from 'react';
import { Loader2 } from 'lucide-react';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { useToast } from '@/hooks/use-toast';
import { useTranslation } from '@/hooks/useTranslation';
import { accountApi, type WorldInfo } from '@/api/account-service';

/**
 * 同一个账号在多个世界各有一个角色: 选 bot 用哪一个 (切换后会重新登录, 之后也一直留在这个世界)
 */
export function WorldSelect({ userId, currentWorldId, onSwitched }: { userId: number; currentWorldId?: number | null; onSwitched: () => void }) {
    const { t } = useTranslation();
    const { toast } = useToast();
    const [worlds, setWorlds] = useState<WorldInfo[] | null>(null);
    const [busy, setBusy] = useState(false);

    // 打开下拉时才去拉列表 (要请求一次登录服务器)
    const load = async (open: boolean) => {
        if (!open || worlds) return;
        try {
            setWorlds(await accountApi.getWorlds(userId));
        } catch (e) {
            toast({ title: t('WORLD_LOAD_FAILED'), description: String(e), variant: 'destructive' });
        }
    };

    const change = async (value: string) => {
        const worldId = Number(value);
        if (worldId === currentWorldId) return;
        setBusy(true);
        try {
            await accountApi.switchWorld(userId, worldId);
            toast({ title: t('WORLD_SWITCHED', [worldId]) });
            setWorlds(null);
            onSwitched();
        } catch (e) {
            toast({ title: t('WORLD_SWITCH_FAILED'), description: String(e), variant: 'destructive' });
        } finally {
            setBusy(false);
        }
    };

    const label = (w: WorldInfo) => `${t('[TitleWarningListWorld]')} ${w.worldId} · ${w.name} · Rank ${w.playerRank}`;

    return (
        <Select value={currentWorldId ? String(currentWorldId) : undefined} onValueChange={change} onOpenChange={load} disabled={busy}>
            <SelectTrigger className="h-8 w-auto min-w-40 gap-2 text-xs" aria-label={t('WORLD_SELECT')}>
                {busy && <Loader2 className="h-3 w-3 animate-spin" />}
                <SelectValue placeholder={t('WORLD_SELECT')}>
                    {currentWorldId ? `${t('[TitleWarningListWorld]')} ${currentWorldId}` : undefined}
                </SelectValue>
            </SelectTrigger>
            <SelectContent>
                {worlds === null ? (
                    <div className="flex justify-center p-3"><Loader2 className="h-4 w-4 animate-spin text-muted-foreground" /></div>
                ) : (
                    worlds.map((w) => <SelectItem key={w.worldId} value={String(w.worldId)}>{label(w)}</SelectItem>)
                )}
            </SelectContent>
        </Select>
    );
}
