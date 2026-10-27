import { useState, useEffect } from 'react';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Card } from '@/components/ui/card';
import { Loader2, Package } from 'lucide-react';
import { useAccountStore } from '@/store/accountStore';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { EquipmentTab } from '@/components/items/tabs/EquipmentTab';
import { ConsumptionTab } from '@/components/items/tabs/ConsumptionTab';
import { MaterialTab } from '@/components/items/tabs/MaterialTab';
import { SphereTab } from '@/components/items/tabs/SphereTab';
import { SetPieceTab } from '@/components/items/tabs/SetPieceTab';
import { UseItemDialog } from '@/components/items/UseItemDialog';
import { UserItemDtoInfo, UserEquipmentDtoInfo, ItemMB } from '@/api/generated';
import { ortegaApi } from '@/api/ortega-client';
import { getItemName } from '@/lib/itemUtils';

export function ItemsPage() {
    const [initialLoading, setInitialLoading] = useState(true);
    const [refreshing, setRefreshing] = useState(false);
    const [activeTab, setActiveTab] = useState('equipment');
    const [items, setItems] = useState<UserItemDtoInfo[]>([]);
    const [userEquipments, setUserEquipments] = useState<UserEquipmentDtoInfo[]>([]);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [selectedItem, setSelectedItem] = useState<UserItemDtoInfo | null>(null);
    const [selectedItemName, setSelectedItemName] = useState('');
    const { currentAccountId } = useAccountStore();
    const sync = useMasterStore(state => state.sync);
    const getTable = useMasterStore(state => state.getTable);
    const t = useLocalizationStore((state) => state.t);

    const loadData = async (isRefresh = false) => {
        try {
            if (isRefresh) {
                setRefreshing(true);
            } else {
                setInitialLoading(true);
            }

            // 确保Master数据已加载
            await sync();

            // 直接调用API获取用户数据
            const userData = await ortegaApi.user.getUserData({});

            // 设置道具数据
            if (userData.userSyncData) {
                setItems(userData.userSyncData.userItemDtoInfo || []);
                setUserEquipments(userData.userSyncData.userEquipmentDtoInfos || []);
            }
        } catch (error) {
            console.error('Failed to load items data:', error);
        } finally {
            if (isRefresh) {
                setRefreshing(false);
            } else {
                setInitialLoading(false);
            }
        }
    };

    useEffect(() => {
        if (!currentAccountId) {
            setInitialLoading(false);
            return;
        }

        loadData();
    }, [currentAccountId]);

    const handleUseItem = async (item: UserItemDtoInfo) => {
        // 获取道具名称
        const itemTable = await getTable<ItemMB>('ItemTable');
        const masterTables = { ItemTable: itemTable };
        const name = getItemName(item.itemType, item.itemId, masterTables, t);

        setSelectedItem(item);
        setSelectedItemName(name);
        setDialogOpen(true);
    };

    const handleUseSuccess = () => {
        // 刷新数据（保持当前 Tab 选中状态）
        loadData(true);
    };

    const totalUniqueItems = items.filter((item) => item.itemCount > 0).length;

    if (initialLoading) {
        return (
            <div className="flex items-center justify-center min-h-[400px]">
                <Loader2 className="w-8 h-8 animate-spin text-primary" />
            </div>
        );
    }

    return (
        <div className="w-full p-6 space-y-6">
            {/* Tab导航和内容 */}
            <Card className="p-6">
                {refreshing && (
                    <div className="flex items-center gap-2 text-sm text-muted-foreground mb-3">
                        <Loader2 className="w-4 h-4 animate-spin" />
                        <span>{t('ITEMS_REFRESHING')}</span>
                    </div>
                )}
                <Tabs value={activeTab} onValueChange={setActiveTab} className="space-y-6">
                    <TabsList className="grid w-full grid-cols-5">
                        <TabsTrigger value="equipment">
                            {t('[ItemBoxTabEquipment]')}
                        </TabsTrigger>
                        <TabsTrigger value="consumption">
                            {t('[ItemBoxTabConsumption]')}
                        </TabsTrigger>
                        <TabsTrigger value="material">
                            {t('[ItemBoxTabMaterial]')}
                        </TabsTrigger>
                        <TabsTrigger value="sphere">
                            {t('[ItemBoxTabSphere]')}
                        </TabsTrigger>
                        <TabsTrigger value="setpiece">
                            {t('[ItemBoxTabSetPiece]')}
                        </TabsTrigger>
                    </TabsList>

                    <TabsContent value="equipment" className="space-y-4">
                        <EquipmentTab
                            items={items}
                            userEquipments={userEquipments}
                        />
                    </TabsContent>

                    <TabsContent value="consumption" className="space-y-4">
                        <ConsumptionTab
                            items={items}
                            onUseItem={handleUseItem}
                        />
                    </TabsContent>

                    <TabsContent value="material" className="space-y-4">
                        <MaterialTab items={items} />
                    </TabsContent>

                    <TabsContent value="sphere" className="space-y-4">
                        <SphereTab items={items} />
                    </TabsContent>

                    <TabsContent value="setpiece" className="space-y-4">
                        <SetPieceTab items={items} />
                    </TabsContent>
                </Tabs>
            </Card>

            {/* 使用道具对话框 */}
            <UseItemDialog
                open={dialogOpen}
                onOpenChange={setDialogOpen}
                item={selectedItem}
                itemName={selectedItemName}
                onSuccess={handleUseSuccess}
            />
        </div>
    );
}
