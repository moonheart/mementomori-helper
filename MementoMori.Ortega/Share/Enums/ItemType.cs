using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("アイテムの種類")]
    public enum ItemType
    {
        [Description("なし")] None = 0,
        [Description("無償仮想通貨")] CurrencyFree = 1,
        [Description("有償仮想通貨")] CurrencyPaid = 2,
        [Description("ゲーム内通貨")] Gold = 3,
        [Description("武具")] Equipment = 4,
        [Description("武具の欠片")] EquipmentFragment = 5,
        [Description("キャラクター")] Character = 6,
        [Description("キャラクターの絆")] CharacterFragment = 7,
        [Description("洞窟の加護")] DungeonBattleRelic = 8,
        [Description("アダマンタイト")] EquipmentSetMaterial = 9,
        [Description("n時間分アイテム")] QuestQuickTicket = 10,
        [Description("キャラ育成素材")] CharacterTrainingMaterial = 11,
        [Description("武具強化アイテム")] EquipmentReinforcementItem = 12,
        [Description("交換所アイテム")] ExchangePlaceItem = 13,
        [Description("スフィア")] Sphere = 14,
        [Description("魔装強化アイテム")] MatchlessSacredTreasureExpItem = 15,
        [Description("ガチャチケット")] GachaTicket = 16,
        [Description("宝箱、未鑑定スフィアなど")] TreasureChest = 17,
        [Description("宝箱の鍵")] TreasureChestKey = 18,
        [Description("ボスチケット")] BossChallengeTicket = 19,
        [Description("無窮の塔チケット")] TowerBattleTicket = 20,
        [Description("回復の果実")] DungeonRecoveryItem = 21,
        [Description("プレイヤー経験値")] PlayerExp = 22,
        [Description("フレンドポイント")] FriendPoint = 23,
        [Description("生命樹の雫")] EquipmentRarityCrystal = 24,
        [Description("レベルリンク経験値")] LevelLinkExp = 25,
        [Description("ギルドストック")] GuildFame = 26,
        [Description("ギルド経験値")] GuildExp = 27,
        [Description("貢献メダル")] ActivityMedal = 28,
        [Description("パネル図鑑解放判定アイテム")] PanelGetJudgmentItem = 29,
        [Description("パネルミッション マス解放アイテム")] UnlockPanelGridItem = 30,
        [Description("パネル図鑑解放アイテム")] PanelUnlockItem = 31,
        [Description("イベント交換所アイテム")] EventExchangePlaceItem = 50
    }
}