import { useState } from 'react';
import dayjs from 'dayjs';
import { useTranslation } from '@/hooks/useTranslation';
import type { ProgressSnapshot } from '@/api/autopilot-service';

type Point = { time: number; value: number; label: string };

const W = 300;
const H = 80;
const PAD = 4;

/**
 * 进度: 主线 / 等级链接 / 无穷塔 各画一张小图 (各自的刻度), 阶梯线, 悬浮看具体值
 * ponytail: 手写 SVG, 要缩放再上图表库
 */
export function ProgressChart({ snapshots }: { snapshots: ProgressSnapshot[] }) {
    const { t } = useTranslation();
    if (snapshots.length < 2) {
        return <p className="py-8 text-center text-sm text-muted-foreground">{t('AP_NO_SNAPSHOTS')}</p>;
    }

    const at = (s: ProgressSnapshot) => dayjs(s.time).valueOf();
    const quest = snapshots.map((s) => ({ time: at(s), value: s.questId, label: s.questName }));
    const link = snapshots.map((s) => ({ time: at(s), value: s.linkLevel, label: String(s.linkLevel) }));
    // 早期的快照没记塔, 没有就跳过, 别从 0 画起
    const tower = snapshots.filter((s) => s.towers?.Infinite).map((s) => ({ time: at(s), value: s.towers.Infinite, label: String(s.towers.Infinite) }));
    const range = { from: at(snapshots[0]), to: at(snapshots[snapshots.length - 1]) };

    return (
        <div className="divide-y">
            <Mini title={t('AP_SERIES_QUEST')} points={quest} range={range} />
            <Mini title={t('AP_SERIES_LINK')} points={link} range={range} />
            <Mini title={t('AP_SERIES_TOWER')} points={tower} range={range} />
        </div>
    );
}

function Mini({ title, points, range }: { title: string; points: Point[]; range: { from: number; to: number } }) {
    const [hover, setHover] = useState<number | null>(null);
    if (points.length === 0) return null;

    const first = points[0];
    const last = points[points.length - 1];
    const min = Math.min(...points.map((p) => p.value));
    const max = Math.max(...points.map((p) => p.value));
    const x = (time: number) => PAD + ((time - range.from) / Math.max(1, range.to - range.from)) * (W - PAD * 2);
    const y = (v: number) => H - PAD - ((v - min) / Math.max(1, max - min)) * (H - PAD * 2);
    // 阶梯线: 进度是一下一下跳的, 不是平滑涨的
    const d = points.map((p, i) => (i === 0 ? `M${x(p.time)},${y(p.value)}` : `H${x(p.time)} V${y(p.value)}`)).join(' ') + ` H${x(range.to)}`;
    const shown = hover != null ? points[hover] : last;

    const onMove = (e: React.MouseEvent<SVGSVGElement>) => {
        const box = e.currentTarget.getBoundingClientRect();
        const time = range.from + ((e.clientX - box.left) / box.width) * (range.to - range.from);
        // 光标位置之前的最后一个点 = 那时候的值
        let i = 0;
        while (i + 1 < points.length && points[i + 1].time <= time) i++;
        setHover(i);
    };

    return (
        <div className="flex items-center gap-4 py-3">
            <div className="w-32 shrink-0">
                <div className="text-sm text-muted-foreground">{title}</div>
                <div className="text-2xl font-semibold tabular-nums">{shown.label}</div>
                <div className="text-xs text-muted-foreground tabular-nums">
                    {hover != null ? dayjs(shown.time).format('MM-DD HH:mm') : `${first.label} → ${last.label}`}
                </div>
            </div>
            <svg viewBox={`0 0 ${W} ${H}`} className="h-16 min-w-0 flex-1 text-primary" preserveAspectRatio="none" role="img"
                 aria-label={`${title}: ${first.label} → ${last.label}`} onMouseMove={onMove} onMouseLeave={() => setHover(null)}>
                <line x1={PAD} x2={W - PAD} y1={H - PAD} y2={H - PAD} className="stroke-border" strokeWidth={1} vectorEffect="non-scaling-stroke" />
                <path d={d} fill="none" stroke="currentColor" strokeWidth={2} vectorEffect="non-scaling-stroke" />
                {hover != null && (
                    <line x1={x(shown.time)} x2={x(shown.time)} y1={0} y2={H} className="stroke-muted-foreground" strokeWidth={1} strokeDasharray="3 3" vectorEffect="non-scaling-stroke" />
                )}
            </svg>
        </div>
    );
}
