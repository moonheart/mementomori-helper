import { useEffect, useState, useRef } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { ScrollArea } from '@/components/ui/scroll-area';
import { useBattleLog } from '@/hooks/useBattleLog';
import { BattleLogModal } from '@/components/battle-log';
import { BattleType } from '@/api/generated/battleType';
import { BattleSimulationResult } from '@/api/generated/battleSimulationResult';
import { toast } from '@/hooks/use-toast';
import { convertKeysToCamelCase } from '@/lib/keyConverter';
import {
    Trophy,
    Sword,
    Target,
    Clock,
    ChevronLeft,
    ChevronRight,
    Trash2,
    Loader2,
    Filter,
    Upload
} from 'lucide-react';
import { cn } from '@/lib/utils';
import { useTranslation } from '@/hooks/useTranslation';

const battleTypeNames: Record<number, string> = {
    [BattleType.Auto]: '[BattleTypeAuto]',
    [BattleType.Boss]: '[BattleTypeBoss]',
    [BattleType.GuildBattle]: '[BattleTypeGvG]',
    [BattleType.BattleLeague]: '[BattleTypeBattleLeague]',
    [BattleType.LegendLeague]: '[BattleTypeLegendLeague]',
    [BattleType.LocalRaid]: '[CommonHeaderLocalRaidLabel]',
    [BattleType.TowerBattle]: '[TowerTypeInfinite]',
    [BattleType.DungeonBattle]: '[CommonHeaderDungeonBattleLabel]',
    [BattleType.GuildRaid]: '[CommonHeaderGuildRaidLabel]',
    [BattleType.GuildTower]: '[GuildTowerTitle]',
    [BattleType.FriendBattle]: '[BattleTypeFriendBattle]',
};

export function BattleLogPage() {
    const { t } = useTranslation();
    const {
        logs,
        loading,
        selectedLog,
        isModalOpen,
        pagination,
        loadLogs,
        viewLogDetail,
        deleteLog,
        closeModal
    } = useBattleLog({ pageSize: 20 });

    const [selectedType, setSelectedType] = useState<number | null>(null);

    // 手动导入 JSON 文件相关状态
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [manualBattleData, setManualBattleData] = useState<BattleSimulationResult | null>(null);
    const [isManualModalOpen, setIsManualModalOpen] = useState(false);

    useEffect(() => {
        loadLogs({ battleType: selectedType ?? undefined });
    }, [selectedType]);

    // 处理文件上传
    const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = (e) => {
            try {
                const content = e.target?.result as string;
                const parsed = JSON.parse(content);
                // 兼容 PascalCase 和 camelCase 两种格式的 JSON
                const battleData = convertKeysToCamelCase<BattleSimulationResult>(parsed);

                // 验证基本结构
                if (!battleData.battleEndInfo || !battleData.battleCharacterReports) {
                    throw new Error('无效的战斗日志格式');
                }

                setManualBattleData(battleData);
                setIsManualModalOpen(true);
                toast({
                    title: t('BATTLE_LOG_IMPORT_SUCCESS'),
                    description: t('BATTLE_LOG_IMPORT_SUCCESS_DESC')
                });
            } catch (error) {
                console.error('Failed to parse battle log:', error);
                toast({
                    title: t('BATTLE_LOG_IMPORT_FAILED'),
                    description: t('BATTLE_LOG_IMPORT_FAILED_DESC'),
                    variant: 'destructive'
                });
            }
        };
        reader.readAsText(file);

        // 重置 input 以便重复选择同一文件
        if (event.target) {
            event.target.value = '';
        }
    };

    const handleUploadClick = () => {
        fileInputRef.current?.click();
    };

    const closeManualModal = () => {
        setIsManualModalOpen(false);
        setManualBattleData(null);
    };

    const handlePageChange = (newPage: number) => {
        if (newPage < 1 || newPage > pagination.totalPages) return;
        loadLogs({ battleType: selectedType ?? undefined, page: newPage });
    };

    const formatDate = (dateStr: string) => {
        const date = new Date(dateStr);
        return date.toLocaleString('zh-CN', {
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    const formatDuration = (ms: number) => {
        if (ms < 1000) return `${ms}ms`;
        return `${(ms / 1000).toFixed(1)}s`;
    };

    return (
        <div className="space-y-6">
            {/* 筛选栏 */}
            <Card>
                <CardContent className="p-4">
                    <div className="flex items-center justify-between gap-4">
                        <div className="flex items-center gap-2 flex-wrap">
                            <Filter className="w-4 h-4 text-muted-foreground" />
                            <span className="text-sm text-muted-foreground">{t('BATTLE_LOG_FILTER')}</span>
                            <Button
                                variant={selectedType === null ? 'default' : 'outline'}
                                size="sm"
                                onClick={() => setSelectedType(null)}
                            >
                                {t('[CommonAllLabel]')}
                            </Button>
                            {[BattleType.Boss, BattleType.BattleLeague, BattleType.LegendLeague, BattleType.TowerBattle].map(type => (
                                <Button
                                    key={type}
                                    variant={selectedType === type ? 'default' : 'outline'}
                                    size="sm"
                                    onClick={() => setSelectedType(type)}
                                >
                                    {battleTypeNames[type] ? t(battleTypeNames[type]) : t('BATTLE_LOG_TYPE_FALLBACK', [type])}
                                </Button>
                            ))}
                        </div>
                        {/* 手动导入按钮 */}
                        <div className="flex items-center gap-2">
                            <input
                                ref={fileInputRef}
                                type="file"
                                accept=".json"
                                onChange={handleFileUpload}
                                className="hidden"
                            />
                            <Button
                                variant="outline"
                                size="sm"
                                onClick={handleUploadClick}
                            >
                                <Upload className="w-4 h-4 mr-1" />
                                {t('BATTLE_LOG_IMPORT')}
                            </Button>
                        </div>
                    </div>
                </CardContent>
            </Card>

            {/* 战斗日志列表 */}
            <Card>
                <CardHeader>
                    <CardTitle className="flex items-center justify-between">
                        <span>{t('BATTLE_LOG_HISTORY')}</span>
                        <span className="text-sm font-normal text-muted-foreground">
                            {t('BATTLE_LOG_TOTAL', [pagination.total])}
                        </span>
                    </CardTitle>
                </CardHeader>
                <CardContent>
                    {loading ? (
                        <div className="flex justify-center py-12">
                            <Loader2 className="w-8 h-8 animate-spin text-muted-foreground" />
                        </div>
                    ) : logs.length === 0 ? (
                        <div className="text-center py-12 text-muted-foreground">
                            <Trophy className="w-12 h-12 mx-auto mb-3 opacity-50" />
                            <p>{t('BATTLE_LOG_EMPTY')}</p>
                            <p className="text-sm mt-1">{t('BATTLE_LOG_EMPTY_HINT')}</p>
                        </div>
                    ) : (
                        <>
                            <ScrollArea className="h-[500px]">
                                <div className="space-y-2">
                                    {logs.map((log) => (
                                        <div
                                            key={log.battleToken}
                                            className={cn(
                                                "flex items-center gap-4 p-3 rounded-lg border cursor-pointer transition-colors",
                                                "hover:bg-muted/50",
                                                log.isWin
                                                    ? "border-green-200 dark:border-green-800 bg-green-50/30 dark:bg-green-900/10"
                                                    : "border-red-200 dark:border-red-800 bg-red-50/30 dark:bg-red-900/10"
                                            )}
                                            onClick={() => viewLogDetail(log.battleToken)}
                                        >
                                            {/* 胜负标识 */}
                                            <div className={cn(
                                                "w-12 h-12 rounded-lg flex items-center justify-center shrink-0",
                                                log.isWin
                                                    ? "bg-green-100 text-green-700 dark:bg-green-900 dark:text-green-300"
                                                    : "bg-red-100 text-red-700 dark:bg-red-900 dark:text-red-300"
                                            )}>
                                                {log.isWin ? (
                                                    <Trophy className="w-6 h-6" />
                                                ) : (
                                                    <Sword className="w-6 h-6" />
                                                )}
                                            </div>

                                            {/* 战斗信息 */}
                                            <div className="flex-1 min-w-0">
                                                <div className="flex items-center gap-2">
                                                    <Badge
                                                        variant={log.isWin ? 'default' : 'destructive'}
                                                        className="text-xs"
                                                    >
                                                        {log.isWin ? t('BATTLE_LOG_WIN') : t('BATTLE_LOG_LOSS')}
                                                    </Badge>
                                                    <span className="text-sm text-muted-foreground">
                                                        {battleTypeNames[log.battleType] ? t(battleTypeNames[log.battleType]) : t('BATTLE_LOG_TYPE_FALLBACK', [log.battleType])}
                                                    </span>
                                                    {log.questId && (
                                                        <span className="text-xs text-muted-foreground">
                                                            {t('BATTLE_STAGE', [log.questId])}
                                                        </span>
                                                    )}
                                                </div>
                                                <div className="flex items-center gap-3 mt-1 text-xs text-muted-foreground">
                                                    <span className="flex items-center gap-1">
                                                        <Clock className="w-3 h-3" />
                                                        {formatDate(log.createdAt)}
                                                    </span>
                                                    <span className="flex items-center gap-1">
                                                        <Target className="w-3 h-3" />
                                                        {t('BATTLE_LOG_TURNS', [log.endTurn])}
                                                    </span>
                                                    <span>{formatDuration(log.battleDurationMs)}</span>
                                                </div>
                                            </div>

                                            {/* 操作按钮 */}
                                            <Button
                                                variant="ghost"
                                                size="icon"
                                                className="shrink-0"
                                                onClick={(e) => {
                                                    e.stopPropagation();
                                                    deleteLog(log.battleToken);
                                                }}
                                            >
                                                <Trash2 className="w-4 h-4 text-muted-foreground" />
                                            </Button>
                                        </div>
                                    ))}
                                </div>
                            </ScrollArea>

                            {/* 分页 */}
                            {pagination.totalPages > 1 && (
                                <div className="flex items-center justify-center gap-2 mt-4 pt-4 border-t">
                                    <Button
                                        variant="outline"
                                        size="sm"
                                        onClick={() => handlePageChange(pagination.page - 1)}
                                        disabled={pagination.page <= 1}
                                    >
                                        <ChevronLeft className="w-4 h-4" />
                                    </Button>
                                    <span className="text-sm text-muted-foreground">
                                        {pagination.page} / {pagination.totalPages}
                                    </span>
                                    <Button
                                        variant="outline"
                                        size="sm"
                                        onClick={() => handlePageChange(pagination.page + 1)}
                                        disabled={pagination.page >= pagination.totalPages}
                                    >
                                        <ChevronRight className="w-4 h-4" />
                                    </Button>
                                </div>
                            )}
                        </>
                    )}
                </CardContent>
            </Card>

            {/* 战斗详情弹窗 - 数据库记录 */}
            <BattleLogModal
                isOpen={isModalOpen}
                onClose={closeModal}
                battleData={selectedLog}
            />

            {/* 战斗详情弹窗 - 手动导入 */}
            <BattleLogModal
                isOpen={isManualModalOpen}
                onClose={closeManualModal}
                battleData={manualBattleData}
            />
        </div>
    );
}
