# What the helper can't do yet

Read together with [game-coverage.md](game-coverage.md), which `tools/game_coverage.py` regenerates from the
Steam client's `global-metadata.dat` (client build of 2026-10-24, master version 1793079161951).
The JSON next to it has the request/response field names of every game call, so a missing call can be written
without decompiling anything.

**Where we stand:** 456 API calls in the game. 118 used, 34 already defined but not wired to anything, 304 missing.
7 real-time (MagicOnion) channels, 2 handled (Temple of Illusions + errors).

## Removed by the game, not by us

| Gone | What it means for the helper |
|---|---|
| `BountyQuestEventMB` | Fountain of Prayers "x2 reward" events no longer exist: not on the server catalog, CDN returns 403, and the client itself has no class for it anymore (only two leftover text keys). The copy people pass around on Discord is an old version (events dated 12/12–12/14). Fixed by loading missing tables as empty; "no event running" is the correct answer now. |
| `LocalRaidQuestMB`, `LocalRaidEnemyMB`, `LocalRaidEventQuestGroupMB` | Temple of Illusions quest data now comes from the server (`getLocalRaidInfo`), the handler already uses that. Only the enemy details in Temple battle replays are empty. |
| `GvGServerLvMB`, `RecitationMB`, `StripePriceCurrencyCodeMB` | Unused by the helper. |
| `bookSortAssistance/dispatch`, `/reward` | Replaced by `bookSortAssistance/addAssistance` + `getInfo` (classes exist, unused). |

## Worth adding (free stuff / less clicking)

Cheap, classes already exist, just unused:
- `character/readCharacterStory` + `getCharacterStoryReward`: character story rewards (diamonds).
- `friend/rewardFriendMission`, `friend/useFriendCode`, `mission/receivePanelMissionBingoReward`.
- `bookSortAssistance/getInfo` + `addAssistance`: the new library assistance flow.
- `equipment/training`, `cast`, `castMany`: holy/magic forging.

Missing, need request classes (fields are in the JSON):
- `towerBattle/towerBattleQuick`: tower sweep instead of fighting floor by floor.
- `character/bulkLevelUp`: one call instead of the level-up loop autopilot does now.
- `bountyQuest/quick`, `getMember`/`setMember`: fountain quick finish and team presets.
- `rentalRaid/*` (9 calls): new rental raid mode, at least `receiveShareReward` and `setShareCharacter` for passive rewards.
- `equipmentSynchro/*` (7 calls): new gear sync system.
- `legendLeague/getLegendLeagueIconRewardInfo` + `buyLegendLeagueIconReward`, `starsGuidanceTradeShop/*`, `webStoreLoginCampaign/rewardLoginCampaignItem`, `popularityVote/rewardVoteItem`, `item/bulkUseItem`, `item/goldExchange`.
- `levelLink/sortMember`, `unsetMember`, `resetCoolTime`: Level Link management.
- Guild management (32 calls): mostly UI for leaders (members, recruiting, announcements). Only needed if someone runs a guild from the helper.

## Not out yet

`miningQuest/*` (10 calls) and its two tables (`MiningQuestBaseParametersMB`, `MiningQuestReinforcementMB`) are in the client
but the server doesn't ship the tables yet: an upcoming mode. `UnionMission*` looks similar. Revisit after the next update.

## Won't do

- **Anything that spends real money**: `shop/startSteamPayment`, `startStripePayment`, DMM/web crystal purchases, mission pass purchases.
- **Account-level actions**: create/delete account, link SNS/Steam/Apple, `copyPlayerData`. Too easy to lose an account.
- **Personal data**: `luckyChance/registerPersonalInfo`, real-prize forms.
- **Talking to other players**: chat messages, private messages, guild notes/surveys, reactions. A bot posting in chat gets noticed and annoys people.
- **Real-time GvG** (`IMagicOnionGvgReceiver`, Local/Global GvG battle actions): needs live decisions during the event and is where a bot is most visible. Read-only info (rankings, logs) is fine to add later.
- **Cosmetic**: music playlists, play-video comments, icon effects, background character, tutorial skips.
