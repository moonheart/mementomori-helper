import { BattleCharacterReport } from '@/api/generated/battleCharacterReport';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { BattleCharacterRow } from './BattleCharacterRow';
import { cn } from '@/lib/utils';
import { Sword, Shield, Heart } from 'lucide-react';
import { useTranslation } from '@/hooks/useTranslation';

interface BattleTeamStatsProps {
    characters: BattleCharacterReport[];
    teamName: string;
    isWin: boolean;
    groupType: BattleFieldCharacterGroupType;
}

export function BattleTeamStats({
    characters,
    teamName,
    isWin,
    groupType
}: BattleTeamStatsProps) {
    const { t } = useTranslation();
    // 计算团队统计
    const totalDamage = characters.reduce((sum, c) => sum + c.totalGiveDamage, 0);
    const totalHeal = characters.reduce((sum, c) => sum + c.totalHpRecovery, 0);
    const totalTaken = characters.reduce((sum, c) => sum + c.totalReceiveDamage, 0);

    // 计算用于进度条的最大值 (本队最大值)
    const maxDamage = Math.max(...characters.map(c => c.totalGiveDamage), 1);
    const maxHeal = Math.max(...characters.map(c => c.totalHpRecovery), 1);
    const maxTaken = Math.max(...characters.map(c => c.totalReceiveDamage), 1);

    const isAttacker = groupType === BattleFieldCharacterGroupType.Attacker;

    return (
        <div className="flex-1 min-w-0">
            {/* 队伍头部 */}
            <div className={cn(
                "flex items-center justify-between p-3 rounded-t-lg",
                isAttacker
                    ? "bg-gradient-to-r from-blue-500 to-blue-600"
                    : "bg-gradient-to-r from-red-500 to-red-600"
            )}>
                <div className="flex items-center gap-2">
                    <span className={cn(
                        "px-2 py-0.5 rounded text-xs font-bold",
                        isWin ? "bg-yellow-400 text-yellow-900" : "bg-white/20 text-white"
                    )}>
                        {isWin ? 'WIN' : 'LOSE'}
                    </span>
                    <span className="text-white font-medium text-sm truncate">
                        {teamName || (isAttacker ? t('BATTLE_TEAM_ALLY') : t('BATTLE_TEAM_ENEMY'))}
                    </span>
                </div>
            </div>

            {/* 团队统计 */}
            <div className={cn(
                "grid grid-cols-3 gap-2 p-2 border-x border-b rounded-b-lg",
                isAttacker
                    ? "bg-blue-50/30 border-blue-200 dark:bg-blue-900/10 dark:border-blue-800"
                    : "bg-red-50/30 border-red-200 dark:bg-red-900/10 dark:border-red-800"
            )}>
                <div className="flex items-center gap-1.5">
                    <Sword className="w-3.5 h-3.5 text-red-500" />
                    <div>
                        <div className="text-[10px] text-muted-foreground">{t('BATTLE_TEAM_TOTAL_DAMAGE')}</div>
                        <div className="text-xs font-semibold">{totalDamage.toLocaleString()}</div>
                    </div>
                </div>
                <div className="flex items-center gap-1.5">
                    <Heart className="w-3.5 h-3.5 text-green-500" />
                    <div>
                        <div className="text-[10px] text-muted-foreground">{t('BATTLE_TEAM_TOTAL_HEAL')}</div>
                        <div className="text-xs font-semibold">{totalHeal.toLocaleString()}</div>
                    </div>
                </div>
                <div className="flex items-center gap-1.5">
                    <Shield className="w-3.5 h-3.5 text-blue-500" />
                    <div>
                        <div className="text-[10px] text-muted-foreground">{t('BATTLE_TEAM_TOTAL_TAKEN')}</div>
                        <div className="text-xs font-semibold">{totalTaken.toLocaleString()}</div>
                    </div>
                </div>
            </div>

            {/* 角色列表 */}
            <div className="mt-3 space-y-2">
                {characters.map((character, index) => (
                    <BattleCharacterRow
                        key={`${character.characterGuid}-${index}`}
                        character={character}
                        maxDamage={maxDamage}
                        maxHeal={maxHeal}
                        maxTaken={maxTaken}
                    />
                ))}
            </div>
        </div>
    );
}
