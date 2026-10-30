import { useEffect, useLayoutEffect, useState } from 'react';
import { Button } from '@/components/ui/button';
import { useTranslation } from '@/hooks/useTranslation';

export type TourStep = { target: string; title: string; body: string };

// 版本号变了就再弹一次 (有新的会花东西/改东西的功能时 +1)
const KEY = 'autopilot-tour-v3';

function seen() {
    try { return localStorage.getItem(KEY) === '1'; } catch { return false; }
}

/**
 * 像游戏新手引导: 压暗页面, 高亮 [data-tour=target] 的元素, 旁边一张卡片 下一步/跳过. 只弹一次
 * ponytail: 手写, 不用引导库; 目标元素不在页面上就跳过那一步
 */
export function Tour({ steps }: { steps: TourStep[] }) {
    const { t } = useTranslation();
    const [i, setI] = useState(() => (seen() ? -1 : 0));
    const [box, setBox] = useState<DOMRect | null>(null);
    const step = i >= 0 ? steps[i] : undefined;

    const close = () => {
        try { localStorage.setItem(KEY, '1'); } catch { /* 隐私模式: 下次再弹 */ }
        setI(-1);
    };
    const next = () => (i + 1 < steps.length ? setI(i + 1) : close());

    useLayoutEffect(() => {
        if (!step) return;
        const el = document.querySelector(`[data-tour="${step.target}"]`);
        if (!el) { next(); return; }
        el.scrollIntoView({ block: 'center', behavior: 'instant' as ScrollBehavior });
        const measure = () => setBox(el.getBoundingClientRect());
        measure();
        window.addEventListener('resize', measure);
        window.addEventListener('scroll', measure, true);
        return () => { window.removeEventListener('resize', measure); window.removeEventListener('scroll', measure, true); };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [step]);

    useEffect(() => {
        if (!step) return;
        const onKey = (e: KeyboardEvent) => { if (e.key === 'Escape') close(); };
        window.addEventListener('keydown', onKey);
        return () => window.removeEventListener('keydown', onKey);
    }, [step]);

    if (!step || !box) return null;
    const pad = 6;
    // 卡片放在高亮框下面, 放不下就放上面
    const below = box.bottom + 200 < window.innerHeight;
    const cardTop = below ? box.bottom + pad + 12 : Math.max(16, box.top - pad - 12 - 180);
    const cardLeft = Math.min(Math.max(16, box.left), window.innerWidth - 16 - Math.min(360, window.innerWidth - 32));

    return (
        <div className="fixed inset-0 z-[100]" role="dialog" aria-modal="true" aria-labelledby="tour-title">
            {/* 大阴影挖出高亮框 = 压暗其余部分 */}
            <div className="pointer-events-none absolute rounded-lg ring-2 ring-primary transition-all duration-200"
                 style={{ top: box.top - pad, left: box.left - pad, width: box.width + pad * 2, height: box.height + pad * 2, boxShadow: '0 0 0 9999px rgb(0 0 0 / 0.6)' }} />
            <div className="absolute w-[min(360px,calc(100vw-32px))] space-y-3 rounded-xl border bg-card p-4 text-card-foreground shadow-xl"
                 style={{ top: cardTop, left: cardLeft }}>
                <div className="text-xs text-muted-foreground tabular-nums">{i + 1} / {steps.length}</div>
                <h3 id="tour-title" className="font-semibold">{step.title}</h3>
                <p className="text-sm text-muted-foreground">{step.body}</p>
                <div className="flex justify-end gap-2">
                    <Button variant="ghost" size="sm" onClick={close}>{t('TOUR_SKIP')}</Button>
                    <Button size="sm" onClick={next} autoFocus>{i + 1 < steps.length ? t('TOUR_NEXT') : t('TOUR_DONE')}</Button>
                </div>
            </div>
        </div>
    );
}
