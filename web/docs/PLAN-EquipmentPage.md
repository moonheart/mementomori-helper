# EquipmentPage 真实数据接入计划

本计划旨在将 `EquipmentPage.tsx` 从 Mock 数据重构为接入后端真实 API 数据，并同步重构相关的 UI 组件。

## 1. 核心任务

- [ ] 在 `src/lib/equipmentUtils.ts` 中定义 `UIEquipment` 组合类型。
- [ ] 更新 `src/lib/equipmentUtils.ts` 中的工具函数以支持真实枚举 (`EquipmentSlotType`, `EquipmentRarityFlags`)。
- [ ] 重构 `EquipmentCard.tsx` 组件，使其直接支持 `UIEquipment` 类型。
- [ ] 重构 `EquipmentFilters.tsx` 组件，使其支持基于真实枚举的筛选。
- [ ] 在 `EquipmentPage.tsx` 中实现数据加载、Master 数据合并及本地化逻辑。

## 2. 数据模型设计

使用 `UIEquipment` 接口合并用户实例数据和 Master 基础数据：

```typescript
export interface UIEquipment {
    guid: string;               // 唯一标识 (UserEquipmentDtoInfo.guid)
    id: number;                 // 模板ID (UserEquipmentDtoInfo.equipmentId)
    master?: EquipmentMB;       // Master 数据 (EquipmentTable)
    name: string;               // 本地化名称
    slotType: EquipmentSlotType;
    rarity: EquipmentRarityFlags;
    level: number;              // 装备基础等级
    enhanceLevel: number;       // 强化等级
    holyLevel: number;          // 圣装等级
    demonLevel: number;         // 魔装等级
    equippedBy?: string;        // 装备者名称 (通过 characterGuid 关联)
    // ... 其他属性
}
```

## 3. 映射逻辑

### 部位 (Slot)
- `EquipmentSlotType.Weapon` (1) -> 武器
- `EquipmentSlotType.Sub` (2) -> 饰品
- `EquipmentSlotType.Gauntlet` (3) -> 手部
- `EquipmentSlotType.Helmet` (4) -> 头部
- `EquipmentSlotType.Armor` (5) -> 身体
- `EquipmentSlotType.Shoes` (6) -> 腿部

### 稀有度 (Rarity)
- 将 `EquipmentRarityFlags` (D, C, B, A, S, R, SR, SSR, UR, LR) 映射到对应的 UI 颜色和名称。

### 属性名称
- 使用 `useLocalizationStore` 翻译 Master 数据中的 `nameKey`。
- 将 `BattleParameterType` 映射为本地化属性名（如：攻击力、防御力等）。

## 4. 实施流程

### 第一阶段：工具库与类型更新
1. 在 `equipmentUtils.ts` 中定义新接口。
2. 实现 `getSlotConfig`, `getRarityConfig` 等支持枚举的工具函数。
3. 更新 `calculateEquipmentPower` 以使用 DTO 字段（四维属性：肌肉、能量、智力、健康）。

### 第二阶段：组件重构
1. 修改 `EquipmentCard` 的 Props，移除对 Mock 类型的依赖。
2. 修改 `EquipmentFilters`，将筛选状态改为对应的枚举值或位标志。

### 第三阶段：页面集成
1. 调用 `ortegaApi.user.getUserData` 获取装备列表。
2. 从 `useMasterStore` 加载 `EquipmentTable` 和 `EquipmentSetTable`。
3. 执行数据合并逻辑。
4. 处理加载状态和错误处理。

## 5. 预期结果

- 页面展示真实账户持有的装备。
- 筛选和排序功能完全基于真实数据属性。
- 装备图标、名称、强化等级、神装等级显示准确。
- 支持本地化切换。
