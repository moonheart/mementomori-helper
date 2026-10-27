import { Sidebar } from './Sidebar';
import { Header } from './Header';
import { Outlet, Navigate } from 'react-router-dom';
import { useAccountStore } from '@/store/accountStore';
import { GlobalLogConsole } from '@/components/GlobalLogConsole';

export function MainLayout() {
    const currentAccountId = useAccountStore((state) => state.currentAccountId);

    if (!currentAccountId) {
        return <Navigate to="/accounts" replace />;
    }

    return (
        <div className="flex h-screen overflow-hidden">
            <Sidebar />
            <div className="flex flex-1 flex-col overflow-hidden">
                <Header />
                <main className="flex-1 overflow-y-auto bg-muted/30 p-6">
                    <Outlet />
                </main>
            </div>
            <GlobalLogConsole />
        </div>
    );
}
