import { Input } from '@/components/ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { useTranslation } from '@/hooks/useTranslation';

// Quartz cron 预设; 不在预设里的就是自定义
const PRESETS: { cron: string; labelKey: string }[] = [
    { cron: '0 30 * ? * *', labelKey: 'AP_EVERY_HOUR' },
    { cron: '0 30 0/2 ? * *', labelKey: 'AP_EVERY_2H' },
    { cron: '0 30 0/4 ? * *', labelKey: 'AP_EVERY_4H' },
    { cron: '0 30 0/6 ? * *', labelKey: 'AP_EVERY_6H' },
    { cron: '0 30 0/12 ? * *', labelKey: 'AP_EVERY_12H' },
    { cron: '0 30 5 ? * *', labelKey: 'AP_DAILY_0530' },
];
const CUSTOM = 'custom';

/**
 * 友好的定时选择: 常用频率下拉, 选 "自定义" 才显示 cron 输入框
 */
export function SchedulePicker({ value, onChange }: { value: string; onChange: (cron: string) => void }) {
    const { t } = useTranslation();
    const preset = PRESETS.find((p) => p.cron === value)?.cron ?? CUSTOM;
    return (
        <div className="flex flex-wrap items-center gap-2">
            <Select value={preset} onValueChange={(v) => onChange(v === CUSTOM ? value : v)}>
                <SelectTrigger className="w-48">
                    <SelectValue />
                </SelectTrigger>
                <SelectContent>
                    {PRESETS.map((p) => (
                        <SelectItem key={p.cron} value={p.cron}>{t(p.labelKey)}</SelectItem>
                    ))}
                    <SelectItem value={CUSTOM}>{t('AP_CUSTOM_CRON')}</SelectItem>
                </SelectContent>
            </Select>
            {preset === CUSTOM && (
                <Input
                    value={value}
                    onChange={(e) => onChange(e.target.value)}
                    className="h-9 w-56 font-mono text-sm"
                    aria-label={t('AP_CUSTOM_CRON')}
                />
            )}
        </div>
    );
}
