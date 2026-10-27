import dayjs from 'dayjs';
import { Badge } from '@/components/ui/badge';
import { cn } from '@/lib/utils';
import { useTranslation } from '@/hooks/useTranslation';
import type { GrowthEvent, GrowthKind } from '@/api/autopilot-service';

const kindStyle: Record<GrowthKind, string> = {
    Level: 'bg-emerald-500/15 text-emerald-600 dark:text-emerald-400',
    LevelLink: 'bg-emerald-500/15 text-emerald-600 dark:text-emerald-400',
    Equip: 'bg-sky-500/15 text-sky-600 dark:text-sky-400',
    Reinforce: 'bg-sky-500/15 text-sky-600 dark:text-sky-400',
    Quest: 'bg-violet-500/15 text-violet-600 dark:text-violet-400',
    Tower: 'bg-violet-500/15 text-violet-600 dark:text-violet-400',
    RankUp: 'bg-amber-500/15 text-amber-600 dark:text-amber-400',
    Gacha: 'bg-pink-500/15 text-pink-600 dark:text-pink-400',
    Team: 'bg-indigo-500/15 text-indigo-600 dark:text-indigo-400',
    Info: 'bg-muted text-muted-foreground',
};

/**
 * 成长记录列表: bot 每次让账号变强的记录
 */
export function GrowthFeed({ events, className }: { events: GrowthEvent[]; className?: string }) {
    const { t } = useTranslation();
    if (events.length === 0) {
        return <p className="py-8 text-center text-sm text-muted-foreground">{t('AP_NO_EVENTS')}</p>;
    }
    return (
        <ul className={cn('divide-y', className)}>
            {events.map((e) => (
                <li key={e.id} className="flex items-start gap-3 py-2">
                    <span className="w-24 shrink-0 pt-0.5 text-xs tabular-nums text-muted-foreground">
                        {dayjs(e.time).format('MM-DD HH:mm')}
                    </span>
                    <Badge variant="secondary" className={cn('shrink-0 border-0', kindStyle[e.kind])}>
                        {t(`AP_KIND_${e.kind}`)}
                    </Badge>
                    <span className="min-w-0 break-words text-sm">{e.text}</span>
                </li>
            ))}
        </ul>
    );
}
