import { useState, useEffect } from 'react';
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';

import { Loader2, Check, Gift, PartyPopper } from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { LoginBonusGetMonthlyLoginBonusInfoResponse } from '@/api/generated/LoginBonusgetMonthlyLoginBonusInfoResponse';
import { LoginBonusReceiveDailyLoginBonusResponse } from '@/api/generated/LoginBonusreceiveDailyLoginBonusResponse';
import { MonthlyLoginBonusMB } from '@/api/generated/monthlyLoginBonusMB';
import { MonthlyLoginBonusRewardListMB } from '@/api/generated/monthlyLoginBonusRewardListMB';
import { LoginDailyRewardInfo } from '@/api/generated/loginDailyRewardInfo';
import { LoginCountRewardInfo } from '@/api/generated/loginCountRewardInfo';
import { useMasterData } from '@/hooks/useMasterData';
import { useItemName } from '@/hooks/useItemName';
import { useTranslation } from '@/hooks/useTranslation';

import { cn } from '@/lib/utils';

interface MonthlyLoginBonusDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
}

export function MonthlyLoginBonusDialog({ open, onOpenChange }: MonthlyLoginBonusDialogProps) {
    const [loading, setLoading] = useState(true);
    const [info, setInfo] = useState<LoginBonusGetMonthlyLoginBonusInfoResponse | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [receivingDay, setReceivingDay] = useState<number | null>(null);
    const [receiveSuccess, setReceiveSuccess] = useState<{day: number, items: string} | null>(null);
    const [selectedDay, setSelectedDay] = useState<number | null>(null);
    const { t } = useTranslation();
    const { getItemName, isLoading: isItemNameLoading } = useItemName();

    // 获取当前月份的 Master 数据
    const { data: monthlyBonusMb, loading: loadingMb } = useMasterData<MonthlyLoginBonusMB>(
        'MonthlyLoginBonusTable',
        info?.monthlyLoginBonusId
    );

    // 获取奖励列表 Master 数据
    const { data: rewardListMb, loading: loadingRewardList } = useMasterData<MonthlyLoginBonusRewardListMB>(
        'MonthlyLoginBonusRewardListTable',
        monthlyBonusMb?.rewardListId
    );

    useEffect(() => {
        if (open) {
            fetchMonthlyLoginBonusInfo();
        } else {
            setSelectedDay(null);
            setReceivingDay(null);
            setReceiveSuccess(null);
            setError(null);
        }
    }, [open]);

    // 数据加载完成后，默认选中今天（如果今天未领取）
    useEffect(() => {
        if (info && !loading) {
            const currentDay = new Date().getDate();
            const isTodayReceived = info.receivedDailyRewardDayList?.includes(currentDay);
            if (!isTodayReceived) {
                setSelectedDay(currentDay);
            } else {
                setSelectedDay(null);
            }
        }
    }, [info, loading]);

    async function fetchMonthlyLoginBonusInfo() {
        try {
            setLoading(true);
            setError(null);
            const response = await ortegaApi.loginBonus.getMonthlyLoginBonusInfo({}) as LoginBonusGetMonthlyLoginBonusInfoResponse;
            setInfo(response);
        } catch (err) {
            console.error('Failed to fetch monthly login bonus info:', err);
            setError(t('LOGIN_BONUS_FETCH_FAILED'));
        } finally {
            setLoading(false);
        }
    }

    async function receiveDailyReward(day: number) {
        setReceivingDay(day);
        setError(null);

        try {
            const response = await ortegaApi.loginBonus.receiveDailyLoginBonus({ receiveDay: day }) as LoginBonusReceiveDailyLoginBonusResponse;

            // 构建奖励文本
            const rewardText = response.rewardItemList
                .map(item => `${getItemName(item.itemType, item.itemId)}×${item.itemCount.toLocaleString()}`)
                .join(', ');

            setReceiveSuccess({ day, items: rewardText });

            // 3秒后清除成功提示
            setTimeout(() => setReceiveSuccess(null), 3000);
        } catch (err) {
            console.error('Failed to receive daily login bonus:', err);
            setError(t('LOGIN_BONUS_CLAIM_FAILED'));
            setTimeout(() => setError(null), 3000);
        }

        // 无论成功失败都刷新数据并重置状态
        try {
            await fetchMonthlyLoginBonusInfo();
        } catch (refreshErr) {
            console.error('Failed to refresh data:', refreshErr);
        } finally {
            setReceivingDay(null);
        }
    }

    const isReceivedDaily = (day: number) => info?.receivedDailyRewardDayList?.includes(day) ?? false;
    const isReceivedCount = (dayCount: number) => info?.receivedLoginCountRewardDayList?.includes(dayCount) ?? false;

    // 根据实际日期计算今天是这个月的第几天
    const today = new Date().getDate();

    const isAllLoading = loading || loadingMb || loadingRewardList || isItemNameLoading;

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-5xl max-h-[85vh] overflow-y-auto">
                <DialogHeader>
                    <DialogTitle className="flex items-center gap-2">
                        <Gift className="h-5 w-5 text-yellow-500" />
                        {t('LOGIN_BONUS_MONTHLY_TITLE')}
                        {monthlyBonusMb && (
                            <Badge variant="secondary">{monthlyBonusMb.yearMonth}</Badge>
                        )}
                    </DialogTitle>
                </DialogHeader>

                {error && (
                    <div className="text-center text-red-500 py-4">{error}</div>
                )}

                {receiveSuccess && (
                    <div className="flex items-center gap-2 p-3 rounded-lg bg-green-50 border border-green-200 dark:bg-green-950 dark:border-green-800 text-green-700 dark:text-green-300">
                        <PartyPopper className="h-5 w-5" />
                        <span className="text-sm">
                            {t('LOGIN_BONUS_CLAIM_SUCCESS', [String(receiveSuccess.day), receiveSuccess.items])}
                        </span>
                    </div>
                )}

                {isAllLoading ? (
                    <div className="flex items-center justify-center py-8">
                        <Loader2 className="h-6 w-6 animate-spin" />
                        <span className="ml-2">{t('COMMON_LOADING')}</span>
                    </div>
                ) : (
                    <div className="space-y-6">
                        {/* 每日签到 */}
                        <div>
                            <div className="grid grid-cols-4 sm:grid-cols-7 gap-2">
                                {rewardListMb?.dailyRewardList?.map((reward: LoginDailyRewardInfo) => {
                                    const day = reward.day;
                                    const received = isReceivedDaily(day);
                                    const isToday = day === today;
                                    const isSelected = selectedDay === day;

                                    return (
                                        <div
                                            key={day}
                                            onClick={() => !received && setSelectedDay(day)}
                                            className={cn(
                                                "relative p-2 rounded-lg border text-center cursor-pointer transition-all",
                                                received
                                                    ? "bg-green-50 border-green-200 dark:bg-green-950 dark:border-green-800 cursor-not-allowed opacity-70"
                                                    : isSelected
                                                        ? "bg-blue-100 border-blue-500 dark:bg-blue-900 dark:border-blue-400 ring-2 ring-blue-500"
                                                        : isToday
                                                            ? "bg-blue-50 border-blue-300 dark:bg-blue-950 dark:border-blue-700 hover:bg-blue-100"
                                                            : day < today
                                                                ? "bg-gray-50 border-gray-200 dark:bg-gray-900 dark:border-gray-800 opacity-60"
                                                                : "bg-card border-border hover:bg-primary/5"
                                            )}
                                        >
                                            <div className="text-xs text-muted-foreground mb-1">{t('LOGIN_BONUS_DAY', [String(day)])}</div>
                                            <div className="font-medium text-xs sm:text-sm leading-tight min-h-10 flex items-center justify-center">
                                                {getItemName(reward.rewardItem.itemType, reward.rewardItem.itemId)}
                                            </div>
                                            <div className="text-xs text-muted-foreground">
                                                ×{reward.rewardItem.itemCount.toLocaleString()}
                                            </div>
                                            {received && (
                                                <div className="absolute top-1 right-1">
                                                    <Check className="h-3 w-3 text-green-500" />
                                                </div>
                                            )}
                                            {isToday && !received && (
                                                <div className="absolute -top-1 -right-1">
                                                    <Badge variant="destructive" className="text-[10px] h-4 px-1">{t('LOGIN_BONUS_TODAY')}</Badge>
                                                </div>
                                            )}
                                            {isSelected && !received && (
                                                <div className="absolute bottom-1 right-1">
                                                    <div className="w-2 h-2 rounded-full bg-blue-500"></div>
                                                </div>
                                            )}
                                        </div>
                                    );
                                })}
                            </div>
                            <div className="mt-4 text-sm text-muted-foreground text-center" dangerouslySetInnerHTML={{ __html: t('LOGIN_BONUS_MONTHLY_PROGRESS', [String(info?.receivedDailyRewardDayList?.length ?? 0)]) }} />
                            {selectedDay && !isReceivedDaily(selectedDay) && (
                                <div className="text-sm text-muted-foreground text-center">
                                    <span className="ml-2" dangerouslySetInnerHTML={{ __html: t('LOGIN_BONUS_SELECTED_DAY', [String(selectedDay)]) }} />
                                </div>
                            )}
                        </div>

                        {/* 累计签到 */}
                        <div>
                            <h4 className="text-sm font-medium text-muted-foreground mb-3">{t('LOGIN_BONUS_CUMULATIVE_TITLE')}</h4>
                            <div className="grid grid-cols-4 gap-3">
                                {rewardListMb?.loginCountRewardList?.map((reward: LoginCountRewardInfo) => {
                                    const received = isReceivedCount(reward.dayCount);
                                    const canReceive = (info?.pastReceivedCount ?? 0) >= reward.dayCount;

                                    return (
                                        <div
                                            key={reward.dayCount}
                                            className={cn(
                                                "flex flex-col items-center p-3 rounded-lg border text-center",
                                                received
                                                    ? "bg-green-50 border-green-200 dark:bg-green-950 dark:border-green-800"
                                                    : canReceive
                                                        ? "bg-blue-50 border-blue-200 dark:bg-blue-950 dark:border-blue-800"
                                                        : "bg-card border-border opacity-60"
                                            )}
                                        >
                                            {/* 天数标识 */}
                                            <div className={cn(
                                                "w-10 h-10 rounded-full flex items-center justify-center text-sm font-bold mb-2",
                                                received
                                                    ? "bg-green-100 text-green-700 dark:bg-green-900 dark:text-green-300"
                                                    : canReceive
                                                        ? "bg-blue-100 text-blue-700 dark:bg-blue-900 dark:text-blue-300"
                                                        : "bg-muted text-muted-foreground"
                                            )}>
                                                {t('LOGIN_BONUS_DAYS', [String(reward.dayCount)])}
                                            </div>

                                            {/* 奖励列表 */}
                                            <div className="flex flex-col gap-1 w-full">
                                                {reward.rewardItemList.map((item, idx) => (
                                                    <div key={idx} className="text-xs">
                                                        <span className="font-medium">{getItemName(item.itemType, item.itemId)}</span>
                                                        <span className="text-muted-foreground"> ×{item.itemCount.toLocaleString()}</span>
                                                    </div>
                                                ))}
                                            </div>

                                            {/* 状态 */}
                                            <div className="mt-2 pt-2 border-t w-full">
                                                {received ? (
                                                    <div className="flex items-center justify-center gap-1 text-green-600 text-xs">
                                                        <Check className="h-3 w-3" />
                                                        <span>{t('LOGIN_BONUS_RECEIVED')}</span>
                                                    </div>
                                                ) : canReceive ? (
                                                    <Badge variant="default" className="text-xs">{t('LOGIN_BONUS_CLAIMABLE')}</Badge>
                                                ) : (
                                                    <Badge variant="outline" className="text-xs text-muted-foreground">{t('LOGIN_BONUS_NOT_ACHIEVED')}</Badge>
                                                )}
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    </div>
                )}

                <div className="flex justify-between items-center mt-4 pt-4 border-t">
                    <div className="text-sm text-muted-foreground">
                        {selectedDay ? (
                            isReceivedDaily(selectedDay) ? (
                                <span className="text-green-600">{t('LOGIN_BONUS_DAY_RECEIVED', [String(selectedDay)])}</span>
                            ) : selectedDay === today ? (
                                <span>{t('LOGIN_BONUS_DAY_READY', [String(selectedDay)])}</span>
                            ) : selectedDay < today ? (
                                <span className="text-gray-500">{t('LOGIN_BONUS_DAY_EXPIRED', [String(selectedDay)])}</span>
                            ) : (
                                <span className="text-gray-500">{t('LOGIN_BONUS_DAY_NOT_READY', [String(selectedDay)])}</span>
                            )
                        ) : (
                            <span>{t('LOGIN_BONUS_CLICK_TO_SELECT')}</span>
                        )}
                    </div>
                    <div className="flex gap-2">
                        <Button variant="outline" onClick={() => onOpenChange(false)}>
                            {t('COMMON_CLOSE')}
                        </Button>
                        <Button
                            onClick={() => selectedDay && receiveDailyReward(selectedDay)}
                            disabled={!selectedDay || isReceivedDaily(selectedDay) || selectedDay !== today || receivingDay !== null}
                        >
                            {receivingDay !== null && receivingDay === selectedDay ? (
                                <>
                                    <Loader2 className="h-4 w-4 animate-spin mr-2" />
                                    {t('LOGIN_BONUS_CLAIMING')}
                                </>
                            ) : (
                                t('LOGIN_BONUS_CLAIM')
                            )}
                        </Button>
                    </div>
                </div>
            </DialogContent>
        </Dialog>
    );
}
