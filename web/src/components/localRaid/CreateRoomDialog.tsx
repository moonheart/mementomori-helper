import { useState } from 'react';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Switch } from '@/components/ui/switch';
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from '@/components/ui/select';
import { LocalRaidQuestInfo } from '@/api/generated';
import { LocalRaidRoomConditionsType } from '@/api/generated';
import { CreateRoomParams } from '@/api/localRaidSignalRService';
import { useTranslation } from '@/hooks/useTranslation';

interface CreateRoomDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
    quest: LocalRaidQuestInfo | null;
    myBattlePower: number;
    onCreate: (params: CreateRoomParams) => Promise<void>;
}

export function CreateRoomDialog({
    open,
    onOpenChange,
    quest,
    myBattlePower,
    onCreate,
}: CreateRoomDialogProps) {
    const { t } = useTranslation();
    const [conditionsType, setConditionsType] = useState<LocalRaidRoomConditionsType>(
        LocalRaidRoomConditionsType.None
    );
    const [requiredBattlePower, setRequiredBattlePower] = useState(0);
    const [password, setPassword] = useState('0');
    const [isAutoStart, setIsAutoStart] = useState(true);
    const [creating, setCreating] = useState(false);

    const handleCreate = async () => {
        if (!quest) return;

        setCreating(true);
        try {
            await onCreate({
                conditionsType,
                questId: quest.id,
                requiredBattlePower: conditionsType === LocalRaidRoomConditionsType.BattlePower ||
                                     conditionsType === LocalRaidRoomConditionsType.All
                    ? requiredBattlePower
                    : 0,
                password: conditionsType === LocalRaidRoomConditionsType.Password ||
                          conditionsType === LocalRaidRoomConditionsType.All
                    ? parseInt(password) || 0
                    : 0,
                isAutoStart,
            });
        } finally {
            setCreating(false);
        }
    };

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-md">
                <DialogHeader>
                    <DialogTitle>{t('CREATE_ROOM_TITLE')}</DialogTitle>
                    <DialogDescription>
                        {t('CREATE_ROOM_DESC')}
                    </DialogDescription>
                </DialogHeader>

                <div className="space-y-4 py-4">
                    {/* 房间条件 */}
                    <div className="space-y-2">
                        <Label>{t('CREATE_ROOM_CONDITIONS')}</Label>
                        <Select
                            value={conditionsType.toString()}
                            onValueChange={(v) => setConditionsType(parseInt(v) as LocalRaidRoomConditionsType)}
                        >
                            <SelectTrigger>
                                <SelectValue placeholder={t('CREATE_ROOM_SELECT_CONDITION')} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value={LocalRaidRoomConditionsType.None.toString()}>
                                    {t('CREATE_ROOM_NO_LIMIT')}
                                </SelectItem>
                                <SelectItem value={LocalRaidRoomConditionsType.BattlePower.toString()}>
                                    {t('CREATE_ROOM_POWER_LIMIT')}
                                </SelectItem>
                                <SelectItem value={LocalRaidRoomConditionsType.Password.toString()}>
                                    {t('CREATE_ROOM_PASSWORD_ROOM')}
                                </SelectItem>
                                <SelectItem value={LocalRaidRoomConditionsType.All.toString()}>
                                    {t('CREATE_ROOM_POWER_AND_PASSWORD')}
                                </SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    {/* 战力限制 */}
                    {(conditionsType === LocalRaidRoomConditionsType.BattlePower ||
                      conditionsType === LocalRaidRoomConditionsType.All) && (
                        <div className="space-y-2">
                            <Label>{t('CREATE_ROOM_MIN_POWER')}</Label>
                            <Input
                                type="number"
                                value={requiredBattlePower}
                                onChange={(e) => setRequiredBattlePower(parseInt(e.target.value) || 0)}
                                placeholder={t('CREATE_ROOM_ENTER_POWER')}
                            />
                            <p className="text-xs text-muted-foreground">
                                {t('CREATE_ROOM_MY_POWER', [myBattlePower.toLocaleString()])}
                            </p>
                        </div>
                    )}

                    {/* 密码 */}
                    {(conditionsType === LocalRaidRoomConditionsType.Password ||
                      conditionsType === LocalRaidRoomConditionsType.All) && (
                        <div className="space-y-2">
                            <Label>{t('CREATE_ROOM_PASSWORD')}</Label>
                            <Input
                                type="number"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                placeholder={t('CREATE_ROOM_PASSWORD_PLACEHOLDER')}
                            />
                        </div>
                    )}

                    {/* 自动开始 */}
                    <div className="flex items-center justify-between">
                        <div className="space-y-0.5">
                            <Label>{t('CREATE_ROOM_AUTO_START_LABEL')}</Label>
                            <p className="text-xs text-muted-foreground">
                                {t('CREATE_ROOM_AUTO_START_DESC')}
                            </p>
                        </div>
                        <Switch
                            checked={isAutoStart}
                            onCheckedChange={setIsAutoStart}
                        />
                    </div>
                </div>

                <DialogFooter>
                    <Button variant="outline" onClick={() => onOpenChange(false)}>
                        {t('COMMON_CANCEL')}
                    </Button>
                    <Button onClick={handleCreate} disabled={creating}>
                        {creating ? t('CREATE_ROOM_CREATING') : t('LOCAL_RAID_CREATE_ROOM')}
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
}
