import { useState } from 'react';
import { accountApi } from '@/api/account-service';
import { useAccountStore } from '@/store/accountStore';
import { Button } from '@/components/ui/button';
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from '@/components/ui/card';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { useToast } from '@/hooks/use-toast';
import { Loader2, Plus } from 'lucide-react';
import { useTranslation } from '@/hooks/useTranslation';

export function AddAccountDialog({
    open,
    onOpenChange,
}: {
    open: boolean;
    onOpenChange: (open: boolean) => void;
}) {
    const [method, setMethod] = useState<'clientkey' | 'password'>('password');
    const [name, setName] = useState('');
    const [userId, setUserId] = useState('');
    const [clientKey, setClientKey] = useState('');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const { addAccount } = useAccountStore();
    const { toast } = useToast();
    const { t } = useTranslation();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!name || !userId) {
            toast({
                title: t('ACCOUNT_ERROR'),
                description: t('ACCOUNT_FILL_REQUIRED_FIELDS'),
                variant: 'destructive',
            });
            return;
        }

        setIsLoading(true);

        try {
            if (method === 'clientkey') {
                if (!clientKey) {
                    toast({
                        title: t('ACCOUNT_ERROR'),
                        description: t('ACCOUNT_ENTER_CLIENT_KEY'),
                        variant: 'destructive',
                    });
                    return;
                }

                const account = await accountApi.addAccountWithClientKey({
                    name,
                    userId: parseInt(userId),
                    clientKey,
                });

                addAccount(account);
                toast({
                    title: t('ACCOUNT_SUCCESS'),
                    description: t('ACCOUNT_ADD_SUCCESS'),
                });
            } else {
                if (!password) {
                    toast({
                        title: t('ACCOUNT_ERROR'),
                        description: t('ACCOUNT_ENTER_PASSWORD'),
                        variant: 'destructive',
                    });
                    return;
                }

                const result = await accountApi.addAccountWithPassword({
                    name,
                    userId: parseInt(userId),
                    password,
                });

                if (result.success && result.clientKey) {
                    toast({
                        title: t('ACCOUNT_SUCCESS'),
                        description: t('ACCOUNT_ADD_SUCCESS'),
                    });

                    const accounts = await accountApi.getAccounts();
                    useAccountStore.getState().setAccounts(accounts);
                } else {
                    toast({
                        title: t('ACCOUNT_FAILED'),
                        description: result.message || t('ACCOUNT_GET_CLIENT_KEY_FAILED'),
                        variant: 'destructive',
                    });
                }
            }

            onOpenChange(false);
            setName('');
            setUserId('');
            setClientKey('');
            setPassword('');
        } catch (error) {
            console.error('Failed to add account:', error);
            toast({
                title: t('ACCOUNT_ERROR'),
                description: error instanceof Error ? error.message : t('ACCOUNT_ADD_FAILED'),
                variant: 'destructive',
            });
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="sm:max-w-[500px]">
                <DialogHeader>
                    <DialogTitle>{t('ACCOUNT_ADD_ACCOUNT')}</DialogTitle>
                    <DialogDescription>
                        {t('ACCOUNT_ADD_ACCOUNT_DESC')}
                    </DialogDescription>
                </DialogHeader>

                <form onSubmit={handleSubmit}>
                    <Tabs value={method} onValueChange={(v) => setMethod(v as any)}>
                        <TabsList className="grid w-full grid-cols-2">
                            <TabsTrigger value="password">{t('ACCOUNT_USE_PASSWORD')}</TabsTrigger>
                            <TabsTrigger value="clientkey">{t('ACCOUNT_USE_CLIENT_KEY')}</TabsTrigger>
                        </TabsList>

                        <TabsContent value="password" className="space-y-4 mt-4">
                            <div className="space-y-2">
                                <Label htmlFor="name-pwd">{t('ACCOUNT_NAME')}</Label>
                                <Input
                                    id="name-pwd"
                                    placeholder={t('ACCOUNT_NAME_PLACEHOLDER')}
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="space-y-2">
                                <Label htmlFor="userid-pwd">{t('ACCOUNT_USER_ID')}</Label>
                                <Input
                                    id="userid-pwd"
                                    type="number"
                                    placeholder={t('ACCOUNT_USER_ID_PLACEHOLDER')}
                                    value={userId}
                                    onChange={(e) => setUserId(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="space-y-2">
                                <Label htmlFor="password">{t('ACCOUNT_PASSWORD_LABEL')}</Label>
                                <Input
                                    id="password"
                                    type="password"
                                    placeholder={t('ACCOUNT_PASSWORD_PLACEHOLDER')}
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                />
                                <p className="text-sm text-muted-foreground">
                                    {t('ACCOUNT_AUTO_GET_CLIENT_KEY')}
                                </p>
                            </div>
                        </TabsContent>

                        <TabsContent value="clientkey" className="space-y-4 mt-4">
                            <div className="space-y-2">
                                <Label htmlFor="name-ck">{t('ACCOUNT_NAME')}</Label>
                                <Input
                                    id="name-ck"
                                    placeholder={t('ACCOUNT_NAME_PLACEHOLDER')}
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="space-y-2">
                                <Label htmlFor="userid-ck">{t('ACCOUNT_USER_ID')}</Label>
                                <Input
                                    id="userid-ck"
                                    type="number"
                                    placeholder={t('ACCOUNT_USER_ID_PLACEHOLDER')}
                                    value={userId}
                                    onChange={(e) => setUserId(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="space-y-2">
                                <Label htmlFor="clientkey">ClientKey</Label>
                                <Input
                                    id="clientkey"
                                    type="password"
                                    placeholder={t('ACCOUNT_CLIENT_KEY_PLACEHOLDER')}
                                    value={clientKey}
                                    onChange={(e) => setClientKey(e.target.value)}
                                    required
                                />
                            </div>
                        </TabsContent>
                    </Tabs>

                    <DialogFooter className="mt-6">
                        <Button
                            type="button"
                            variant="outline"
                            onClick={() => onOpenChange(false)}
                            disabled={isLoading}
                        >
                            {t('[CommonCancelLabel]')}
                        </Button>
                        <Button type="submit" disabled={isLoading}>
                            {isLoading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                            {t('ACCOUNT_ADD_ACCOUNT')}
                        </Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    );
}
