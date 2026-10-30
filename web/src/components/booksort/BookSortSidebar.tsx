import { useState, useEffect, useMemo } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Store, CheckSquare, Users } from 'lucide-react';
import { BookSortSyncData } from '@/api/generated/bookSortSyncData';
import { BookSortMissionsDialog } from './BookSortMissionsDialog';
import { ortegaApi } from '@/api/ortega-client';
import { MissionGroupType, MissionStatusType, MissionGetMissionInfoResponse, UserMissionDtoInfo, MissionInfo } from '@/api/generated';
import { useTranslation } from '@/hooks/useTranslation';

// Simple parser for Unity rich text tags like <color=#248CA7>text</color>
const RichTextRenderer = ({ text }: { text: string }) => {
    if (!text) return null;

    // Regular expression to match <color=#hex>...</color>
    // This splits the string into an array where odd indices are the matched groups.
    // E.g. "a <color=#fff>b</color> c" -> ["a ", "fff", "b", " c"]
    // A more robust regex:
    const regex = /<color=([^>]+)>(.*?)<\/color>/g;

    const parts = [];
    let lastIndex = 0;

    let match;
    while ((match = regex.exec(text)) !== null) {
        // Push preceding plain text
        if (match.index > lastIndex) {
            parts.push(<span key={lastIndex}>{text.substring(lastIndex, match.index)}</span>);
        }

        // Push the colored styled text
        const color = match[1];
        const content = match[2];
        parts.push(<span key={match.index} style={{ color }}>{content}</span>);

        lastIndex = regex.lastIndex;
    }

    // Push remaining plain text
    if (lastIndex < text.length) {
        parts.push(<span key={lastIndex}>{text.substring(lastIndex)}</span>);
    }

    return <>{parts.length > 0 ? parts : text}</>;
};

interface BookSortSidebarProps {
    syncData: BookSortSyncData | null;
    items: { id: number, count: number, name: string, desc: string }[];
    selectedItemId: number | null;
    onSelectItem: (id: number) => void;
    onOpenDispatch: () => void;
    onOpenShop: () => void;
}

export function BookSortSidebar({
    syncData,
    items,
    selectedItemId,
    onSelectItem,
    onOpenDispatch,
    onOpenShop
}: BookSortSidebarProps) {
    const { t } = useTranslation();
    const [showMissionsDialog, setShowMissionsDialog] = useState(false);
    const [missionData, setMissionData] = useState<MissionGetMissionInfoResponse | null>(null);

    const fetchMissionInfo = async () => {
        try {
            const response = await ortegaApi.mission.getMissionInfo({
                targetMissionGroupList: [MissionGroupType.BookSort]
            });
            setMissionData(response);
        } catch (error) {
            console.error('Failed to fetch BookSort mission info:', error);
        }
    };

    useEffect(() => {
        fetchMissionInfo();
    }, []);

    const claimableCount = useMemo(() => {
        if (!missionData?.missionInfoDict) return 0;
        const dict = missionData.missionInfoDict as Record<string | number, MissionInfo>;
        const groupInfo = dict[MissionGroupType.BookSort] || dict[MissionGroupType[MissionGroupType.BookSort]];
        if (!groupInfo || !groupInfo.userMissionDtoInfoDict) return 0;

        let count = 0;
        Object.values(groupInfo.userMissionDtoInfoDict).forEach((dtoList) => {
            if (!dtoList) return;
            (dtoList as UserMissionDtoInfo[]).forEach((dto) => {
                const history = dto.missionStatusHistory as Record<string | number, number[]>;
                if (!history) return;
                const notReceivedList = history[MissionStatusType.NotReceived] || history[MissionStatusType[MissionStatusType.NotReceived]] || [];
                if (notReceivedList.length > 0) {
                    count++;
                }
            });
        });
        return count;
    }, [missionData]);

    return (
        <>
            <Card className="h-full flex flex-col rounded-l-none border-y-0 border-r-0 border-l bg-background/95 backdrop-blur-xl">
                <CardContent className="p-4 flex-1 flex flex-col gap-4">

                    {/* Top Actions */}
                    <div className="grid grid-cols-3 gap-2 shrink-0">
                        <Button variant="outline" className="flex flex-col h-auto py-3 gap-1 hover:border-primary/50 hover:bg-primary/5 transition-colors" onClick={onOpenDispatch}>
                            <Users className="w-5 h-5 text-blue-400" />
                            <span className="text-xs font-medium">{t('[BookAssistTitle]')}</span>
                        </Button>
                        <Button variant="outline" className="flex flex-col h-auto py-3 gap-1 hover:border-primary/50 hover:bg-primary/5 transition-colors relative" onClick={() => setShowMissionsDialog(true)}>
                            <CheckSquare className="w-5 h-5 text-green-400" />
                            <span className="text-xs font-medium">{t('[BookSortMissionButtonLabel]')}</span>
                            {claimableCount > 0 && (
                                <span className="absolute top-1 right-1 w-3 h-3 bg-red-500 rounded-full animate-bounce" />
                            )}
                        </Button>
                        <Button variant="outline" className="flex flex-col h-auto py-3 gap-1 hover:border-primary/50 hover:bg-primary/5 transition-colors" onClick={onOpenShop}>
                            <Store className="w-5 h-5 text-orange-400" />
                            <span className="text-xs font-medium">{t('[BookSortShopButtonLabel]')}</span>
                        </Button>
                    </div>

                    <div className="text-sm font-semibold tracking-wider text-muted-foreground border-b pb-2">{t('BOOKSORT_CLEANING_ITEMS')}</div>

                    {/* Items List */}
                    <ScrollArea className="flex-1 -mx-2 px-2">
                        <div className="space-y-2 p-1">
                            {items.length === 0 ? (
                                <div className="text-center text-sm text-muted-foreground py-8">
                                    {t('BOOKSORT_NO_ITEMS')}
                                </div>
                            ) : (
                                items.map(item => (
                                    <button
                                        key={item.id}
                                        onClick={() => onSelectItem(item.id)}
                                        className={`
                                            w-full text-left p-3 rounded-lg border transition-all duration-200
                                            ${selectedItemId === item.id
                                                ? 'border-primary bg-primary/10 shadow-[0_0_15px_rgba(var(--primary),0.15)] scale-[1.02]'
                                                : 'border-border bg-card hover:border-primary/50 hover:bg-accent'}
                                        `}
                                    >
                                        <div className="flex items-center justify-between mb-1">
                                            <span className="font-semibold text-sm">{item.name}</span>
                                            <span className="text-xs font-mono bg-background px-2 py-0.5 rounded-full shadow-sm">
                                                x{item.count}
                                            </span>
                                        </div>
                                    </button>
                                ))
                            )}
                        </div>
                    </ScrollArea>

                    {/* Active Item Details */}
                    {selectedItemId && (
                        <div className="shrink-0 p-3 bg-secondary/50 rounded-lg border border-primary/20 backdrop-blur-md">
                            {items.find(i => i.id === selectedItemId) && (
                                <>
                                    <h4 className="font-medium text-sm text-primary mb-1">
                                        {items.find(i => i.id === selectedItemId)?.name}
                                    </h4>
                                    <div className="text-xs text-muted-foreground space-y-1 mt-1">
                                        <RichTextRenderer text={items.find(i => i.id === selectedItemId)?.desc || ''} />
                                    </div>
                                </>
                            )}
                        </div>
                    )}
                </CardContent>
            </Card>

            <BookSortMissionsDialog
                open={showMissionsDialog}
                onOpenChange={setShowMissionsDialog}
                missionData={missionData}
                onFetchNeeded={fetchMissionInfo}
            />
        </>
    );
}
