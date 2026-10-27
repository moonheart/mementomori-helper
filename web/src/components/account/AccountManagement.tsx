import { useEffect, useState } from 'react';
import { accountApi } from '@/api/account-service';
import { useAccountStore } from '@/store/accountStore';
import { AddAccountDialog } from '@/components/account/AddAccountDialog';
import { Button } from '@/components/ui/button';
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from '@/components/ui/card';
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
import { Loader2, MoreVertical, Plus, Trash2, LogIn, ArrowRight } from 'lucide-react';
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
        return date.toLocaleString(dateTimeLocales[currentLanguage] ?? 'en-US');
    };

    return (
        <div className="container mx-auto py-6 space-y-6">
            <Card>
                <CardHeader>
                    <div className="flex items-center justify-between">
                        <div>
                            <CardTitle>{t('ACCOUNT_MANAGEMENT_TITLE')}</CardTitle>
                            <CardDescription>
                                {t('ACCOUNT_MANAGEMENT_DESC')}
                            </CardDescription>
                        </div>
                        <Button onClick={() => setShowAddDialog(true)}>
                            <Plus className="mr-2 h-4 w-4" />
                            {t('ACCOUNT_ADD_ACCOUNT')}
                        </Button>
                    </div>
                </CardHeader>
                <CardContent>
                    {accounts.length === 0 ? (
                        <div className="text-center py-12">
                            <p className="text-muted-foreground mb-4">{t('ACCOUNT_NO_ACCOUNTS')}</p>
                            <Button onClick={() => setShowAddDialog(true)} variant="outline">
                                <Plus className="mr-2 h-4 w-4" />
                                {t('ACCOUNT_ADD_FIRST')}
                            </Button>
                        </div>
                    ) : (
                        <div className="space-y-2">
                            {accounts.map((account) => (
                                <div
                                    key={account.userId}
                                    className="flex items-center justify-between p-4 border rounded-lg hover:bg-accent transition-colors"
                                >
                                    <div className="flex items-center gap-4">
                                        <div>
                                            <div className="flex items-center gap-2">
                                                <span className="font-medium">{account.name}</span>
                                                {account.isLoggedIn && (
                                                    <Badge variant="default" className="bg-green-600">{t('[Online]')}</Badge>
                                                )}
                                            </div>
                                            <div className="flex flex-wrap items-center gap-2 text-sm text-muted-foreground">
                                                ID: {account.userId}
                                                <WorldSelect userId={account.userId} currentWorldId={account.currentWorldId} onSwitched={loadAccounts} />
                                            </div>
                                            {account.lastLoginTime && (
                                                <div className="text-xs text-muted-foreground">
                                                    {t('[GuildPlayerListSortItemTypeLastAccess]')}: {formatLastLogin(account.lastLoginTime)}
                                                </div>
                                            )}
                                        </div>
                                        {currentAccountId === account.userId && (
                                            <Badge variant="outline">{t('ACCOUNT_CURRENT')}</Badge>
                                        )}
                                    </div>

                                    <div className="flex items-center gap-2">
                                        <Button
                                            size="sm"
                                            onClick={() => handleEnterAccount(account.userId)}
                                        >
                                            <ArrowRight className="mr-2 h-4 w-4" />
                                            {t('ACCOUNT_ENTER_ACCOUNT')}
                                        </Button>

                                        <Button
                                            size="sm"
                                            variant="outline"
                                            onClick={() => handleLogin(account.userId, account.clientKey)}
                                            disabled={loginLoading === account.userId}
                                        >
                                            {loginLoading === account.userId ? (
                                                <Loader2 className="h-4 w-4 animate-spin" />
                                            ) : (
                                                <>
                                                    <LogIn className="mr-2 h-4 w-4" />
                                                    {t('ACCOUNT_LOGIN')}
                                                </>
                                            )}
                                        </Button>

                                        <DropdownMenu>
                                            <DropdownMenuTrigger asChild>
                                                <Button variant="ghost" size="icon">
                                                    <MoreVertical className="h-4 w-4" />
                                                </Button>
                                            </DropdownMenuTrigger>
                                            <DropdownMenuContent align="end">
                                                <DropdownMenuItem
                                                    className="text-destructive"
                                                    onClick={() => setDeleteUserId(account.userId)}
                                                >
                                                    <Trash2 className="mr-2 h-4 w-4" />
                                                    {t('[AccountDeleteTitle]')}
                                                </DropdownMenuItem>
                                            </DropdownMenuContent>
                                        </DropdownMenu>
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </CardContent>
            </Card>

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
