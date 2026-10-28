import { X, Home, Users, Package, Boxes, ListTodo, ShoppingCart, Swords, Shield, MapPin, Settings, Sparkles, Mountain, Ghost, Bot, ScrollText, Rocket, Crown } from 'lucide-react';
import { cn } from '@/lib/utils';
import { Link, useLocation } from 'react-router-dom';
import { useLocalizationStore } from '@/store/localization-store';

const navItems: { titleKey: string; href: string; icon: React.ElementType }[] = [
    {
        titleKey: '[CommonFooterHomeButtonLabel]',
        href: '/dashboard',
        icon: Home
    },
    {
        titleKey: 'SIDEBAR_AUTOMATION',
        href: '/automation',
        icon: Bot
    },
    {
        titleKey: 'SIDEBAR_AUTOPILOT',
        href: '/autopilot',
        icon: Rocket
    },
    {
        titleKey: 'SIDEBAR_TEAM_TIERS',
        href: '/team-tiers',
        icon: Crown
    },
    {
        titleKey: '[CommonFooterCharacterButtonLabel]',
        href: '/characters',
        icon: Users
    },
    {
        titleKey: '[ItemBoxTabEquipment]',
        href: '/equipment',
        icon: Package
    },
    {
        titleKey: '[CommonFooterItemBoxButtonLabel]',
        href: '/items',
        icon: Boxes
    },
    {
        titleKey: '[CommonFooterAutoBattleButtonLabel]',
        href: '/battle',
        icon: Swords
    },
    {
        titleKey: '[CommonHeaderTowerBattleLabel]',
        href: '/tower',
        icon: MapPin
    },
    {
        titleKey: '[CommonHeaderBountyQuestLabel]',
        href: '/fountain',
        icon: Sparkles
    },
    {
        titleKey: '[CommonHeaderDungeonBattleLabel]',
        href: '/cave',
        icon: Mountain
    },
    {
        titleKey: '[CommonHeaderLocalRaidLabel]',
        href: '/temple',
        icon: Ghost
    },
    {
        titleKey: '[LockEquipmentDeckTypeLeague]',
        href: '/pvp',
        icon: Shield
    },
    {
        titleKey: '[CommonFooterGuildButtonLabel]',
        href: '/guild',
        icon: Users
    },
    {
        titleKey: '[MyPageMenuButtonMissionLabel]',
        href: '/missions',
        icon: ListTodo
    },
    {
        titleKey: '[CommonFooterGachaButtonLabel]',
        href: '/gacha',
        icon: Sparkles
    },
    {
        titleKey: '[CommonHeaderExchangeLabel]',
        href: '/shop',
        icon: ShoppingCart
    },
    {
        titleKey: '[CommonHeaderFriendLabel]',
        href: '/friends',
        icon: Users
    },
    {
        titleKey: '[LocalRaidBattleHistoryDialogTitle]',
        href: '/battle-logs',
        icon: ScrollText
    },
    {
        titleKey: '[MenuSettingButton]',
        href: '/settings',
        icon: Settings
    }
];

/** 桌面: 固定在左边; 手机 (<md): 点菜单按钮才滑出来, 点链接或背景关闭 */
export function Sidebar({ open, onClose }: { open: boolean; onClose: () => void }) {
    const { t } = useLocalizationStore();
    const location = useLocation();

    return (
        <>
        {open && <div className="fixed inset-0 z-40 bg-black/50 md:hidden" onClick={onClose} aria-hidden />}
        <div className={cn(
            'fixed inset-y-0 left-0 z-50 flex h-full w-64 flex-col border-r bg-background transition-transform md:static md:translate-x-0',
            open ? 'translate-x-0' : '-translate-x-full'
        )}>
            {/* Logo */}
            <div className="flex h-16 items-center justify-between border-b px-6">
                <h1 className="text-xl font-bold bg-gradient-to-r from-purple-600 to-pink-600 bg-clip-text text-transparent">
                    MementoMori
                </h1>
                <button className="rounded-md p-2 text-muted-foreground hover:bg-accent md:hidden" onClick={onClose} aria-label="Close menu">
                    <X className="h-5 w-5" />
                </button>
            </div>

            {/* Navigation */}
            <nav className="flex-1 space-y-1 overflow-y-auto p-4">
                {navItems.map((item) => {
                    const Icon = item.icon;
                    const isActive = location.pathname === item.href ||
                        (item.href !== '/dashboard' && location.pathname.startsWith(item.href + '/'));

                    return (
                        <Link
                            key={item.href}
                            to={item.href}
                            onClick={onClose}
                            className={cn(
                                'flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors',
                                isActive
                                    ? 'bg-primary text-primary-foreground'
                                    : 'text-muted-foreground hover:bg-accent hover:text-accent-foreground'
                            )}
                        >
                            <Icon className="h-5 w-5" />
                            {t(item.titleKey)}
                        </Link>
                    );
                })}
            </nav>
        </div>
        </>
    );
}
