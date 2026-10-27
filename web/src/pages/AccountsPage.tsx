import { useEffect, useState } from 'react';
import { AccountManagement } from '@/components/account/AccountManagement';
import { ortegaApi } from '@/api/ortega-client';
import { UserStatusDtoInfo } from '@/api/generated/userStatusDtoInfo';
import { UserGetUserDataResponse } from '@/api/generated/';
import { useTranslation } from '@/hooks/useTranslation';

export function AccountsPage() {
    const [loading, setLoading] = useState(true);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [userInfo, setUserInfo] = useState<UserStatusDtoInfo | null>(null);
    const { t } = useTranslation();

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                setLoading(true);
                const response = await ortegaApi.user.getUserData({}) as UserGetUserDataResponse;
                if (response.userSyncData?.userStatusDtoInfo) {
                    setUserInfo(response.userSyncData.userStatusDtoInfo);
                    setIsLoggedIn(true);
                }
            } catch (error) {
                console.error('Failed to fetch user data:', error);
                setIsLoggedIn(false);
                setUserInfo(null);
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, []);

    return (
        <div className="min-h-screen bg-gradient-to-br from-slate-900 via-purple-900 to-slate-900">
            <div className="container mx-auto px-4 py-8">
                <div className="mb-6 flex justify-end">
                    <div className={`px-6 py-3 rounded-xl border backdrop-blur-md transition-all ${loading ? 'bg-slate-800/50 border-slate-700' :
                        isLoggedIn ? 'bg-emerald-900/30 border-emerald-500/50' :
                            'bg-red-900/30 border-red-500/50'
                        }`}>
                        {loading ? (
                            <div className="flex items-center gap-2 text-slate-300">
                                <div className="w-4 h-4 border-2 border-slate-300 border-t-transparent rounded-full animate-spin" />
                                <span>{t('ACCOUNT_CHECKING_LOGIN')}</span>
                            </div>
                        ) : isLoggedIn && userInfo ? (
                            <div className="flex items-center gap-6">
                                <div>
                                    <div className="text-xs text-emerald-300 font-medium uppercase tracking-wider mb-1">
                                        {t('ACCOUNT_CURRENT_LOGIN')}
                                    </div>
                                    <div className="text-lg font-bold text-white flex items-center gap-3">
                                        {userInfo.name}
                                        <span className="text-xs py-0.5 px-2 rounded bg-slate-800 text-slate-300 font-mono">
                                            ID: {userInfo.playerId}
                                        </span>
                                    </div>
                                </div>
                                <div className="flex gap-4 border-l border-white/10 pl-4">
                                    <div>
                                        <div className="text-xs text-slate-400">{t('[RankingGuildLevelLabel]')}</div>
                                        <div className="font-mono text-white">Lv.{userInfo.rank}</div>
                                    </div>
                                    <div>
                                        <div className="text-xs text-slate-400">VIP</div>
                                        <div className="font-mono text-yellow-400">VIP {userInfo.vip}</div>
                                    </div>
                                </div>
                            </div>
                        ) : (
                            <div className="flex items-center gap-2 text-red-200">
                                <span className="w-2 h-2 rounded-full bg-red-500" />
                                <span className="font-medium">{t('ACCOUNT_NOT_LOGGED_IN')}</span>
                            </div>
                        )}
                    </div>
                </div>

                <AccountManagement />
            </div>
        </div>
    );
}
