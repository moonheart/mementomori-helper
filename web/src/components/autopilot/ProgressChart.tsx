import dayjs from 'dayjs';
import { useTranslation } from '@/hooks/useTranslation';
import type { ProgressSnapshot } from '@/api/autopilot-service';

type Series = { key: string; label: string; color: string; value: (s: ProgressSnapshot) => number };

const W = 600;
const H = 160;
const PAD = 8;

/**
 * 进度曲线: 主线关卡 / 等级链接 / 无穷塔, 各自归一化画在一张图上
 * ponytail: 手写 SVG, 要缩放/悬浮提示再上图表库
 */
export function ProgressChart({ snapshots }: { snapshots: ProgressSnapshot[] }) {
    const { t } = useTranslation();
    if (snapshots.length < 2) {
        return <p className="py-8 text-center text-sm text-muted-foreground">{t('AP_NO_SNAPSHOTS')}</p>;
    }

    const series: Series[] = [
        { key: 'quest', label: t('AP_SERIES_QUEST'), color: '#8b5cf6', value: (s) => s.questId },
        { key: 'link', label: t('AP_SERIES_LINK'), color: '#10b981', value: (s) => s.linkLevel },
        { key: 'tower', label: t('AP_SERIES_TOWER'), color: '#0ea5e9', value: (s) => s.towers?.Infinite ?? 0 },
    ];
    const t0 = dayjs(snapshots[0].time).valueOf();
    const t1 = dayjs(snapshots[snapshots.length - 1].time).valueOf();
    const x = (s: ProgressSnapshot) => PAD + ((dayjs(s.time).valueOf() - t0) / Math.max(1, t1 - t0)) * (W - PAD * 2);

    const first = snapshots[0];
    const last = snapshots[snapshots.length - 1];

    return (
        <div className="space-y-3">
            <svg viewBox={`0 0 ${W} ${H}`} className="h-40 w-full" preserveAspectRatio="none" role="img" aria-label={t('AP_PROGRESS')}>
                {series.map((se) => {
                    const values = snapshots.map(se.value);
                    const min = Math.min(...values);
                    const max = Math.max(...values);
                    const y = (v: number) => H - PAD - ((v - min) / Math.max(1, max - min)) * (H - PAD * 2);
                    const d = snapshots.map((s, i) => `${i ? 'L' : 'M'}${x(s).toFixed(1)},${y(values[i]).toFixed(1)}`).join(' ');
                    return <path key={se.key} d={d} fill="none" stroke={se.color} strokeWidth={2} vectorEffect="non-scaling-stroke" />;
                })}
            </svg>
            <div className="flex flex-wrap gap-x-6 gap-y-1 text-sm">
                {series.map((se) => (
                    <span key={se.key} className="flex items-center gap-2">
                        <span className="h-2 w-2 rounded-full" style={{ background: se.color }} />
                        <span className="text-muted-foreground">{se.label}</span>
                        <span className="font-medium tabular-nums">
                            {se.key === 'quest' ? `${first.questName} → ${last.questName}` : `${se.value(first)} → ${se.value(last)}`}
                        </span>
                    </span>
                ))}
            </div>
        </div>
    );
}
