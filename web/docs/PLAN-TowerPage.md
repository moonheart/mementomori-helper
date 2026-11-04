# TowerPage 真实数据接入方案

## 1. 数据需求分析

### 1.1 用户进度数据 (UserTowerBattleDtoInfo)
通过 `ortegaApi.user.getUserData({})` 获取 `userTowerBattleDtoInfos: UserTowerBattleDtoInfo[]`。
每个对象包含：
- `towerType`: 塔类型 (1: 无穷, 2: 蓝, 3: 红, 4: 绿, 5: 黄)
- `maxTowerBattleId`: 当前通关的最大 ID (Master 数据中的 ID)
- `todayBattleCount`: 今日已挑战次数
- `todayClearNewFloorCount`: 今日新通关层数

### 1.2 Master 静态数据 (TowerBattleQuestMB)
通过 `masterService.getTableData('TowerBattleQuestMB')` 获取全量楼层配置。
- `id`: 唯一标识
- `towerType`: 对应塔类型
- `floor`: 楼层号
- `baseClearPartyDeckPower`: 推荐战力
- `battleRewardsFirst`: 首通奖励
- `battleRewardsConfirmed`: 常规奖励

## 2. 状态管理方案

使用 React `useState` 和 `useEffect` 加载数据。
需要加载：
1. `userTowerBattleDtoInfos` (从 `AccountStore` 获取或通过 API 直接请求)
2. `towerQuests` (Master 数据)

## 3. 核心逻辑实现

### 3.1 属性塔开放状态
参考 `api/MementoMori/Funcs/InfiniteTower.cs` 中的逻辑：
- 采用 4:00 AM 重置时间。
- 开放安排：
    - **周日**: 全部开放 (蓝、红、绿、黄)
    - **周一**: 蓝
    - **周二**: 红
    - **周三**: 绿
    - **周四**: 黄
    - **周五**: 蓝、红
    - **周六**: 黄、绿
    - **无穷之塔**: 始终开放

```typescript
const getAvailableTowerTypes = (): TowerType[] => {
    // 采用 4:00 AM 重置
    const now = new Date();
    const adjustedNow = new Date(now.getTime() - 4 * 60 * 60 * 1000);
    const day = adjustedNow.getDay(); // 0: Sunday, 1: Monday, ...

    const available = [TowerType.Infinite];
    switch (day) {
        case 0: available.push(TowerType.Blue, TowerType.Red, TowerType.Green, TowerType.Yellow); break;
        case 1: available.push(TowerType.Blue); break;
        case 2: available.push(TowerType.Red); break;
        case 3: available.push(TowerType.Green); break;
        case 4: available.push(TowerType.Yellow); break;
        case 5: available.push(TowerType.Blue, TowerType.Red); break;
        case 6: available.push(TowerType.Yellow, TowerType.Green); break;
    }
    return available;
};
```

### 3.2 楼层列表渲染
筛选出当前选定塔的所有楼层，并标记：
- `cleared`: `floor.id <= userTowerProgress.maxTowerBattleId`
- `current`: `floor.id === userTowerProgress.maxTowerBattleId + 1`

### 3.3 次数与限制
- **无穷之塔**:
    - 每日 3 次免费快速挑战 (根据 `InfiniteTower.cs` 逻辑)。
    - 通关新楼层不消耗次数。
- **属性塔**:
    - 每日每种属性塔最多通关 10 层 (`TodayClearNewFloorCount < 10`)。

### 3.4 奖励展示逻辑
- 遍历 `battleRewardsFirst` (首通奖励) 和 `battleRewardsConfirmed` (常规奖励)。
- 使用 `TextResourceTable` (或前端的翻译/图标逻辑) 显示道具名称和数量。

## 4. 任务清单

1. [ ] 在 `src/pages/TowerPage.tsx` 中引入 `ortegaApi` 和 `masterService`。
2. [ ] 定义 `TowerPage` 所需的状态结构。
3. [ ] 实现数据获取函数 `fetchTowerData`。
4. [ ] 实现 Master 数据与用户进度的聚合逻辑。
5. [ ] 更新 UI 模板，绑定真实数据。
6. [ ] 优化奖励显示 (解析 `UserItem` 数组)。
7. [ ] 添加 Loading 和 Error 处理状态。
