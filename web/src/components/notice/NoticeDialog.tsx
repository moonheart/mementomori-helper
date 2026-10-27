import React, { useState, useEffect } from 'react';
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Tabs, TabsList, TabsTrigger, TabsContent } from '@/components/ui/tabs';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Button } from '@/components/ui/button';
import { Loader2, Megaphone, Calendar, ChevronRight } from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { NoticeInfo } from '@/api/generated/noticeInfo';
import { NoticeGetMyPageNoticeInfoListResponse } from '@/api/generated/NoticegetMyPageNoticeInfoListResponse';
import { LanguageType } from '@/api/generated/languageType';
import { cn } from '@/lib/utils';
import { useTranslation } from '@/hooks/useTranslation';
import { useLocalizationStore } from '@/store/localization-store';

interface NoticeDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
}

export function NoticeDialog({ open, onOpenChange }: NoticeDialogProps) {
    const { t } = useTranslation();
    const currentLanguage = useLocalizationStore(state => state.currentLanguage);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [eventList, setEventList] = useState<NoticeInfo[]>([]);
    const [noticeList, setNoticeList] = useState<NoticeInfo[]>([]);
    const [selectedNotice, setSelectedNotice] = useState<NoticeInfo | null>(null);
    const [activeTab, setActiveTab] = useState<'event' | 'notice'>('event');

    const getLanguageType = () => LanguageType[currentLanguage as keyof typeof LanguageType] ?? LanguageType.zhCN;

    useEffect(() => {
        if (open) {
            fetchNoticeList();
        } else {
            setSelectedNotice(null);
            setActiveTab('event');
            setError(null);
        }
    }, [open]);

    // 切换 Tab 时，自动选中第一项
    useEffect(() => {
        const list = activeTab === 'event' ? eventList : noticeList;
        setSelectedNotice(list.length > 0 ? list[0] : null);
    }, [activeTab, eventList, noticeList]);

    async function fetchNoticeList() {
        try {
            setLoading(true);
            setError(null);
            const response = await ortegaApi.notice.getMypageNoticeInfoList({
                languageType: getLanguageType(),
            }) as NoticeGetMyPageNoticeInfoListResponse;

            const events = response?.eventInfoList ?? [];
            const notices = response?.noticeInfoList ?? [];
            setEventList(events);
            setNoticeList(notices);

            // 默认选中活动第一项
            if (events.length > 0) {
                setSelectedNotice(events[0]);
                setActiveTab('event');
            } else if (notices.length > 0) {
                setSelectedNotice(notices[0]);
                setActiveTab('notice');
            }
        } catch (err) {
            console.error('Failed to fetch notice list:', err);
            setError(t('NOTICE_DIALOG_FETCH_ERROR'));
        } finally {
            setLoading(false);
        }
    }

    /** 去除所有 HTML / Unity 标签，用于纯文本场合 */
    function stripTags(text: string): string {
        if (!text) return '';
        return text
            .replace(/<br\s*\/?>/gi, ' ')
            .replace(/<color=[^>]+>(.*?)<\/color>/gi, '$1')
            .replace(/<[^>]+>/g, '');
    }

    /** 左侧列表标题：保留 <br> 换行，去除 color 等其他标签 */
    function renderTitleContent(text: string): React.ReactNode {
        if (!text) return null;
        const lines = text.split(/<br\s*\/?>\s*/i);
        return lines.map((line, i) => {
            // 去掉 color 标签但保留文字
            const clean = line
                .replace(/<color=[^>]+>(.*?)<\/color>/gi, '$1')
                .replace(/<[^>]+>/g, '');
            return (
                <React.Fragment key={i}>
                    {clean}
                    {i < lines.length - 1 && <br />}
                </React.Fragment>
            );
        });
    }

    /** 解析 <br> 和 Unity <color=#XXXXXX>...</color> 标签为 React 元素 */
    function renderNoticeContent(text: string): React.ReactNode {
        if (!text) return null;

        // 先按 <br> 分段
        const lines = text.split(/<br\s*\/?>/i);
        const result: React.ReactNode[] = [];

        lines.forEach((line, lineIdx) => {
            // 在每行内解析 color 标签
            const parts = line.split(/(<color=[^>]+>.*?<\/color>)/i);
            parts.forEach((part, partIdx) => {
                const colorMatch = part.match(/^<color=([^>]+)>(.*?)<\/color>$/i);
                if (colorMatch) {
                    result.push(
                        <span key={`${lineIdx}-${partIdx}`} style={{ color: colorMatch[1] }}>
                            {colorMatch[2]}
                        </span>
                    );
                } else {
                    result.push(<span key={`${lineIdx}-${partIdx}`}>{part}</span>);
                }
            });

            // 非最后一行时插入换行
            if (lineIdx < lines.length - 1) {
                result.push(<br key={`br-${lineIdx}`} />);
            }
        });

        return result;
    }

    const currentList = activeTab === 'event' ? eventList : noticeList;

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className="max-w-3xl h-[75vh] flex flex-col p-0 gap-0 overflow-hidden">
                <DialogHeader className="px-6 pt-5 pb-3 border-b shrink-0">
                    <DialogTitle className="flex items-center gap-2 text-base">
                        <Megaphone className="h-4 w-4 text-amber-500" />
                        {t('NOTICE_DIALOG_TITLE')}
                    </DialogTitle>
                </DialogHeader>

                <Tabs
                    value={activeTab}
                    onValueChange={(v) => setActiveTab(v as 'event' | 'notice')}
                    className="flex flex-col flex-1 min-h-0"
                >
                    {/* Tab 切换 */}
                    <div className="px-4 pt-2 shrink-0">
                        <TabsList className="w-full">
                            <TabsTrigger value="event" className="flex-1 flex items-center gap-1.5">
                                <Calendar className="h-3.5 w-3.5" />
                                {t('NOTICE_DIALOG_EVENT_TAB')}
                                {eventList.length > 0 && (
                                    <span className="ml-1 text-xs text-muted-foreground">({eventList.length})</span>
                                )}
                            </TabsTrigger>
                            <TabsTrigger value="notice" className="flex-1 flex items-center gap-1.5">
                                <Megaphone className="h-3.5 w-3.5" />
                                {t('NOTICE_DIALOG_NOTICE_TAB')}
                                {noticeList.length > 0 && (
                                    <span className="ml-1 text-xs text-muted-foreground">({noticeList.length})</span>
                                )}
                            </TabsTrigger>
                        </TabsList>
                    </div>

                    {/* 内容区 */}
                    <TabsContent value={activeTab} className="flex-1 min-h-0 mt-0 p-0" forceMount>
                        {loading ? (
                            <div className="flex items-center justify-center h-full">
                                <Loader2 className="h-6 w-6 animate-spin" />
                                <span className="ml-2 text-sm text-muted-foreground">{t('NOTICE_DIALOG_LOADING')}</span>
                            </div>
                        ) : error ? (
                            <div className="flex flex-col items-center justify-center h-full gap-3">
                                <p className="text-sm text-destructive">{error}</p>
                                <Button variant="outline" size="sm" onClick={fetchNoticeList}>
                                    {t('NOTICE_DIALOG_RETRY')}
                                </Button>
                            </div>
                        ) : currentList.length === 0 ? (
                            <div className="flex items-center justify-center h-full text-sm text-muted-foreground">
                                {activeTab === 'event' ? t('NOTICE_DIALOG_NO_EVENTS') : t('NOTICE_DIALOG_NO_NOTICES')}
                            </div>
                        ) : (
                            <div className="flex h-full">
                                {/* 左侧列表 */}
                                <div className="w-52 shrink-0 border-r flex flex-col">
                                    <ScrollArea className="flex-1">
                                        <div className="py-1">
                                            {currentList.map((item) => {
                                                const isSelected = selectedNotice?.id === item.id;
                                                return (
                                                    <button
                                                        key={item.id}
                                                        onClick={() => setSelectedNotice(item)}
                                                        className={cn(
                                                            'w-full text-left px-3 py-2.5 flex items-center gap-2 transition-colors text-sm',
                                                            'hover:bg-accent hover:text-accent-foreground',
                                                            isSelected
                                                                ? 'bg-primary/10 text-primary font-medium border-l-2 border-primary'
                                                                : 'border-l-2 border-transparent'
                                                        )}
                                                    >
                                                        <span className="flex-1 leading-snug">
                                                            {renderTitleContent(item.buttonTitle || item.title)}
                                                        </span>
                                                        <ChevronRight className={cn(
                                                            'h-3.5 w-3.5 shrink-0 text-muted-foreground transition-opacity',
                                                            isSelected ? 'opacity-100' : 'opacity-0'
                                                        )} />
                                                    </button>
                                                );
                                            })}
                                        </div>
                                    </ScrollArea>
                                </div>

                                {/* 右侧详情 */}
                                <div className="flex-1 min-w-0">
                                    {selectedNotice ? (
                                        <ScrollArea className="h-full">
                                            <div className="px-5 py-4 space-y-3">
                                                <h3 className="font-semibold text-base leading-snug">
                                                    ◆ {selectedNotice.title}
                                                </h3>
                                                <div className="text-sm text-foreground/90 leading-relaxed">
                                                    {renderNoticeContent(selectedNotice.mainText)}
                                                </div>
                                            </div>
                                        </ScrollArea>
                                    ) : (
                                        <div className="flex items-center justify-center h-full text-sm text-muted-foreground">
                                            {activeTab === 'event' ? t('NOTICE_DIALOG_SELECT_EVENT') : t('NOTICE_DIALOG_SELECT_NOTICE')}
                                        </div>
                                    )}
                                </div>
                            </div>
                        )}
                    </TabsContent>
                </Tabs>

                {/* 底部关闭按钮 */}
                <div className="px-6 py-3 border-t shrink-0 flex justify-end">
                    <Button variant="outline" size="sm" onClick={() => onOpenChange(false)}>
                        {t('NOTICE_DIALOG_CLOSE')}
                    </Button>
                </div>
            </DialogContent>
        </Dialog>
    );
}
