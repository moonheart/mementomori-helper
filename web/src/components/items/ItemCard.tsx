import { Card, CardContent } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Package } from 'lucide-react';
import { useLocalizationStore } from '@/store/localization-store';

export interface ItemCardProps {
    name: string;
    rarity: string;
    count: number;
    iconUrl?: string;
    canUse?: boolean;
    onUse?: () => void;
}

export function ItemCard({ name, rarity, count, iconUrl, canUse, onUse }: ItemCardProps) {
    const t = useLocalizationStore(state => state.t);
    const getRarityColor = (rarityStr: string) => {
        const upper = rarityStr.toUpperCase();
        if (upper.includes('LR')) return 'bg-yellow-100 text-yellow-800 border-yellow-300';
        if (upper.includes('UR')) return 'bg-purple-100 text-purple-800 border-purple-300';
        if (upper.includes('SSR')) return 'bg-red-100 text-red-800 border-red-300';
        if (upper.includes('SR')) return 'bg-orange-100 text-orange-800 border-orange-300';
        if (upper.includes('R')) return 'bg-blue-100 text-blue-800 border-blue-300';
        return 'bg-gray-100 text-gray-800 border-gray-300';
    };

    return (
        <Card className="hover:shadow-md transition-shadow">
            <CardContent className="p-4">
                <div className="flex flex-col items-center gap-2">
                    {/* 图标 */}
                    <div className="w-16 h-16 bg-gradient-to-br from-gray-100 to-gray-200 rounded-lg flex items-center justify-center overflow-hidden">
                        {iconUrl ? (
                            <img
                                src={iconUrl}
                                alt={name}
                                className="w-full h-full object-contain"
                                loading="lazy"
                                decoding="async"
                                onError={(e) => {
                                    (e.target as HTMLImageElement).style.display = 'none';
                                }}
                            />
                        ) : (
                            <Package className="w-8 h-8 text-gray-600" />
                        )}
                    </div>

                    {/* 名称 */}
                    <h3 className="font-medium text-sm text-center line-clamp-2 min-h-[2.5rem]">
                        {name}
                    </h3>

                    {/* 稀有度 */}
                    {rarity && (
                        <Badge
                            variant="outline"
                            className={`text-xs ${getRarityColor(rarity)}`}
                        >
                            {rarity}
                        </Badge>
                    )}

                    {/* 数量 */}
                    <div className="text-lg font-bold text-primary">
                        ×{count.toLocaleString()}
                    </div>

                    {/* 使用按钮 */}
                    {canUse && onUse && (
                        <Button
                            size="sm"
                            variant="outline"
                            className="w-full mt-1"
                            onClick={onUse}
                        >
                            {t('[ItemBoxButtonUse]') || 'Use'}
                        </Button>
                    )}
                </div>
            </CardContent>
        </Card>
    );
}
