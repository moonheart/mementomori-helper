import { useState, useEffect, useCallback } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
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
import { Separator } from '@/components/ui/separator';
import { Slider } from '@/components/ui/slider';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from '@/components/ui/dialog';
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from '@/components/ui/select';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import {
    Gem,
    Coins,
    Heart,
    Trophy,
    Clock,
    Tag,
    Loader2,
    AlarmClock
} from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { settingsApi } from '@/api/settings-service';
import { useTranslation } from '@/hooks/useTranslation';
import { useToast } from '@/hooks/use-toast';
import { useAccountStore } from '@/store/accountStore';
import { useMasterStore } from '@/store/masterStore';
import { TradeShopTabInfo } from '@/api/generated/tradeShopTabInfo';
import { ItemType } from '@/api/generated/itemType';
import { TradeShopItem } from '@/api/generated/tradeShopItem';
import { TradeShopTabMB } from '@/api/generated/tradeShopTabMB';
import { TradeShopType } from '@/api/generated/tradeShopType';
import { SphereMB } from '@/api/generated/sphereMB';
import { TradeShopSphereMB } from '@/api/generated/tradeShopSphereMB';
import { UserItemDtoInfo } from '@/api/generated/userItemDtoInfo';
import { ShopConfig } from '@/api/generated/shopConfig';
import { ShopAutoBuyItem } from '@/api/generated/shopAutoBuyItem';
import { UserItem } from '@/api/generated/userItem';
import { useItemName } from '@/hooks/useItemName';
import { LegendLeagueIconShop, StarsGuidanceShop } from '@/components/shop/SpendShops';
import { getUserItemCount } from '@/lib/itemUtils';

export function ShopPage() {
    const { t } = useTranslation();
    const { toast } = useToast();
    const currentAccount = useAccountStore(state => state.accounts.find(a => a.userId === state.currentAccountId));
    const getTable = useMasterStore(state => state.getTable);
    const { getItemName } = useItemName();

    const [loading, setLoading] = useState(false);
    const [tradeShopTabMasterMap, setTradeShopTabMasterMap] = useState<Record<number, TradeShopTabMB>>({});
    const [weeklyShopTabs, setWeeklyShopTabs] = useState<TradeShopTabInfo[]>([]);
    const [tradeShopTabs, setTradeShopTabs] = useState<TradeShopTabInfo[]>([]);
    const [userItems, setUserItems] = useState<UserItemDtoInfo[]>([]);
    const [sphereList, setSphereList] = useState<SphereMB[]>([]);
    const [tradeShopSphereMap, setTradeShopSphereMap] = useState<Record<number, TradeShopSphereMB>>({});

    // 符石购买状态
    const [sphereBuyState, setSphereBuyState] = useState<Record<number, { lv: number; count: number }>>({});

    // 自动购买配置
    const [shopConfig, setShopConfig] = useState<ShopConfig>(() => ({ ...new ShopConfig() }));

    // 自动购买对话框状态
    const [autoBuyDialogOpen, setAutoBuyDialogOpen] = useState(false);
    const [currentAutoBuyTabId, setCurrentAutoBuyTabId] = useState<number>(0);
    const [currentAutoBuyTabItems, setCurrentAutoBuyTabItems] = useState<TradeShopItem[]>([]);
    const [selectedAutoBuyItem, setSelectedAutoBuyItem] = useState<string>(''); // "itemType-itemId" 格式
    const [selectedConsumeItem, setSelectedConsumeItem] = useState<string>(''); // "itemType-itemId" 格式
    const [autoBuyDiscount, setAutoBuyDiscount] = useState<number>(0);
    const [autoBuyConsumeCount, setAutoBuyConsumeCount] = useState<number>(0);
    const [nowMs, setNowMs] = useState(() => Date.now());

    // 购买二次确认对话框状态
    const [confirmDialogOpen, setConfirmDialogOpen] = useState(false);
    const [confirmDialogTitle, setConfirmDialogTitle] = useState(t('[PurchaseConfirmationTitle]'));
    const [confirmDialogDescription, setConfirmDialogDescription] = useState(t('[PurchaseConfirmationMessage]'));
    const [confirmDialogActionLabel, setConfirmDialogActionLabel] = useState(t('[PurchaseConfirmationYesButton]'));
    const [confirmDialogItemLabel, setConfirmDialogItemLabel] = useState('');
    const [confirmDialogConsumeItems, setConfirmDialogConsumeItems] = useState<Array<{ name: string; required: number; owned: number }>>([]);
    const [confirmDialogOwnedCount, setConfirmDialogOwnedCount] = useState<number | null>(null);
    const [confirmAction, setConfirmAction] = useState<(() => Promise<void>) | null>(null);



    const loadData = useCallback(async () => {
        if (!currentAccount) return;
        setLoading(true);
        try {
            const [tradeShopRes, userRes, weeklyRes, shopData] = await Promise.all([
                ortegaApi.tradeShop.getList({}),
                ortegaApi.user.getUserData({}),
                ortegaApi.weeklyTopics.getWeeklyTopicsInfo({}),
                settingsApi.getSetting<ShopConfig>(currentAccount.userId, 'shop')
            ]);

            // 加载商店自动购买配置
            if (shopData) {
                setShopConfig(shopData);
            }

            setTradeShopTabs(tradeShopRes.tradeShopTabInfoList || []);

            // 加载 Master 数据
            const [tradeShopTabTable, sphereTable, tradeShopSphereTable] = await Promise.all([
                getTable<TradeShopTabMB>('TradeShopTabTable'),
                getTable<SphereMB>('SphereTable'),
                getTable<TradeShopSphereMB>('TradeShopSphereTable')
            ]);

            const masterMap: Record<number, TradeShopTabMB> = {};
            tradeShopTabTable.forEach((m) => {
                masterMap[m.id] = m;
            });
            setTradeShopTabMasterMap(masterMap);

            // 设置符石列表
            setSphereList(sphereTable);

            // 设置符石商店等级映射
            const sphereMap: Record<number, TradeShopSphereMB> = {};
            tradeShopSphereTable.forEach((m) => {
                sphereMap[m.sphereLevel] = m;
            });
            setTradeShopSphereMap(sphereMap);

            // 处理 WeeklyTopics 中的商店数据
            if (weeklyRes.shopData?.tradeShopItemList) {
                const groups: Record<number, TradeShopItem[]> = {};
                weeklyRes.shopData.tradeShopItemList.forEach(item => {
                    const tabId = Math.floor(item.tradeShopItemId / 10000);
                    if (!groups[tabId]) groups[tabId] = [];
                    groups[tabId].push(item);
                });

                const weeklyTabs = Object.entries(groups).map(([tabId, items]) => ({
                    tradeShopTabId: parseInt(tabId),
                    tradeShopItems: items,
                    expirationTimeStamp: weeklyRes.shopData.expirationTimeStamp,
                    lastFreeManualUpdateTime: 0
                } as TradeShopTabInfo));
                setWeeklyShopTabs(weeklyTabs);
            }

            const syncData = userRes.userSyncData;
            if (syncData) {
                const items = syncData.userItemDtoInfo || [];
                setUserItems(items);
            }
        } catch (error) {
            console.error('Failed to load shop data:', error);
            toast({
                title: t('BATTLE_LOAD_FAILED'),
                description: t('SHOP_LOAD_FAILED_DESC'),
                variant: 'destructive',
            });
        } finally {
            setLoading(false);
        }
    }, [currentAccount, toast]);

    useEffect(() => {
        if (currentAccount) {
            loadData();
        }
    }, [loadData, currentAccount]);

    useEffect(() => {
        const timer = window.setInterval(() => {
            setNowMs(Date.now());
        }, 1000);

        return () => window.clearInterval(timer);
    }, []);

    // 保存自动购买配置到服务器
    const saveShopConfig = async (newConfig: ShopConfig) => {
        if (!currentAccount) return;
        try {
            await settingsApi.saveSetting(currentAccount.userId, 'shop', newConfig);
            setShopConfig(newConfig);
        } catch (error) {
            console.error('Failed to save shop config:', error);
            toast({
                title: t('SHOP_SAVE_FAILED'),
                description: t('SHOP_SAVE_FAILED_DESC'),
                variant: 'destructive',
            });
        }
    };

    // 打开自动购买对话框
    const showAutoBuyDialog = (tabId: number, items: TradeShopItem[], defaultItem?: TradeShopItem) => {
        setCurrentAutoBuyTabId(tabId);
        // 去重：相同 giveItem (itemType + itemId + itemCount) 只保留一个
        const uniqueItems = items.filter((item, index, arr) =>
            index === arr.findIndex(i =>
                i.giveItem.itemType === item.giveItem.itemType &&
                i.giveItem.itemId === item.giveItem.itemId &&
                i.giveItem.itemCount === item.giveItem.itemCount
            )
        );
        setCurrentAutoBuyTabItems(uniqueItems);

        if (defaultItem) {
            setSelectedAutoBuyItem(`${defaultItem.giveItem.itemType}-${defaultItem.giveItem.itemId}-${defaultItem.giveItem.itemCount}`);
            if (defaultItem.consumeItem1) {
                setSelectedConsumeItem(`${defaultItem.consumeItem1.itemType}-${defaultItem.consumeItem1.itemId}`);
                setAutoBuyConsumeCount(defaultItem.consumeItem1.itemCount);
            }
        } else {
            setSelectedAutoBuyItem('');
            setSelectedConsumeItem('');
            setAutoBuyConsumeCount(0);
        }
        setAutoBuyDiscount(0);
        setAutoBuyDialogOpen(true);
    };

    // 确认自动购买
    const confirmAutoBuy = async () => {
        if (!selectedAutoBuyItem && !selectedConsumeItem) {
            toast({
                title: t('SHOP_SETTING_ERROR'),
                description: t('SHOP_SETTING_ERROR_DESC'),
                variant: 'destructive',
            });
            return;
        }

        // 构建购买物品
        let buyItem: UserItem | undefined;
        if (selectedAutoBuyItem) {
            const [type, id, count] = selectedAutoBuyItem.split('-');
            buyItem = new UserItem();
            buyItem.itemType = parseInt(type);
            buyItem.itemId = parseInt(id);
            buyItem.itemCount = parseInt(count);
        }

        // 构建消耗物品
        let consumeItem: UserItem | undefined;
        if (selectedConsumeItem) {
            const [type, id] = selectedConsumeItem.split('-');
            consumeItem = new UserItem();
            consumeItem.itemType = parseInt(type);
            consumeItem.itemId = parseInt(id);
            consumeItem.itemCount = autoBuyConsumeCount;
        }

        const newAutoBuyItem = new ShopAutoBuyItem();
        newAutoBuyItem.shopTabId = currentAutoBuyTabId;
        newAutoBuyItem.buyItem = buyItem!;
        newAutoBuyItem.consumeItem = consumeItem!;
        newAutoBuyItem.minDiscountPercent = autoBuyDiscount;

        // 检查是否已存在相同规则（同商店Tab + 同购买物品/同消耗物品）
        const existingIndex = shopConfig.autoBuyItems.findIndex(item => {
            const sameTab = item.shopTabId === currentAutoBuyTabId;
            if (!sameTab) return false;

            const existingBuyItemType = item.buyItem?.itemType;
            const existingBuyItemId = item.buyItem?.itemId;
            const existingBuyItemCount = item.buyItem?.itemCount;
            const existingConsumeItemType = item.consumeItem?.itemType;
            const existingConsumeItemId = item.consumeItem?.itemId;
            const newBuyItemType = buyItem?.itemType;
            const newBuyItemId = buyItem?.itemId;
            const newBuyItemCount = buyItem?.itemCount;
            const newConsumeItemType = consumeItem?.itemType;
            const newConsumeItemId = consumeItem?.itemId;

            // 如果都不选购买物品，则比较消耗物品
            if (!newBuyItemType && !existingBuyItemType) {
                return existingConsumeItemType === newConsumeItemType && existingConsumeItemId === newConsumeItemId;
            }
            // 如果都不选消耗物品，则比较购买物品
            if (!newConsumeItemType && !existingConsumeItemType) {
                return existingBuyItemType === newBuyItemType && existingBuyItemId === newBuyItemId;
            }
            // 否则比较两者
            return existingBuyItemType === newBuyItemType && existingBuyItemId === newBuyItemId && existingBuyItemCount === newBuyItemCount &&
                   existingConsumeItemType === newConsumeItemType && existingConsumeItemId === newConsumeItemId;
        });

        let newAutoBuyItems: ShopAutoBuyItem[];
        if (existingIndex >= 0) {
            // 更新现有规则
            newAutoBuyItems = [...shopConfig.autoBuyItems];
            newAutoBuyItems[existingIndex] = newAutoBuyItem;
        } else {
            // 添加新规则
            newAutoBuyItems = [...shopConfig.autoBuyItems, newAutoBuyItem];
        }

        const newConfig = new ShopConfig();
        newConfig.autoBuyItems = newAutoBuyItems;
        await saveShopConfig(newConfig);

        toast({
            title: t('SHOP_SETTING_SAVED'),
            description: t('SHOP_SETTING_SAVED_DESC'),
        });
        setAutoBuyDialogOpen(false);
    };

    const openPurchaseConfirm = (
        title: string,
        description: string,
        actionLabel: string,
        itemLabel: string,
        consumeItems: Array<{ name: string; required: number; owned: number }>,
        ownedCount: number | null,
        action: () => Promise<void>
    ) => {
        setConfirmDialogTitle(title);
        setConfirmDialogDescription(description);
        setConfirmDialogActionLabel(actionLabel);
        setConfirmDialogItemLabel(itemLabel);
        setConfirmDialogConsumeItems(consumeItems);
        setConfirmDialogOwnedCount(ownedCount);
        setConfirmAction(() => action);
        setConfirmDialogOpen(true);
    };

    const handleConfirmPurchase = async () => {
        if (!confirmAction) return;
        const action = confirmAction;
        setConfirmDialogOpen(false);
        setConfirmAction(null);
        await action();
    };



    const handleBuyTradeItem = async (tabId: number, item: TradeShopItem) => {
        try {
            await ortegaApi.tradeShop.buyItem({
                tradeShopTabId: tabId,
                tradeShopItemInfos: [{
                    tradeShopItemId: item.tradeShopItemId,
                    dedicatedItemId: item.dedicatedItemId,
                    tradeCount: 1
                }],
                tradeShopSphereId: 0,
                tradeSphereCount: 0
            });

            toast({
                title: t('SHOP_BUY_SUCCESS'),
                description: t('SHOP_OBTAINED', [getItemName(item.giveItem.itemType, item.giveItem.itemId)]),
            });

            // 重新加载数据以更新余额和库存
            loadData();
        } catch (error) {
            console.error('Buy item failed:', error);
            toast({
                title: t('SHOP_BUY_FAILED'),
                description: t('SHOP_BUY_FAILED_DESC'),
                variant: 'destructive',
            });
        }
    };

    // 获取符石购买状态
    const getSphereBuyState = (sphereId: number) => {
        return sphereBuyState[sphereId] || { lv: 1, count: 1 };
    };

    // 更新符石购买状态
    const updateSphereBuyState = (sphereId: number, updates: Partial<{ lv: number; count: number }>) => {
        setSphereBuyState(prev => ({
            ...prev,
            [sphereId]: { ...getSphereBuyState(sphereId), ...updates }
        }));
    };

    // 计算符石最大可购买数量
    const getMaxSphereBuyCount = (sphere: SphereMB, lv: number) => {
        const tradeShopSphereMb = tradeShopSphereMap[lv];
        if (!tradeShopSphereMb) return 0;

        let canBuyCount = 0;

        // 检查消耗物品1
        if (tradeShopSphereMb.consumeItem1) {
            const availableCount = getUserItemCount(
                userItems,
                tradeShopSphereMb.consumeItem1.itemType,
                tradeShopSphereMb.consumeItem1.itemId,
                tradeShopSphereMb.consumeItem1.itemType === ItemType.CurrencyFree || tradeShopSphereMb.consumeItem1.itemType === ItemType.CurrencyPaid
            );
            canBuyCount = Math.floor(availableCount / tradeShopSphereMb.consumeItem1.itemCount);
        }

        // 检查消耗物品2
        if (tradeShopSphereMb.consumeItem2 && canBuyCount > 0) {
            const availableCount = getUserItemCount(
                userItems,
                tradeShopSphereMb.consumeItem2.itemType,
                tradeShopSphereMb.consumeItem2.itemId,
                tradeShopSphereMb.consumeItem2.itemType === ItemType.CurrencyFree || tradeShopSphereMb.consumeItem2.itemType === ItemType.CurrencyPaid
            );
            const canBuyCount2 = Math.floor(availableCount / tradeShopSphereMb.consumeItem2.itemCount);
            canBuyCount = Math.min(canBuyCount, canBuyCount2);
        }

        return canBuyCount;
    };

    // 检查符石是否可以购买
    const canBuySphere = (lv: number) => {
        const tradeShopSphereMb = tradeShopSphereMap[lv];
        if (!tradeShopSphereMb) return false;

        // 检查消耗物品1
        if (tradeShopSphereMb.consumeItem1) {
            const availableCount = getUserItemCount(
                userItems,
                tradeShopSphereMb.consumeItem1.itemType,
                tradeShopSphereMb.consumeItem1.itemId,
                tradeShopSphereMb.consumeItem1.itemType === ItemType.CurrencyFree || tradeShopSphereMb.consumeItem1.itemType === ItemType.CurrencyPaid
            );
            if (availableCount < tradeShopSphereMb.consumeItem1.itemCount) return false;
        }

        // 检查消耗物品2
        if (tradeShopSphereMb.consumeItem2) {
            const availableCount = getUserItemCount(
                userItems,
                tradeShopSphereMb.consumeItem2.itemType,
                tradeShopSphereMb.consumeItem2.itemId,
                tradeShopSphereMb.consumeItem2.itemType === ItemType.CurrencyFree || tradeShopSphereMb.consumeItem2.itemType === ItemType.CurrencyPaid
            );
            if (availableCount < tradeShopSphereMb.consumeItem2.itemCount) return false;
        }

        return true;
    };

    // 获取符石购买消耗文本
    const getSphereConsumeText = (lv: number, count: number) => {
        const tradeShopSphereMb = tradeShopSphereMap[lv];
        if (!tradeShopSphereMb) return '';

        let text = '';
        if (tradeShopSphereMb.consumeItem1) {
            const name = getItemName(tradeShopSphereMb.consumeItem1.itemType, tradeShopSphereMb.consumeItem1.itemId);
            const totalCount = tradeShopSphereMb.consumeItem1.itemCount * count;
            text = `${name} × ${totalCount.toLocaleString()}`;
        }

        if (tradeShopSphereMb.consumeItem2) {
            const name = getItemName(tradeShopSphereMb.consumeItem2.itemType, tradeShopSphereMb.consumeItem2.itemId);
            const totalCount = tradeShopSphereMb.consumeItem2.itemCount * count;
            text += ` ${name} × ${totalCount.toLocaleString()}`;
        }

        return text;
    };

    const getTradeItemConsumeItems = (item: TradeShopItem) => {
        const items: Array<{ name: string; required: number; owned: number }> = [];

        if (item.consumeItem1) {
            const owned = getUserItemCount(
                userItems,
                item.consumeItem1.itemType,
                item.consumeItem1.itemId,
                item.consumeItem1.itemType === ItemType.CurrencyFree || item.consumeItem1.itemType === ItemType.CurrencyPaid
            );
            items.push({
                name: getItemName(item.consumeItem1.itemType, item.consumeItem1.itemId),
                required: item.consumeItem1.itemCount,
                owned
            });
        }

        if (item.consumeItem2) {
            const owned = getUserItemCount(
                userItems,
                item.consumeItem2.itemType,
                item.consumeItem2.itemId,
                item.consumeItem2.itemType === ItemType.CurrencyFree || item.consumeItem2.itemType === ItemType.CurrencyPaid
            );
            items.push({
                name: getItemName(item.consumeItem2.itemType, item.consumeItem2.itemId),
                required: item.consumeItem2.itemCount,
                owned
            });
        }

        return items;
    };

    const getSphereConsumeItems = (lv: number, count: number) => {
        const tradeShopSphereMb = tradeShopSphereMap[lv];
        if (!tradeShopSphereMb) return [];

        const items: Array<{ name: string; required: number; owned: number }> = [];

        if (tradeShopSphereMb.consumeItem1) {
            const required = tradeShopSphereMb.consumeItem1.itemCount * count;
            const owned = getUserItemCount(
                userItems,
                tradeShopSphereMb.consumeItem1.itemType,
                tradeShopSphereMb.consumeItem1.itemId,
                tradeShopSphereMb.consumeItem1.itemType === ItemType.CurrencyFree || tradeShopSphereMb.consumeItem1.itemType === ItemType.CurrencyPaid
            );
            items.push({
                name: getItemName(tradeShopSphereMb.consumeItem1.itemType, tradeShopSphereMb.consumeItem1.itemId),
                required,
                owned
            });
        }

        if (tradeShopSphereMb.consumeItem2) {
            const required = tradeShopSphereMb.consumeItem2.itemCount * count;
            const owned = getUserItemCount(
                userItems,
                tradeShopSphereMb.consumeItem2.itemType,
                tradeShopSphereMb.consumeItem2.itemId,
                tradeShopSphereMb.consumeItem2.itemType === ItemType.CurrencyFree || tradeShopSphereMb.consumeItem2.itemType === ItemType.CurrencyPaid
            );
            items.push({
                name: getItemName(tradeShopSphereMb.consumeItem2.itemType, tradeShopSphereMb.consumeItem2.itemId),
                required,
                owned
            });
        }

        return items;
    };

    const formatRemainTime = (expirationTimeStamp: number) => {
        const remainSeconds = Math.max(0, Math.floor((expirationTimeStamp - nowMs) / 1000));
        const days = Math.floor(remainSeconds / 86400);
        const hours = Math.floor((remainSeconds % 86400) / 3600);
        const minutes = Math.floor((remainSeconds % 3600) / 60);
        const seconds = remainSeconds % 60;

        if (days > 0) {
            return t('[CommonRemainTimeFull]', [days, hours, minutes, seconds]);
        }

        return t('[CommonTimeFormatOnlyTime]', [hours, minutes, seconds]);
    };

    const getOwnedItemCount = (itemType: ItemType, itemId: number) => {
        return getUserItemCount(
            userItems,
            itemType,
            itemId,
            itemType === ItemType.CurrencyFree || itemType === ItemType.CurrencyPaid
        );
    };

    // 检查普通商品是否可以购买
    const canBuyTradeItem = (item: TradeShopItem) => {
        // 检查库存限制
        if (item.limitTradeCount > 0 && item.tradeCount >= item.limitTradeCount) {
            return false;
        }

        // 检查消耗道具1
        if (item.consumeItem1) {
            const availableCount = getUserItemCount(
                userItems,
                item.consumeItem1.itemType,
                item.consumeItem1.itemId,
                item.consumeItem1.itemType === ItemType.CurrencyFree || item.consumeItem1.itemType === ItemType.CurrencyPaid
            );
            if (availableCount < item.consumeItem1.itemCount) return false;
        }

        // 检查消耗道具2
        if (item.consumeItem2) {
            const availableCount = getUserItemCount(
                userItems,
                item.consumeItem2.itemType,
                item.consumeItem2.itemId,
                item.consumeItem2.itemType === ItemType.CurrencyFree || item.consumeItem2.itemType === ItemType.CurrencyPaid
            );
            if (availableCount < item.consumeItem2.itemCount) return false;
        }

        return true;
    };

    // 处理符石购买
    const handleBuySphere = async (tabId: number, sphere: SphereMB) => {
        const state = getSphereBuyState(sphere.id);
        const targetSphere = sphereList.find(s => s.categoryId === sphere.categoryId && s.lv === state.lv);

        if (!targetSphere) {
            toast({
                title: t('SHOP_BUY_FAILED'),
                description: t('SHOP_SPHERE_NOT_FOUND'),
                variant: 'destructive',
            });
            return;
        }

        try {
            await ortegaApi.tradeShop.buyItem({
                tradeShopTabId: tabId,
                tradeShopItemInfos: [],
                tradeShopSphereId: targetSphere.id,
                tradeSphereCount: state.count
            });

            toast({
                title: t('SHOP_BUY_SUCCESS'),
                description: t('SHOP_OBTAINED_COUNT', [getItemName(ItemType.Sphere, targetSphere.id), state.count]),
            });

            // 重新加载数据以更新余额
            loadData();
        } catch (error) {
            console.error('Buy sphere failed:', error);
            toast({
                title: t('SHOP_BUY_FAILED'),
                description: t('SHOP_BUY_FAILED_DESC'),
                variant: 'destructive',
            });
        }
    };

    const getCurrencyIcon = (itemType: ItemType) => {
        switch (itemType) {
            case ItemType.Gold:
                return <Coins className="h-4 w-4 text-yellow-600" />;
            case ItemType.FriendPoint:
                return <Heart className="h-4 w-4 text-pink-500" />;
            case ItemType.CurrencyFree:
            case ItemType.CurrencyPaid:
                return <Gem className="h-4 w-4 text-cyan-500" />;
            case ItemType.ActivityMedal:
                return <Trophy className="h-4 w-4 text-orange-500" />;
            default:
                return <Tag className="h-4 w-4 text-muted-foreground" />;
        }
    };



    if (loading && tradeShopTabs.length === 0 && weeklyShopTabs.length === 0) {
        return (
            <div className="flex flex-col items-center justify-center min-h-[400px] space-y-4">
                <Loader2 className="h-8 w-8 animate-spin text-primary" />
                <p className="text-muted-foreground">{t('SHOP_LOADING')}</p>
            </div>
        );
    }

    return (
        <div className="space-y-6">
            <div className="flex justify-end">
                <Button variant="outline" size="sm" onClick={loadData} disabled={loading}>
                    <Clock className={`h-4 w-4 mr-2 ${loading ? 'animate-spin' : ''}`} />
                    {t('SHOP_REFRESH')}
                </Button>
            </div>

            <Tabs defaultValue={([...tradeShopTabs, ...weeklyShopTabs])[0]?.tradeShopTabId?.toString()} className="space-y-6">
                <div className="overflow-x-auto pb-2">
                    <TabsList className="inline-flex w-auto min-w-full justify-start">
                        {([...tradeShopTabs, ...weeklyShopTabs]).map((tab) => (
                            <TabsTrigger key={tab.tradeShopTabId} value={tab.tradeShopTabId.toString()} className="whitespace-nowrap">
                                {(() => {
                                    const mb = tradeShopTabMasterMap[tab.tradeShopTabId];
                                    return mb ? (t(mb.tabNameKey) || mb.memo) : t('SHOP_TAB_FALLBACK', [tab.tradeShopTabId]);
                                })()}
                            </TabsTrigger>
                        ))}
                        <TabsTrigger value="legendLeagueIcon" className="whitespace-nowrap">{t('LL_ICON_SHOP_TITLE')}</TabsTrigger>
                        <TabsTrigger value="starsGuidance" className="whitespace-nowrap">{t('SG_SHOP_TITLE')}</TabsTrigger>
                    </TabsList>
                </div>

                {([...tradeShopTabs, ...weeklyShopTabs]).map((tab) => {
                    const tabMaster = tradeShopTabMasterMap[tab.tradeShopTabId];
                    const consumeItems = tabMaster?.consumeItemInfos || [];
                    const isSphereShop = tabMaster?.tradeShopType === TradeShopType.Sphere;

                    return (
                    <TabsContent key={tab.tradeShopTabId} value={tab.tradeShopTabId.toString()} className="space-y-4">
                        <Card>
                            <CardHeader>
                                <div className="flex items-center justify-between">
                                    <div>
                                        <CardTitle>
                                            {(() => {
                                                const mb = tradeShopTabMasterMap[tab.tradeShopTabId];
                                                return mb ? (t(mb.tabNameKey) || mb.memo) : t('SHOP_TAB_FALLBACK', [tab.tradeShopTabId]);
                                            })()}
                                        </CardTitle>
                                        <CardDescription>
                                            {tab.expirationTimeStamp > 0
                                                ? `${t('[ExchangeExpirationTimeLabel]')} ${formatRemainTime(tab.expirationTimeStamp)}`
                                                : null
                                            }
                                        </CardDescription>
                                    </div>
                                    <div className="flex items-center gap-3">
                                        {/* 显示该 tab 涉及的消耗道具持有数量 */}
                                        {isSphereShop ? (
                                            // 符石商店显示所有等级的消耗道具
                                            <div className="flex items-center gap-2 bg-muted/50 rounded-lg px-3 py-1.5">
                                                                                                {[1, 2, 3, 4, 5, 6, 7, 8, 9].map((lv, idx) => {
                                                    const tradeShopSphereMb = tradeShopSphereMap[lv];
                                                    if (!tradeShopSphereMb || !tradeShopSphereMb.consumeItem1) return null;
                                                    
                                                    const consumeItem = tradeShopSphereMb.consumeItem1;
                                                    const count = getUserItemCount(userItems, consumeItem.itemType, consumeItem.itemId,
                                                        consumeItem.itemType === ItemType.CurrencyFree || consumeItem.itemType === ItemType.CurrencyPaid);
                                                    
                                                    // 去重：检查前面的等级是否已经显示过相同的道具
                                                    const isDuplicate = [1, 2, 3, 4, 5, 6, 7, 8, 9].slice(0, idx).some(prevLv => {
                                                        const prevMb = tradeShopSphereMap[prevLv];
                                                        return prevMb?.consumeItem1?.itemType === consumeItem.itemType && 
                                                               prevMb?.consumeItem1?.itemId === consumeItem.itemId;
                                                    });
                                                    if (isDuplicate) return null;
                                                    
                                                    return (
                                                        <div key={`sphere-${lv}`} className="flex items-center gap-1.5">
                                                            {idx > 0 && <Separator orientation="vertical" className="h-4 mx-1" />}
                                                            {getCurrencyIcon(consumeItem.itemType)}
                                                            <span className="text-xs text-muted-foreground truncate max-w-[80px]">
                                                                {getItemName(consumeItem.itemType, consumeItem.itemId)}
                                                            </span>
                                                            <span className="text-sm font-semibold">
                                                                {count.toLocaleString()}
                                                            </span>
                                                        </div>
                                                    );
                                                }).filter(Boolean)}
                                            </div>
                                        ) : consumeItems.length > 0 && (
                                            <div className="flex items-center gap-2 bg-muted/50 rounded-lg px-3 py-1.5">
                                                {consumeItems.map((consumeItem, idx) => {
                                                    const count = getUserItemCount(userItems, consumeItem.itemType, consumeItem.itemId,
                                                        consumeItem.itemType === ItemType.CurrencyFree || consumeItem.itemType === ItemType.CurrencyPaid);
                                                    return (
                                                        <div key={`${consumeItem.itemType}-${consumeItem.itemId}`} className="flex items-center gap-1.5">
                                                            {idx > 0 && <Separator orientation="vertical" className="h-4 mx-1" />}
                                                            {getCurrencyIcon(consumeItem.itemType)}
                                                            <span className="text-xs text-muted-foreground truncate max-w-[80px]">
                                                                {getItemName(consumeItem.itemType, consumeItem.itemId)}
                                                            </span>
                                                            <span className="text-sm font-semibold">
                                                                {count.toLocaleString()}
                                                            </span>
                                                        </div>
                                                    );
                                                })}
                                            </div>
                                        )}
                                    </div>
                                </div>
                            </CardHeader>
                            <CardContent>
                                {isSphereShop ? (
                                    // 符石商店特殊渲染
                                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                                        {sphereList.filter(s => s.lv === 1).map((sphere) => {
                                            const state = getSphereBuyState(sphere.id);
                                            const maxCount = getMaxSphereBuyCount(sphere, state.lv);
                                            const buyEnabled = canBuySphere(state.lv) && maxCount > 0;
                                            const targetSphereInView = sphereList.find(s => s.categoryId === sphere.categoryId && s.lv === state.lv);

                                            return (
                                                <Card key={sphere.id}>
                                                    <CardHeader className="pb-3">
                                                        <CardTitle className="text-base">{t(sphere.nameKey)}</CardTitle>
                                                    </CardHeader>
                                                    <CardContent className="space-y-4">
                                                        {/* 等级选择 */}
                                                        <div className="space-y-2">
                                                            <div className="flex justify-between text-sm">
                                                                <span className="text-muted-foreground">{t('[EquipmentSphereSynthesisSphereLv]')}</span>
                                                                <span className="font-semibold">Lv.{state.lv}</span>
                                                            </div>
                                                            <Slider
                                                                value={[state.lv]}
                                                                onValueChange={(values: number[]) => {
                                                                    const newLv = Math.max(1, Math.min(9, values[0] || 1));
                                                                    updateSphereBuyState(sphere.id, { lv: newLv });
                                                                    // 确保数量不超过最大值
                                                                    const newMax = getMaxSphereBuyCount(sphere, newLv);
                                                                    if (state.count > newMax && newMax > 0) {
                                                                        updateSphereBuyState(sphere.id, { count: newMax });
                                                                    }
                                                                }}
                                                                min={1}
                                                                max={9}
                                                                step={1}
                                                            />
                                                        </div>

                                                        {/* 数量选择 */}
                                                        <div className="space-y-2">
                                                            <div className="flex justify-between text-sm">
                                                                <span className="text-muted-foreground">{t('SHOP_BUY_COUNT')}</span>
                                                                <span className="font-semibold">{state.count} / {maxCount}</span>
                                                            </div>
                                                            <Slider
                                                                value={[state.count]}
                                                                onValueChange={(values: number[]) => updateSphereBuyState(sphere.id, { count: Math.max(1, Math.min(maxCount || 1, values[0] || 1)) })}
                                                                min={1}
                                                                max={maxCount || 1}
                                                                step={1}
                                                                disabled={maxCount === 0}
                                                            />
                                                        </div>

                                                        {/* 消耗显示 */}
                                                        <div className="text-sm text-muted-foreground">
                                                            {t('SHOP_COST', [getSphereConsumeText(state.lv, state.count)])}
                                                        </div>
                                                    </CardContent>
                                                    <CardContent className="pt-0">
                                                        <Button
                                                            className="w-full"
                                                            disabled={!buyEnabled}
                                                            onClick={() => openPurchaseConfirm(
                                                                t('[PurchaseConfirmationTitle]'),
                                                                t('[PurchaseConfirmationMessage]'),
                                                                t('[PurchaseConfirmationYesButton]'),
                                                                `${t(sphere.nameKey)} Lv.${state.lv} × ${state.count}`,
                                                                getSphereConsumeItems(state.lv, state.count),
                                                                getOwnedItemCount(ItemType.Sphere, targetSphereInView?.id ?? sphere.id),
                                                                () => handleBuySphere(tab.tradeShopTabId, sphere)
                                                            )}
                                                        >
                                                            {t('[PurchaseConfirmationYesButton]')}
                                                        </Button>
                                                    </CardContent>
                                                </Card>
                                            );
                                        })}
                                    </div>
                                ) : (
                                    // 普通商店渲染
                                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                                        {tab.tradeShopItems?.map((item) => (
                                            <Card key={`${item.tradeShopItemId}-${item.giveItem.itemType}-${item.giveItem.itemId}-${item.giveItem.itemCount}`}>
                                                <CardContent className="p-4">
                                                    <div className="flex items-center justify-between mb-3">
                                                        <div>
                                                            <div className="font-semibold">{getItemName(item.giveItem.itemType, item.giveItem.itemId)}</div>
                                                            <div className="text-sm text-muted-foreground">
                                                                ×{item.giveItem.itemCount}
                                                            </div>
                                                        </div>
                                                        <Badge variant={item.tradeCount >= item.limitTradeCount && item.limitTradeCount > 0 ? "destructive" : "secondary"}>
                                                            {item.limitTradeCount > 0 ? `${item.tradeCount}/${item.limitTradeCount}` : '∞'}
                                                        </Badge>
                                                    </div>
                                                    <div className="flex items-center justify-between">
                                                    <div className="flex flex-col gap-1">
                                                        {item.consumeItem1 ? (
                                                            <div className="flex items-center gap-2">
                                                                {getCurrencyIcon(item.consumeItem1.itemType)}
                                                                <span className="text-sm text-muted-foreground">
                                                                    {getItemName(item.consumeItem1.itemType, item.consumeItem1.itemId)}
                                                                </span>
                                                                <span className="font-bold">
                                                                    ×{item.consumeItem1.itemCount.toLocaleString()}
                                                                </span>
                                                            </div>
                                                        ) : (
                                                            <span className="font-bold text-green-600">{t('[ShopProductFreeLabel]')}</span>
                                                        )}
                                                        {item.consumeItem2 && (
                                                            <div className="flex items-center gap-2">
                                                                {getCurrencyIcon(item.consumeItem2.itemType)}
                                                                <span className="text-sm text-muted-foreground">
                                                                    {getItemName(item.consumeItem2.itemType, item.consumeItem2.itemId)}
                                                                </span>
                                                                <span className="font-bold">
                                                                    ×{item.consumeItem2.itemCount.toLocaleString()}
                                                                </span>
                                                            </div>
                                                        )}
                                                    </div>
                                                        <div className="flex items-center gap-2">
                                                            {(() => {
                                                                const canBuy = canBuyTradeItem(item);
                                                                const isSoldOut = item.limitTradeCount > 0 && item.tradeCount >= item.limitTradeCount;
                                                                return (
                                                                    <>
                                                                        <Button
                                                                            variant="outline"
                                                                            size="icon"
                                                                            className="h-8 w-8"
                                                                            onClick={() => showAutoBuyDialog(tab.tradeShopTabId, tab.tradeShopItems || [], item)}
                                                                            title={t('SHOP_SET_AUTO_BUY')}
                                                                        >
                                                                            <AlarmClock className="h-4 w-4" />
                                                                        </Button>
                                                                        <Button
                                                                            size="sm"
                                                                            disabled={!canBuy}
                                                                            onClick={() => openPurchaseConfirm(
                                                                                t('[PurchaseConfirmationTitle]'),
                                                                                t('[PurchaseConfirmationMessage]'),
                                                                                t('[PurchaseConfirmationYesButton]'),
                                                                                `${getItemName(item.giveItem.itemType, item.giveItem.itemId)} × ${item.giveItem.itemCount}`,
                                                                                getTradeItemConsumeItems(item),
                                                                                getOwnedItemCount(item.giveItem.itemType, item.giveItem.itemId),
                                                                                () => handleBuyTradeItem(tab.tradeShopTabId, item)
                                                                            )}
                                                                        >
                                                                            {isSoldOut ? t('SHOP_SOLD_OUT') : t('[PurchaseConfirmationYesButton]')}
                                                                        </Button>
                                                                    </>
                                                                );
                                                            })()}
                                                        </div>
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        ))}
                                    </div>
                                )}
                            </CardContent>
                        </Card>
                    </TabsContent>
                );})}
                <TabsContent value="legendLeagueIcon"><LegendLeagueIconShop /></TabsContent>
                <TabsContent value="starsGuidance"><StarsGuidanceShop /></TabsContent>
            </Tabs>

            <AlertDialog open={confirmDialogOpen} onOpenChange={(open) => {
                setConfirmDialogOpen(open);
                if (!open) {
                    setConfirmAction(null);
                }
            }}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{confirmDialogTitle}</AlertDialogTitle>
                        <AlertDialogDescription>{confirmDialogDescription}</AlertDialogDescription>
                    </AlertDialogHeader>
                    <div className="space-y-3 text-sm">
                        <div className="rounded-md border bg-muted/30 px-3 py-2 text-center font-medium">
                            {confirmDialogItemLabel || t('SHOP_UNKNOWN_ITEM')}
                        </div>
                        {confirmDialogOwnedCount !== null && (
                            <div className="flex items-center gap-2 text-xs text-emerald-600">
                                <span>{t('[CommonOwnCurrencyLabel]')}</span>
                                <span className="font-semibold">{confirmDialogOwnedCount.toLocaleString()}</span>
                            </div>
                        )}
                        {confirmDialogConsumeItems.length > 0 ? (
                            <div className="space-y-2">
                                {confirmDialogConsumeItems.map((consumeItem) => (
                                    <div key={consumeItem.name} className="flex items-center justify-between gap-2">
                                        <span className="text-muted-foreground truncate">{consumeItem.name}</span>
                                        <div className="flex items-center gap-3 text-xs">
                                            <span className="text-muted-foreground">{t('[CommonRequireLabel]')}</span>
                                            <span className="font-semibold">{consumeItem.required.toLocaleString()}</span>
                                            <Separator orientation="vertical" className="h-3" />
                                            <span className="text-muted-foreground">{t('[CommonPossessionLabel]')}</span>
                                            <span className="font-semibold">{consumeItem.owned.toLocaleString()}</span>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        ) : (
                            <div className="text-center text-muted-foreground">{t('[ShopProductFreeLabel]')}</div>
                        )}
                    </div>
                    <AlertDialogFooter>
                        <AlertDialogCancel>{t('[CommonCancelLabel]')}</AlertDialogCancel>
                        <AlertDialogAction onClick={handleConfirmPurchase}>{confirmDialogActionLabel}</AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>

            {/* 自动购买对话框 */}
            <Dialog open={autoBuyDialogOpen} onOpenChange={setAutoBuyDialogOpen}>
                <DialogContent className="max-w-md">
                    <DialogHeader>
                        <DialogTitle>
                            {(() => {
                                const mb = tradeShopTabMasterMap[currentAutoBuyTabId];
                                return mb ? t('SHOP_TAB_AUTO_BUY', [t(mb.tabNameKey) || mb.memo]) : t('SHOP_AUTO_BUY');
                            })()}
                        </DialogTitle>
                        <DialogDescription>
                            {t('SHOP_AUTO_BUY_TITLE')}
                        </DialogDescription>
                    </DialogHeader>

                    <div className="space-y-4 py-4">
                        {/* 选择要购买的物品 */}
                        <div className="space-y-2">
                            <Label>{t('SHOP_BUY_ITEM')}</Label>
                            <Select
                                value={selectedAutoBuyItem || "any"}
                                onValueChange={(value) => setSelectedAutoBuyItem(value === "any" ? "" : value)}
                            >
                                <SelectTrigger>
                                    <SelectValue placeholder={t('SHOP_ANY_ITEM_HINT')} />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="any">{t('SHOP_ANY_ITEM_OPTION')}</SelectItem>
                                    {currentAutoBuyTabItems.map((item) => (
                                        <SelectItem
                                            key={`${item.giveItem.itemType}-${item.giveItem.itemId}-${item.giveItem.itemCount}`}
                                            value={`${item.giveItem.itemType}-${item.giveItem.itemId}-${item.giveItem.itemCount}`}
                                        >
                                            {getItemName(item.giveItem.itemType, item.giveItem.itemId)} × {item.giveItem.itemCount}
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                            <p className="text-xs text-muted-foreground">{t('SHOP_ANY_ITEM_HINT')}</p>
                        </div>

                        {/* 折扣幅度 */}
                        <div className="space-y-2">
                            <div className="flex justify-between">
                                <Label>{t('SHOP_DISCOUNT')}</Label>
                                <span className="text-sm font-medium">{autoBuyDiscount}%OFF</span>
                            </div>
                            <Slider
                                value={[autoBuyDiscount]}
                                onValueChange={(values) => setAutoBuyDiscount(values[0] || 0)}
                                min={0}
                                max={100}
                                step={10}
                            />
                        </div>

                        {/* 选择消耗物品 */}
                        <div className="space-y-2">
                            <Label>{t('SHOP_CONSUME_ITEM')}</Label>
                            <Select
                                value={selectedConsumeItem || "any"}
                                onValueChange={(value) => setSelectedConsumeItem(value === "any" ? "" : value)}
                            >
                                <SelectTrigger>
                                    <SelectValue placeholder={t('SHOP_ANY_CONSUME_HINT')} />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="any">{t('SHOP_ANY_CONSUME_OPTION')}</SelectItem>
                                    {(() => {
                                        const tabMaster = tradeShopTabMasterMap[currentAutoBuyTabId];
                                        const consumeItems = tabMaster?.consumeItemInfos || [];
                                        return consumeItems.map((item) => (
                                            <SelectItem
                                                key={`${item.itemType}-${item.itemId}`}
                                                value={`${item.itemType}-${item.itemId}`}
                                            >
                                                {getItemName(item.itemType, item.itemId)}
                                            </SelectItem>
                                        ));
                                    })()}
                                </SelectContent>
                            </Select>
                            <p className="text-xs text-muted-foreground">{t('SHOP_ANY_CONSUME_HINT')}</p>
                        </div>

                        {/* 消耗数量 */}
                        <div className="space-y-2">
                            <Label>{t('SHOP_CONSUME_COUNT')}</Label>
                            <Input
                                type="number"
                                min={0}
                                value={autoBuyConsumeCount}
                                onChange={(e) => setAutoBuyConsumeCount(parseInt(e.target.value) || 0)}
                                placeholder="0"
                            />
                            <p className="text-xs text-muted-foreground">{t('SHOP_ZERO_ANY')}</p>
                        </div>
                    </div>

                    <DialogFooter>
                        <Button variant="outline" onClick={() => setAutoBuyDialogOpen(false)}>
                            {t('SHOP_CANCEL')}
                        </Button>
                        <Button onClick={confirmAutoBuy} variant="outline" className="text-pink-500 border-pink-500 hover:bg-pink-50">
                            {t('SHOP_AUTO_BUY')}
                        </Button>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </div>
    );
}
