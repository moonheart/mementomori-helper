# BattlePage 真实数据接入计划

## 1. 目标
将 [`src/pages/BattlePage.tsx`](../src/pages/BattlePage.tsx) 中的模拟数据替换为通过 [`ortegaApi`](../src/api/ortega-client.ts) 获取的真实游戏数据，并实现相关的交互功能。

## 2. 数据源映射

| UI 模块 | 真实数据来源 | 逻辑说明 |
| :--- | :--- | :--- |
| **当前进度** | `userSyncData.userBattleBossDtoInfo.bossClearMaxQuestId` | 玩家目前通关的最高关卡 ID。 |
| **自动战斗时长** | `Date.now() - userSyncData.receivedAutoBattleRewardLastTime` | 计算自上次领取奖励以来累积的时间。 |
| **产出速率** | `QuestMB.goldPerMinute`, `QuestMB.minPlayerExp` | 根据当前通关关卡 ID 从 `MasterData` 查询每分钟收益。 |
| **累积收益** | `产出速率 * 累积分钟数` | 前端根据速率实时计算并显示的预估收益。 |
| **高速战斗** | `ortegaApi.battle.quick` | 使用快速战斗券或钻石获取 2 小时收益。 |
| **挑战首领** | `QuestMB` (ID: `maxQuestId + 1`) | 获取下一关的名称、推荐战力 (`baseBattlePower`) 等信息。 |
| **领民数据** | `QuestMB.population` + `ChapterMB` | 汇总已通关关卡的人口，并按章节的 `territoryNameKey` 进行分类显示。 |

## 3. 关键接口 (RPC)

- **领取挂机收益**: [`ortegaApi.battle.rewardAutoBattle({})`](../src/api/ortega-client.ts)
- **开始高速战斗**: [`ortegaApi.battle.quick({ itemType })`](../src/api/ortega-client.ts)
- **挑战关卡首领**: [`ortegaApi.battle.boss({ targetQuestId })`](../src/api/ortega-client.ts)

## 4. 实施步骤

### 4.1 数据预处理
- 使用 `useAccountStore` 获取当前账号的 `userSyncData`。
- 加载 `QuestMB` 和 `ChapterMB` 两个 Master 数据表。
- 使用 `useTranslation` 处理 `nameKey` 和 `territoryNameKey` 的本地化。

### 4.2 计算逻辑实现
- 实现 `useAutoBattleStats` hook，内部维护一个计时器以动态更新“已进行时长”。
- 实现人口汇总函数：
  ```typescript
  const citizenStats = chapters.map(ch => ({
    name: t(ch.territoryNameKey),
    count: quests.filter(q => q.chapterId === ch.id && q.id <= maxQuestId)
                 .reduce((sum, q) => sum + q.population, 0)
  }));
  ```

### 4.3 UI 替换
- 移除硬编码的 `mockStages`, `mockBattleStats`, `mockRescuedCitizens`。
- 动态生成关卡列表（当前关卡的前后各 5 关）。
- 将操作按钮（领取、高速、挑战）绑定到对应的 RPC 调用，并在成功后触发数据同步。

## 5. 验收标准
- [ ] 页面显示的关卡名称与当前进度一致。
- [ ] 自动战斗时间随现实时间流逝而更新。
- [ ] 领民数据正确反映了已通关章节的总人口。
- [ ] 点击“领取收益”能成功调用接口并刷新玩家金币/经验。
