import { useCallback, useEffect, useState } from 'react';
import dayjs from 'dayjs';
import { Pause, Play } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator, DropdownMenuTrigger } from '@/components/ui/dropdown-menu';
import { useAccountStore } from '@/store/accountStore';
import { useTranslation } from '@/hooks/useTranslation';
import { jobsApi, type PauseState } from '@/api/jobs-service';

/**
 * 暂停 bot: 停掉所有定时任务, 也就不会重新登录把电脑上的游戏挤下线
 */
export function PauseButton() {
    const { t } = useTranslation();
    const userId = useAccountStore((s) => s.currentAccountId);
    const [state, setState] = useState<PauseState | null>(null);

    const load = useCallback(async () => {
        if (!userId) return;
        try {
            setState(await jobsApi.getPause(userId));
        } catch {
            setState(null);
        }
    }, [userId]);

    useEffect(() => {
        const first = setTimeout(load, 0);
        const id = setInterval(load, 60000); // 到点自动恢复后刷新显示
        return () => { clearTimeout(first); clearInterval(id); };
    }, [load]);

    if (!userId || !state) return null;

    const pause = async (hours: number | null) => {
        await jobsApi.pause(userId, hours);
        await load();
    };
    const resume = async () => {
        await jobsApi.resume(userId);
        await load();
    };

    if (state.paused) {
        return (
            <Button variant="outline" size="sm" onClick={resume} className="gap-1.5 border-amber-500/50 text-amber-600 dark:text-amber-400" title={t('PAUSE_RESUME')}>
                <Play className="h-4 w-4" />
                {state.until ? t('PAUSE_PAUSED_UNTIL', [dayjs(state.until).format('HH:mm')]) : t('PAUSE_PAUSED')}
            </Button>
        );
    }

    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="ghost" size="sm" className="gap-1.5 text-muted-foreground" title={t('PAUSE_HINT')}>
                    <Pause className="h-4 w-4" />
                    <span className="text-sm">{t('PAUSE_BUTTON')}</span>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-64">
                <DropdownMenuLabel className="text-xs font-normal text-muted-foreground">{t('PAUSE_HINT')}</DropdownMenuLabel>
                <DropdownMenuSeparator />
                {[1, 2, 4].map((h) => (
                    <DropdownMenuItem key={h} onClick={() => pause(h)}>{t('PAUSE_FOR_HOURS', [h])}</DropdownMenuItem>
                ))}
                <DropdownMenuItem onClick={() => pause(null)}>{t('PAUSE_UNTIL_RESUMED')}</DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    );
}
