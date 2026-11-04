import { useState } from 'react';
import { Sidebar } from './Sidebar';
import { Header } from './Header';
import { Outlet, Navigate } from 'react-router-dom';
import { useAccountStore } from '@/store/accountStore';
import { GlobalLogConsole } from '@/components/GlobalLogConsole';

export function MainLayout() {
    const currentAccountId = useAccountStore((state) => state.currentAccountId);
    const [navOpen, setNavOpen] = useState(false); // 手机上侧边栏是抽屉

    if (!currentAccountId) {
        return <Navigate to="/accounts" replace />;
    }

    return (
        <div className="flex h-screen overflow-hidden">
            <Sidebar open={navOpen} onClose={() => setNavOpen(false)} />
            <div className="flex min-w-0 flex-1 flex-col overflow-hidden">
                <Header onMenu={() => setNavOpen(true)} />
                <main className="flex-1 overflow-y-auto bg-muted/30 p-3 md:p-6">
                    <Outlet />
                </main>
            </div>
            <GlobalLogConsole />
        </div>
    );
}
