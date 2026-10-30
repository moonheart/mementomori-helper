import { AccountManagement } from '@/components/account/AccountManagement';
import { LanguageMenu, ThemeToggle } from '@/components/layout/AppControls';

export function AccountsPage() {
    return (
        <div className="min-h-screen bg-muted/40 dark:bg-background">
            <header className="border-b bg-background/80 backdrop-blur">
                <div className="mx-auto flex h-14 max-w-3xl items-center justify-between px-4">
                    <span className="bg-gradient-to-r from-violet-600 to-pink-500 bg-clip-text text-lg font-bold text-transparent">MementoMori Helper</span>
                    <div className="flex items-center gap-1">
                        <LanguageMenu />
                        <ThemeToggle />
                    </div>
                </div>
            </header>
            <main className="mx-auto max-w-3xl px-4 py-8">
                <AccountManagement />
            </main>
        </div>
    );
}
