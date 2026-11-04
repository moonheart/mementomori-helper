# What the helper can't do yet

Read together with [game-coverage.md](game-coverage.md), which `tools/game_coverage.py` regenerates from the
Steam client's `global-metadata.dat` (client build of 2026-10-24, master version 1793079161951).
The JSON next to it has the request/response field names of every game call, so a missing call can be written
without decompiling anything.

**Where we stand:** see the top of game-coverage.md (regenerated with the tool).
7 real-time (MagicOnion) channels, 2 handled (Temple of Illusions + errors).

## Removed by the game, not by us

| Gone | What it means for the helper |
|---|---|
| `BountyQuestEventMB` | Fountain of Prayers "x2 reward" events no longer exist: not on the server catalog, CDN returns 403, and the client itself has no class for it anymore (only two leftover text keys). The copy people pass around on Discord is an old version (events dated 12/12–12/14). Fixed by loading missing tables as empty; "no event running" is the correct answer now. |
| `LocalRaidQuestMB`, `LocalRaidEnemyMB`, `LocalRaidEventQuestGroupMB` | Temple of Illusions quest data now comes from the server (`getLocalRaidInfo`), the handler already uses that. Only the enemy details in Temple battle replays are empty. |
| `GvGServerLvMB`, `RecitationMB`, `StripePriceCurrencyCodeMB` | Unused by the helper. |
| `bookSortAssistance/dispatch`, `/reward` | Replaced by `bookSortAssistance/addAssistance` + `getInfo` (classes exist, unused). |

## Done (2026-10-27)

Daily actions in the **Freebies** module (`MementoMori.Api/Freebies`), on by default because they only claim:
character memories (story rewards), friend mission rewards, panel bingo lines (worked out from the 3x3 grid, no blind
requests), popularity vote rewards, rental raid share rewards, gear synchro mission rewards.
Run one by hand with `POST /api/jobs/{userId}/action/{ActionKey}`.

- Tower of Infinity sweep already existed (`InfiniteTowerHandler`, daily). It now only asks for the sweeps left today
  (it always asked for 3 and errored if any were used) and warns if diamonds went down.
- Autopilot level-up uses `character/bulkLevelUp`: one request per character instead of one per level.

## Still worth adding

- `bountyQuest/getMember` + `setMember`: fountain team presets.
- `levelLink/sortMember`, `unSetMember`: Level Link management (`resetCoolTime` probably costs diamonds, check first).
- `bookSortAssistance/getInfo` + `addAssistance` (classes exist): new library assistance flow, needs someone to look at it in game first.
- Guild management (32 calls): mostly UI for leaders (members, recruiting, announcements). Only needed if someone runs a guild from the helper.

## Left out on purpose (spends something you'd want to choose yourself)

- `webStore/*` (login campaign): the web store is the real-money shop on its own site; the game API returns 404 for it.
- `bountyQuest/quick`: finishes a fountain quest instantly for diamonds.
- `legendLeague/buyLegendLeagueIconReward`: spends Legend League coins.
- `starsGuidanceTradeShop/exchangeItem`, `equipment/training`, `cast`: spend materials.

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
