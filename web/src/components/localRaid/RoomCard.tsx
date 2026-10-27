import { Card, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Users, Lock, Trophy, Crown } from 'lucide-react';
import { LocalRaidPartyInfo } from '@/api/localRaidSignalRService';
import { LocalRaidRoomConditionsType } from '@/api/generated';
import { useTranslation } from '@/hooks/useTranslation';

interface RoomCardProps {
    room: LocalRaidPartyInfo;
    myBattlePower: number;
    onJoin: (room: LocalRaidPartyInfo) => void;
}

export function RoomCard({ room, myBattlePower, onJoin }: RoomCardProps) {
    const { t } = useTranslation();
    const memberCount = room.localRaidBattleLogPlayerInfoList?.length || 0;
    const isFull = memberCount >= 3;
    const hasPassword = room.conditionsType === LocalRaidRoomConditionsType.Password ||
                        room.conditionsType === LocalRaidRoomConditionsType.All;
    const hasBattlePowerLimit = room.conditionsType === LocalRaidRoomConditionsType.BattlePower ||
                                room.conditionsType === LocalRaidRoomConditionsType.All;
    const meetsBattlePower = !hasBattlePowerLimit || myBattlePower >= room.requiredBattlePower;
    const canJoin = !isFull && meetsBattlePower;

    return (
        <Card className={isFull ? 'opacity-60' : ''}>
            <CardContent className="py-3">
                <div className="flex items-center justify-between">
                    {/* 房间信息 */}
                    <div className="flex items-center gap-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary/10">
                            {hasPassword ? (
                                <Lock className="h-5 w-5 text-primary" />
                            ) : (
                                <Users className="h-5 w-5 text-primary" />
                            )}
                        </div>
                        <div>
                            <div className="flex items-center gap-2">
                                <span className="font-medium">{room.leaderPlayerName}</span>
                                <Crown className="h-4 w-4 text-yellow-500" />
                            </div>
                            <div className="flex items-center gap-2 text-sm text-muted-foreground">
                                <span>ID: {room.roomId?.slice(0, 8) || 'Unknown'}...</span>
                                <span>•</span>
                                <span>{t('ROOM_MEMBERS', [String(memberCount)])}</span>
                            </div>
                        </div>
                    </div>

                    {/* 条件标签 */}
                    <div className="flex items-center gap-2">
                        {hasPassword && (
                            <Badge variant="outline" className="text-xs">
                                <Lock className="h-3 w-3 mr-1" />
                                {t('ROOM_PASSWORD_REQUIRED')}
                            </Badge>
                        )}
                        {hasBattlePowerLimit && (
                            <Badge
                                variant={meetsBattlePower ? 'outline' : 'destructive'}
                                className="text-xs"
                            >
                                <Trophy className="h-3 w-3 mr-1" />
                                {room.requiredBattlePower.toLocaleString()}
                            </Badge>
                        )}
                        {isFull && (
                            <Badge variant="secondary" className="text-xs">
                                {t('ROOM_FULL')}
                            </Badge>
                        )}
                        {room.isAutoStart && (
                            <Badge variant="outline" className="text-xs bg-green-50 text-green-700">
                                {t('ROOM_AUTO_START')}
                            </Badge>
                        )}
                    </div>

                    {/* 加入按钮 */}
                    <Button
                        size="sm"
                        disabled={!canJoin}
                        onClick={() => onJoin(room)}
                    >
                        {t('ROOM_JOIN')}
                    </Button>
                </div>
            </CardContent>
        </Card>
    );
}
