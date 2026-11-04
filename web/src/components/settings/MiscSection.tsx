import React from 'react';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { Switch } from '@/components/ui/switch';
import { Label } from '@/components/ui/label';
import { Input } from '@/components/ui/input';
import { Shield, TowerControl as Tower } from 'lucide-react';
import { GuildTowerOption } from '@/api/generated/guildTowerOption';
import { useTranslation } from '@/hooks/useTranslation';

interface MiscSectionProps {
    guildTower: GuildTowerOption;
    onUpdateGuildTower: (config: GuildTowerOption) => void;
}

export function MiscSection({
    guildTower,
    onUpdateGuildTower,
}: MiscSectionProps) {
    const { t } = useTranslation();

    return (
        <div className="space-y-6">
            <Card>
                <CardHeader>
                    <div className="flex items-center space-x-2">
                        <Tower className="h-5 w-5 text-primary" />
                        <CardTitle>{t('SETTINGS_MISC_GUILD_TOWER_TITLE')}</CardTitle>
                    </div>
                    <CardDescription>{t('SETTINGS_MISC_GUILD_TOWER_DESC')}</CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                    <div className="grid gap-4 sm:grid-cols-2">
                        <div className="flex items-center justify-between space-x-2 rounded-lg border p-3">
                            <Label htmlFor="gt-entry">{t('SETTINGS_MISC_GUILD_TOWER_AUTO_ENTRY')}</Label>
                            <Switch
                                id="gt-entry"
                                checked={guildTower.autoEntry}
                                onCheckedChange={(checked) => onUpdateGuildTower({ ...guildTower, autoEntry: checked })}
                            />
                        </div>
                        <div className="flex items-center justify-between space-x-2 rounded-lg border p-3">
                            <Label htmlFor="gt-challenge">{t('SETTINGS_MISC_GUILD_TOWER_AUTO_CHALLENGE')}</Label>
                            <Switch
                                id="gt-challenge"
                                checked={guildTower.autoChallenge}
                                onCheckedChange={(checked) => onUpdateGuildTower({ ...guildTower, autoChallenge: checked })}
                            />
                        </div>
                        <div className="flex items-center justify-between space-x-2 rounded-lg border p-3">
                            <Label htmlFor="gt-reinforce">{t('SETTINGS_MISC_GUILD_TOWER_AUTO_REINFORCE')}</Label>
                            <Switch
                                id="gt-reinforce"
                                checked={guildTower.autoReinforcement}
                                onCheckedChange={(checked) => onUpdateGuildTower({ ...guildTower, autoReinforcement: checked })}
                            />
                        </div>
                        <div className="flex items-center justify-between space-x-2 rounded-lg border p-3">
                            <Label htmlFor="gt-reward">{t('SETTINGS_MISC_GUILD_TOWER_AUTO_REWARD')}</Label>
                            <Switch
                                id="gt-reward"
                                checked={guildTower.autoReceiveReward}
                                onCheckedChange={(checked) => onUpdateGuildTower({ ...guildTower, autoReceiveReward: checked })}
                            />
                        </div>
                    </div>

                    <div className="space-y-2">
                        <Label>{t('SETTINGS_MISC_GUILD_TOWER_RETRY_COUNT')}</Label>
                        <Input
                            type="number"
                            className="w-32"
                            value={guildTower.autoChallengeRetryCount}
                            onChange={(e) => onUpdateGuildTower({ ...guildTower, autoChallengeRetryCount: parseInt(e.target.value) || 0 })}
                        />
                    </div>
                </CardContent>
            </Card>

            <Card>
                <CardHeader>
                    <div className="flex items-center space-x-2">
                        <Shield className="h-5 w-5 text-primary" />
                        <CardTitle>{t('SETTINGS_MISC_OTHER_TITLE')}</CardTitle>
                    </div>
                    <CardDescription>{t('SETTINGS_MISC_OTHER_DESC')}</CardDescription>
                </CardHeader>
                <CardContent>
                    <p className="text-sm text-muted-foreground italic">{t('SETTINGS_MISC_OTHER_EMPTY')}</p>
                </CardContent>
            </Card>
        </div>
    );
}
