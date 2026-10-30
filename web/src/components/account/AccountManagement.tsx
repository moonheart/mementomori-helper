import { useEffect, useState } from 'react';
import { accountApi } from '@/api/account-service';
import { useAccountStore } from '@/store/accountStore';
import { AddAccountDialog } from '@/components/account/AddAccountDialog';
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';
import { useToast } from '@/hooks/use-toast';
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
} from '@/components/ui/alert-dialog';
import { WorldSelect } from './WorldSelect';
import { Badge } from '@/components/ui/badge';
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';
import { Loader2, MoreVertical, Plus, Trash2, LogIn, ArrowRight, Copy, RefreshCw, UserPlus } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from '@/hooks/useTranslation';
import { useLocalizationStore } from '@/store/localization-store';

export function AccountManagement() {
    const { accounts, currentAccountId, setAccounts, removeAccount, setCurrentAccount, setLoading } = useAccountStore();
    const navigate = useNavigate();
    const [showAddDialog, setShowAddDialog] = useState(false);
    const [deleteUserId, setDeleteUserId] = useState<number | null>(null);
    const [loginLoading, setLoginLoading] = useState<number | null>(null);
    const { toast } = useToast();
    const { t } = useTranslation();
    const currentLanguage = useLocalizationStore((state) => state.currentLanguage);

    useEffect(() => {
        loadAccounts();
    }, []);

    const loadAccounts = async () => {
        try {
            setLoading(true);
            const data = await accountApi.getAccounts();
            setAccounts(data);
        } catch (error) {
            console.error('Failed to load accounts:', error);
            toast({
                title: t('ACCOUNT_ERROR'),
                description: t('ACCOUNT_LOAD_FAILED'),
                variant: 'destructive',
            });
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async () => {
        if (!deleteUserId) return;

        try {
            await accountApi.deleteAccount(deleteUserId);
            removeAccount(deleteUserId);
            toast({
                title: t('ACCOUNT_SUCCESS'),
                description: t('ACCOUNT_DELETE_SUCCESS'),
            });
        } catch (error) {
            console.error('Failed to delete account:', error);
            toast({
                title: t('ACCOUNT_ERROR'),
                description: t('ACCOUNT_DELETE_FAILED'),
                variant: 'destructive',
            });
        } finally {
            setDeleteUserId(null);
        }
    };

    const handleLogin = async (userId: number, clientKey: string) => {
        try {
            setLoginLoading(userId);
            const result = await accountApi.login({ userId, clientKey });

            if (result.success) {
                setCurrentAccount(userId);
                await loadAccounts();
                toast({
                    title: t('ACCOUNT_LOGIN_SUCCESS'),
                    description: result.message,
                });
                return true;
            } else {
                toast({
                    title: t('ACCOUNT_LOGIN_FAILED'),
                    description: result.message,
                    variant: 'destructive',
                });
            }
        } catch (error) {
            console.error('Failed to login:', error);
            toast({
                title: t('ACCOUNT_ERROR'),
                description: t('ACCOUNT_LOGIN_FAILED'),
                variant: 'destructive',
            });
        } finally {
            setLoginLoading(null);
        }
        return false;
    };

    const handleEnterAccount = (userId: number) => {
        setCurrentAccount(userId);
        toast({
            title: t('ACCOUNT_SWITCHED'),
            description: t('ACCOUNT_ENTERING_GAME'),
        });
        navigate('/dashboard');
    };

    const dateTimeLocales: Record<string, string> = {
        zhCN: 'zh-CN',
        zhTW: 'zh-TW',
        enUS: 'en-US',
        jaJP: 'ja-JP',
        koKR: 'ko-KR'
    };

    const formatLastLogin = (timestamp: number | Date) => {
        const date = timestamp instanceof Date ? timestamp : new Date(timestamp);
        // "2 小时前 / 2 hours ago", 超过一周再显示日期
        const locale = dateTimeLocales[currentLanguage] ?? 'en-US';
        const seconds = (date.getTime() - Date.now()) / 1000;
        if (Math.abs(seconds) > 7 * 86400) return date.toLocaleDateString(locale);
        const rtf = new Intl.RelativeTimeFormat(locale, { numeric: 'auto' });
        const units: [Intl.RelativeTimeFormatUnit, number][] = [['day', 86400], ['hour', 3600], ['minute', 60]];
        const [unit, size] = units.find(([, n]) => Math.abs(seconds) >= n) ?? ['second', 1];
        return rtf.format(Math.round(seconds / size), unit);
    };

    // 头像颜色: 按名字固定一个颜色, 不同账号一眼能分开
    const avatarColor = (name: string) => {
        const colors = ['bg-rose-500', 'bg-amber-500', 'bg-emerald-500', 'bg-sky-500', 'bg-violet-500', 'bg-pink-500', 'bg-teal-500'];
        let h = 0;
        for (const ch of name) h = (h * 31 + ch.charCodeAt(0)) >>> 0;
        return colors[h % colors.length];
    };

    const copyId = async (userId: number) => {
        try {
            await navigator.clipboard.writeText(String(userId));
            toast({ title: t('ACCOUNT_ID_COPIED') });
        } catch { /* 剪贴板不可用就算了 */ }
    };

    // 一个主按钮: 已登录 → 打开; 没登录 → 登录后直接进主页
    const openAccount = async (userId: number, clientKey: string, isLoggedIn: boolean) => {
        if (isLoggedIn) return handleEnterAccount(userId);
        if (await handleLogin(userId, clientKey)) handleEnterAccount(userId);
    };

    return (
        <div className="space-y-4">
            <div className="flex flex-wrap items-end justify-between gap-3">
                <div>
                    <h1 className="text-2xl font-semibold tracking-tight">{t('ACCOUNT_MANAGEMENT_TITLE')}</h1>
                    <p className="text-sm text-muted-foreground">{t('ACCOUNT_PAGE_DESC')}</p>
                </div>
                {accounts.length > 0 && (
                    <Button onClick={() => setShowAddDialog(true)} variant="outline">
                        <Plus className="mr-2 h-4 w-4" />
                        {t('ACCOUNT_ADD_ACCOUNT')}
                    </Button>
                )}
            </div>

            {accounts.length === 0 ? (
                <Card className="border-dashed">
                    <CardContent className="flex flex-col items-center gap-3 py-14 text-center">
                        <div className="flex h-12 w-12 items-center justify-center rounded-full bg-muted">
                            <UserPlus className="h-6 w-6 text-muted-foreground" />
                        </div>
                        <div>
                            <p className="font-medium">{t('ACCOUNT_NO_ACCOUNTS')}</p>
                            <p className="mt-1 max-w-sm text-sm text-muted-foreground">{t('ACCOUNT_ADD_HINT')}</p>
                        </div>
                        <Button onClick={() => setShowAddDialog(true)}>
                            <Plus className="mr-2 h-4 w-4" />
                            {t('ACCOUNT_ADD_FIRST')}
                        </Button>
                    </CardContent>
                </Card>
            ) : (
                <ul className="space-y-3">
                    {accounts.map((account) => {
                        const isCurrent = currentAccountId === account.userId;
                        const busy = loginLoading === account.userId;
                        return (
                            <li key={account.userId}>
                                <Card className={isCurrent ? 'border-primary/40 shadow-sm' : ''}>
                                    <CardContent className="flex flex-wrap items-center gap-4 p-4 sm:flex-nowrap">
                                        <div className="relative shrink-0">
                                            <div className={`flex h-12 w-12 items-center justify-center rounded-full text-lg font-semibold text-white ${avatarColor(account.name)}`}>
                                                {account.name.slice(0, 1).toUpperCase()}
                                            </div>
                                            <span
                                                className={`absolute -bottom-0.5 -right-0.5 h-3.5 w-3.5 rounded-full border-2 border-card ${account.isLoggedIn ? 'bg-emerald-500' : 'bg-muted-foreground/40'}`}
                                                title={account.isLoggedIn ? t('[Online]') : t('ACCOUNT_OFFLINE')}
                                            />
                                        </div>

                                        <div className="min-w-0 flex-1 space-y-1.5">
                                            <div className="flex flex-wrap items-center gap-2">
                                                <span className="truncate text-base font-semibold">{account.name}</span>
                                                <span className={`text-xs ${account.isLoggedIn ? 'text-emerald-600 dark:text-emerald-400' : 'text-muted-foreground'}`}>
                                                    {account.isLoggedIn ? t('[Online]') : t('ACCOUNT_OFFLINE')}
                                                </span>
                                                {isCurrent && <Badge variant="secondary" className="text-xs">{t('ACCOUNT_CURRENT')}</Badge>}
                                            </div>
                                            <div className="flex flex-wrap items-center gap-x-3 gap-y-1.5 text-sm text-muted-foreground">
                                                <WorldSelect userId={account.userId} currentWorldId={account.currentWorldId} onSwitched={loadAccounts} />
                                                <button
                                                    type="button"
                                                    onClick={() => copyId(account.userId)}
                                                    className="inline-flex items-center gap-1 rounded px-1 font-mono text-xs hover:bg-accent hover:text-foreground"
                                                    title={t('ACCOUNT_COPY_ID')}
                                                >
                                                    ID {account.userId}
                                                    <Copy className="h-3 w-3" />
                                                </button>
                                                {account.lastLoginTime && (
                                                    <span className="text-xs">{t('ACCOUNT_LAST_LOGIN', [formatLastLogin(account.lastLoginTime)])}</span>
                                                )}
                                            </div>
                                        </div>

                                        <div className="flex w-full items-center justify-end gap-1 sm:w-auto">
                                            <Button onClick={() => openAccount(account.userId, account.clientKey, account.isLoggedIn)} disabled={busy} className="min-w-28">
                                                {busy ? (
                                                    <Loader2 className="h-4 w-4 animate-spin" />
                                                ) : account.isLoggedIn ? (
                                                    <>{t('ACCOUNT_OPEN')}<ArrowRight className="ml-2 h-4 w-4" /></>
                                                ) : (
                                                    <><LogIn className="mr-2 h-4 w-4" />{t('ACCOUNT_LOGIN')}</>
                                                )}
                                            </Button>
                                            <DropdownMenu>
                                                <DropdownMenuTrigger asChild>
                                                    <Button variant="ghost" size="icon" aria-label={t('ACCOUNT_MORE')}>
                                                        <MoreVertical className="h-4 w-4" />
                                                    </Button>
                                                </DropdownMenuTrigger>
                                                <DropdownMenuContent align="end">
                                                    <DropdownMenuItem onClick={() => handleLogin(account.userId, account.clientKey)} disabled={busy}>
                                                        <RefreshCw className="mr-2 h-4 w-4" />
                                                        {t('ACCOUNT_RELOGIN')}
                                                    </DropdownMenuItem>
                                                    <DropdownMenuItem className="text-destructive" onClick={() => setDeleteUserId(account.userId)}>
                                                        <Trash2 className="mr-2 h-4 w-4" />
                                                        {t('ACCOUNT_REMOVE')}
                                                    </DropdownMenuItem>
                                                </DropdownMenuContent>
                                            </DropdownMenu>
                                        </div>
                                    </CardContent>
                                </Card>
                            </li>
                        );
                    })}
                </ul>
            )}

            <AddAccountDialog
                open={showAddDialog}
                onOpenChange={setShowAddDialog}
            />

            <AlertDialog open={!!deleteUserId} onOpenChange={() => setDeleteUserId(null)}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{t('ACCOUNT_CONFIRM_DELETE')}</AlertDialogTitle>
                        <AlertDialogDescription>
                            {t('ACCOUNT_CONFIRM_DELETE_DESC')}
                        </AlertDialogDescription>
                    </AlertDialogHeader>
                    <AlertDialogFooter>
                        <AlertDialogCancel>{t('[CommonCancelLabel]')}</AlertDialogCancel>
                        <AlertDialogAction onClick={handleDelete}>{t('[AccountDeleteTitle]')}</AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>
        </div>
    );
}
