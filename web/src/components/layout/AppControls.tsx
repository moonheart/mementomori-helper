import { useState } from 'react';
import { ChevronDown, Globe, Moon, Sun } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from '@/components/ui/dropdown-menu';
import { useLocalizationStore } from '@/store/localization-store';
import { applyTheme, getTheme, type Theme } from '@/lib/theme';

const LANGUAGES = [
    { code: 'zhCN', label: '简体中文' },
    { code: 'zhTW', label: '繁體中文' },
    { code: 'enUS', label: 'English' },
    { code: 'jaJP', label: '日本語' },
    { code: 'koKR', label: '한국어' },
];

/** 浅色/深色切换 (顶栏和账号页共用) */
export function ThemeToggle() {
    const { t } = useLocalizationStore();
    const [theme, setTheme] = useState<Theme>(getTheme);
    const toggle = () => {
        const next = theme === 'dark' ? 'light' : 'dark';
        applyTheme(next, true);
        setTheme(next);
    };
    return (
        <Button variant="ghost" size="icon" onClick={toggle} title={t('HEADER_TOGGLE_THEME')} aria-label={t('HEADER_TOGGLE_THEME')}>
            {theme === 'dark' ? <Sun className="h-5 w-5 text-muted-foreground" /> : <Moon className="h-5 w-5 text-muted-foreground" />}
        </Button>
    );
}

/** 语言菜单: 显示当前语言, 不只是一个地球 */
export function LanguageMenu() {
    const { t, currentLanguage, setLanguage } = useLocalizationStore();
    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="ghost" size="sm" className="gap-1.5 text-muted-foreground" title={t('HEADER_SWITCH_LANGUAGE')}>
                    <Globe className="h-4 w-4" />
                    <span className="hidden text-sm sm:inline">{LANGUAGES.find(l => l.code === currentLanguage)?.label ?? currentLanguage}</span>
                    <ChevronDown className="h-3.5 w-3.5" />
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
                {LANGUAGES.map(lang => (
                    <DropdownMenuItem key={lang.code} onClick={() => setLanguage(lang.code)} className={currentLanguage === lang.code ? 'bg-accent' : ''}>
                        {lang.label}
                    </DropdownMenuItem>
                ))}
            </DropdownMenuContent>
        </DropdownMenu>
    );
}
