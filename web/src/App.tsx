import { useEffect } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Toaster } from './components/ui/toaster';
import { useLocalizationStore } from './store/localization-store';
import { useMasterStore } from './store/masterStore';
import { MainLayout } from './components/layout/MainLayout';
import { DashboardPage } from './pages/DashboardPage';
import { CharactersPage } from './pages/CharactersPage';
import { EquipmentPage } from './pages/EquipmentPage';
import { ItemsPage } from './pages/ItemsPage';
import { BattlePage } from './pages/BattlePage';
import { TowerPage } from './pages/TowerPage';
import { WishingFountainPage } from './pages/WishingFountainPage';
import { MissionsPage } from './pages/MissionsPage';
import { ShopPage } from './pages/ShopPage';
import { GachaPage } from './pages/GachaPage';
import { PVPPage } from './pages/PVPPage';
import { GuildPage } from './pages/GuildPage';
import { TimeSpaceCavePage } from './pages/TimeSpaceCavePage';
import { PhantomTemplePage } from './pages/PhantomTemplePage';
import { FriendsPage } from './pages/FriendsPage';
import { SettingsPage } from './pages/SettingsPage';
import { AccountsPage } from './pages/AccountsPage';
import { AutomationPage } from './pages/AutomationPage';
import { BattleLogPage } from './pages/BattleLogPage';
import { AutopilotPage } from './pages/AutopilotPage';
import { TeamTiersPage } from './pages/TeamTiersPage';
import { BASE_PATH } from './lib/base-path';
import './index.css';

function App() {
  const { currentLanguage, fetchResources } = useLocalizationStore();
  const syncMasterData = useMasterStore(state => state.sync);

  useEffect(() => {
    fetchResources(currentLanguage);
    syncMasterData();

    // 清理旧的 translation 存储 (localStorage)
    // 之前使用 useTranslationStore 时 persist 存储的名称是 'mementomori-translation'
    if (localStorage.getItem('mementomori-translation')) {
      console.log('Cleaning up legacy translation storage...');
      localStorage.removeItem('mementomori-translation');
    }
  }, [currentLanguage, fetchResources, syncMasterData]);

  return (
    <>
      <svg
        aria-hidden="true"
        width="0"
        height="0"
        style={{ position: 'absolute', width: 0, height: 0, overflow: 'hidden' }}
      >
        <filter id="svgTintUR">
          <feColorMatrix
            type="matrix"
            values="0.2784313725490196 0.2784313725490196 0.2784313725490196  0 0 0.00784313725490196 0.00784313725490196 0.00784313725490196  0 0 0 0 0  0 0  0 0 0 1 0"
          />
        </filter>
        <filter id="svgTintSSR">
          <feColorMatrix
            type="matrix"
            values="0.1411764705882353 0.1411764705882353 0.1411764705882353  0 0 0.027450980392156862 0.027450980392156862 0.027450980392156862  0 0 0.2549019607843137 0.2549019607843137 0.2549019607843137  0 0  0 0 0 1 0"
          />
        </filter>
        <filter id="svgTintSR">
          <feColorMatrix
            type="matrix"
            values="0.44313725490196076 0.44313725490196076 0.44313725490196076  0 0 0.2235294117647059 0.2235294117647059 0.2235294117647059  0 0 0.0196078431372549 0.0196078431372549 0.0196078431372549  0 0  0 0 0 1 0"
          />
        </filter>
        <filter id="svgTintR">
          <feColorMatrix
            type="matrix"
            values="0.15294117647058825 0.15294117647058825 0.15294117647058825  0 0 0.18823529411764706 0.18823529411764706 0.18823529411764706  0 0 0.27058823529411763 0.27058823529411763 0.27058823529411763  0 0  0 0 0 1 0"
          />
        </filter>
        <filter id="svgTintN">
          <feColorMatrix
            type="matrix"
            values="0.06666666666666667 0.06666666666666667 0.06666666666666667  0 0 0.0196078431372549 0.0196078431372549 0.0196078431372549  0 0 0.01568627450980392 0.01568627450980392 0.01568627450980392  0 0  0 0 0 1 0"
          />
        </filter>
      </svg>
      <BrowserRouter basename={BASE_PATH || undefined}>
        <Routes>
          {/* 账号管理页面 - 独立页面，不使用 MainLayout */}
          <Route path="/accounts" element={<AccountsPage />} />

          {/* 主应用 - 使用 MainLayout */}
          <Route element={<MainLayout />}>
            <Route path="/dashboard" element={<DashboardPage />} />
            <Route path="/characters" element={<CharactersPage />} />
            <Route path="/equipment" element={<EquipmentPage />} />
            <Route path="/items" element={<ItemsPage />} />
            <Route path="/battle" element={<BattlePage />} />
            <Route path="/tower" element={<TowerPage />} />
            <Route path="/fountain" element={<WishingFountainPage />} />
            <Route path="/missions" element={<MissionsPage />} />
            <Route path="/shop" element={<ShopPage />} />
            <Route path="/gacha" element={<GachaPage />} />
            <Route path="/pvp" element={<PVPPage />} />
            <Route path="/guild" element={<GuildPage />} />
            <Route path="/cave" element={<TimeSpaceCavePage />} />
            <Route path="/temple" element={<PhantomTemplePage />} />
            <Route path="/friends" element={<FriendsPage />} />
            <Route path="/battle-logs" element={<BattleLogPage />} />
            <Route path="/autopilot" element={<AutopilotPage />} />
            <Route path="/team-tiers" element={<TeamTiersPage />} />
            <Route path="/automation" element={<AutomationPage />} />
            <Route path="/settings" element={<SettingsPage />} />
          </Route>

          {/* 默认重定向到 Dashboard */}
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
          {/* 捕获所有未定义的路由并重定向到 Dashboard */}
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Routes>
        <Toaster />
      </BrowserRouter>
    </>
  );
}

export default App;
