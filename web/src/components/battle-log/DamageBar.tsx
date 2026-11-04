import { cn } from '@/lib/utils';

interface DamageBarProps {
    value: number;
    maxValue: number;
    label: string;
    color?: 'red' | 'green' | 'blue';
    showValue?: boolean;
}

export function DamageBar({
    value,
    maxValue,
    label,
    color = 'red',
    showValue = true
}: DamageBarProps) {
    const percentage = maxValue > 0 ? (value / maxValue) * 100 : 0;

    const colorClasses = {
        red: 'bg-red-500',
        green: 'bg-green-500',
        blue: 'bg-blue-500'
    };

    const bgClasses = {
        red: 'bg-red-100 dark:bg-red-900/30',
        green: 'bg-green-100 dark:bg-green-900/30',
        blue: 'bg-blue-100 dark:bg-blue-900/30'
    };

    return (
        <div className="flex items-center gap-2 text-xs">
            <span className="text-muted-foreground w-10 shrink-0">{label}</span>
            <div className="flex-1 min-w-0">
                <div className={cn("h-2 rounded-full overflow-hidden", bgClasses[color])}>
                    <div
                        className={cn("h-full rounded-full transition-all duration-300", colorClasses[color])}
                        style={{ width: `${Math.min(percentage, 100)}%` }}
                    />
                </div>
            </div>
            {showValue && (
                <span className="text-muted-foreground w-16 text-right shrink-0">
                    {value.toLocaleString()}
                </span>
            )}
        </div>
    );
}
