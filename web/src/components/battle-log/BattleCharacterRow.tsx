import { BattleCharacterReport } from '@/api/generated/battleCharacterReport';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { useBattleUnitInfo } from '@/hooks/useBattleUnitInfo';
import { DamageBar } from './DamageBar';
import { cn } from '@/lib/utils';
import { useLocalizationStore } from '@/store/localization-store';
import { UnitIconType } from '@/api/generated/unitIconType';
import { AssetManager } from '@/lib/asset-manager';
import { useTranslation } from '@/hooks/useTranslation';

interface BattleCharacterRowProps {
    character: BattleCharacterReport;
    maxDamage: number;
    maxHeal: number;
    maxTaken: number;
}

// 构建图标 URL
function getUnitIconUrl(unitIconType: UnitIconType, unitIconId: number): string {
    switch (unitIconType) {
        case UnitIconType.Character:
        case UnitIconType.ShareCharacter:
            return AssetManager.character.getAvatarUrl(unitIconId);
        case UnitIconType.EnemyCharacter:
            return AssetManager.enemy.getUrl(unitIconId);
        case UnitIconType.WitchQlipha:
            return AssetManager.enemy.getUrl(unitIconId);
        default:
            return AssetManager.enemy.getUrl(unitIconId);
    }
}

export function BattleCharacterRow({
    character,
    maxDamage,
    maxHeal,
    maxTaken
}: BattleCharacterRowProps) {
    const t = useLocalizationStore(state => state.t);
    const { t: translate } = useTranslation();
    const { name: nameKey, unitIconType, unitIconId, isLoading } = useBattleUnitInfo(character.unitType, character.unitId);
    const name = t(nameKey);

    const isAttacker = character.groupType === BattleFieldCharacterGroupType.Attacker;
    const iconUrl = getUnitIconUrl(unitIconType, unitIconId);

    return (
        <div className={cn(
            "flex items-center gap-3 p-2 rounded-lg",
            isAttacker ? "bg-blue-50/50 dark:bg-blue-900/20" : "bg-red-50/50 dark:bg-red-900/20"
        )}>
            {/* 角色头像区域 */}
            <div className="flex flex-col items-center gap-1 shrink-0 w-14">
                <div className={cn(
                    "w-10 h-10 rounded-lg overflow-hidden flex items-center justify-center",
                    isAttacker
                        ? "bg-blue-100 dark:bg-blue-900"
                        : "bg-red-100 dark:bg-red-900"
                )}>
                    {isLoading ? (
                        <span className="text-xs font-bold text-muted-foreground">...</span>
                    ) : (
                        <img
                            src={iconUrl}
                            alt={name}
                            className="w-full h-full object-cover"
                            onError={(e) => {
                                // 图片加载失败时显示 ID 后两位
                                e.currentTarget.style.display = 'none';
                                e.currentTarget.parentElement!.innerHTML = `<span class="text-xs font-bold ${isAttacker ? 'text-blue-700 dark:text-blue-300' : 'text-red-700 dark:text-red-300'}">${character.unitId.toString().slice(-2)}</span>`;
                            }}
                        />
                    )}
                </div>
                <span className="text-[10px] text-muted-foreground">
                    Lv{character.characterLevel}
                </span>
            </div>

            {/* 角色名称 */}
            <div className="w-20 shrink-0">
                <div className="text-xs font-medium truncate">
                    {name}
                </div>
                <div className="text-[10px] text-muted-foreground">
                    HP: {character.hp.toLocaleString()}/{character.maxHp.toLocaleString()}
                </div>
            </div>

            {/* 统计数据 */}
            <div className="flex-1 space-y-1 min-w-0">
                <DamageBar
                    value={character.totalGiveDamage}
                    maxValue={maxDamage}
                    label={translate('BATTLE_LOG_DAMAGE')}
                    color="red"
                />
                <DamageBar
                    value={character.totalHpRecovery}
                    maxValue={maxHeal}
                    label={translate('BATTLE_LOG_HEAL')}
                    color="green"
                />
                <DamageBar
                    value={character.totalReceiveDamage}
                    maxValue={maxTaken}
                    label={translate('BATTLE_LOG_TAKEN')}
                    color="blue"
                />
            </div>
        </div>
    );
}
