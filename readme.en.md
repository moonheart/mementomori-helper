[[Zh](readme.md)|[En](readme.en.md)]


# MementoMori Game Assistant

Under construction

[Feedback and Communication](https://t.me/+gTRe8AxKxIdkOTg9)

<table>
<tbody>
<tr><td> 

![](images/intro1.png) </td><td>![](images/intro2.png)</td></tr>
<tr><td>

![](images/intro3.png)</td><td>![](images/intro4.png)</td></tr>
<tr><td>

![](images/intro5.png)</td><td>![](images/intro6.png)</td></tr>
<tr><td>

![](images/intro7.png)</td><td>![](images/intro8.png)</td></tr>
</tbody>
</table>

## Todos

<!-- prettier-ignore -->
<table>
  <tbody>
  <tr >
      <td>

- [ ] Multi-account support
- [ ] Home
    - [x] User login
    - [x] Claim daily login rewards
    - [x] Claim daily VIP gifts
    - [x] One-click send/receive friendship points
    - [x] One-click claim gift boxes
    - [x] One-click claim task rewards
    - [x] One-click claim badge rewards
    - [x] One-click use fixed items
    - [x] Automatic claim of monthly card rewards
- [ ] Exchange Shop
    - [x] Purchase regular items
    - [x] Purchase crystals
- [ ] Characters
    - [x] Character list
    - [x] Character attributes
    - [x] Detailed character equipment
    - [ ] Level up
    - [ ] Breakthrough
    - [ ] Formation

</td>
<td>

- [ ] Storage Box
    - [x] Automatically use items
    - [x] Automatically refine SABC equipment into magical equipment, inherit to D-grade equipment
    - [x] Automatically inherit godly equipment to D-grade equipment
    - [x] Automatically polish equipment
    - [ ] Manually use items
- [ ] Adventure
    - [x] Claim auto-battle rewards
    - [x] One-click high-speed battles
    - [x] One-click boss sweep
    - [x] Automatic stage clearing
- [ ] Trials
    - [x] One-click tower sweep
    - [x] Automatic stage clearing in the tower
    - [ ] One-click challenge in the Phantom Temple
    - [x] Five automatic challenges in the Ancient Arena
    - [x] One-click claim all wishes from the Wishing Well
    - [x] One-click expedition at the Wishing Well
        - [x] Optionally assign tasks with specified reward items only
    - [x] One-click automatic execution in the Time-Space Cave
        - [x] Optionally automatically purchase specified items
- [ ] Gacha
    - [x] Daily free/gold coin gacha
    - [x] Gacha pool list
    - [x] Manual gacha
- [ ] Guild
    - [x] Guild sign-in
    - [x] Guild battle automatic sweep
    - [x] Auto open Guild Raid
    - [x] Auto receive Gvg Reward

</td>
</tr>

  </tbody>
</table>

## Usage

Go to the release page: https://github.com/moonheart/mementomori-helper/releases, and then download `publish-win-x64.zip` and unzip it.

### Method 1: Run Directly

To run, you need to configure your account information. Then you can run `MementoMori.WebUI.exe`. Find something like `Now listening on: http://0.0.0.0:5290` in the logs and open this address.

### Method 2: Run with Docker

```yaml
version: '3'
services:
  mementomori:
    image: moonheartmoon/mementomori-webui:v1
    container_name: mementomori
    restart: unless-stopped
    privileged: false
    ports:
      - "5290:80"
    environment:
      - TZ=Asia/Shanghai
    volumes:
      - ./Master/:/app/Master/
      - ${PWD}/account.xml:/app/account.xml:rw
      - ${PWD}/appsettings.user.json:/app/appsettings.user.json:rw
```

After entering the web page, click login once, and then you can operate as you like.

## Automatic Tasks

The program will execute specific operations at specific times.

### Daily Tasks (Server time: 4:10)

- Claim daily login rewards
- Claim daily VIP rewards
- Claim automatic battle rewards
- Send friendship points
- Claim gift boxes
- Enhance equipment once (automatically select the lowest-level equipment currently equipped by characters for daily tasks) (See automatic task configuration for details)
- Clear main stages 3 times
- Sweep the Infinite Tower 3 times
- Free high-speed battle (once for free, once for monthly card)
- Guild sign-in
- Guild battle automatic sweep
- Claim rewards from the Wishing Well
- Automatically dispatch tasks at the Wishing Well
- Automatic execution in the Time-Space Cave (See automatic task configuration for details)
- Claim daily/weekly task rewards
- Use fixed items (See automatic task configuration for details)
- Consume items for gacha (See gacha configuration below) (See automatic task configuration for details)
- Automatically enhance characters (R->R+, SR->SR+) (See automatic task configuration for details)

### Scheduled Reward Claim (Server time: 0:30, 4:30, 8:30, 12:30, 16:30, 20:30)

- Claim automatic battle rewards
- Wishing Well dispatch and claim
- Guild battle
- Claim friendship points
- Claim task rewards
- Claim GvG rewards
- Automatic gacha (See gacha configuration below) (See automatic task configuration for details)
- Use fixed items (See automatic task configuration for details)

### Arena (Server time: 20:00)

- Arena 5 times (automatically select opponents with the lowest power from the list) (See automatic task configuration for details)
- Will stop other tasks currently running (e.g., story mode)

## Configuration

The configuration file is `appsettings.user.json`, you can copy the configuration from `appsettings.json` to `appsettings.user.json` and make changes to override the default configuration.

Here's an example of `appsettings.user.json`:

```json5
{
  "AuthOption": {
    "ClientKey": "", // Fill in your client key here
    "DeviceToken": "",
    "AppVersion": "1.4.4",
    "OSVersion": "Android OS 13 / API-33 (TKQ1.220829.002/V14.0.12.0.TLACNXM)",
    "ModelName": "Xiaomi 2203121C",
    "UserId": 0 // Change this 0 to your user ID
  }
}
```

### Account Configuration



#### Method 1:
Log in once on an Android phone, then retrieve the configuration file `/data/data/jp.boi.mementomori.android/shared_prefs/jp.boi.mementomori.android.v2.playerprefs.xml`, rename it to `account.xml`, and place it in the project directory.

You can also manually find the UserId and Clientkey as in Method 2 and fill them in `appsettings.user.json`.

#### Method 2:
Log in once with DMM on Windows, then find the registry key `\HKEY_CURRENT_USER\Software\BankOfInnovation\MementoMori`. Retrieve UserId and Clientkey.

- UserId: xxxxxx_Userid_hxxxxxx
- ClientKey: xxxxxx_ClientKey_hxxxxxx

Double-click the name, and binary data will be displayed. Copy the text on the right side; ClientKey consists of 32 letters, be careful not to copy it incorrectly, and do not share it with others or post it in groups.

Then, fill them into `appsettings.user.json`, AuthOption.ClientKey and AuthOption.UserId.

### Time-Space Cave

Configuration path: `GameConfig.DungeonBattleRelicSort`

When executing the Time-Space Cave automatically, you need to choose various bonus effects. This configuration is used to set the priority for selecting which bonus effects. Those listed first will be selected first.

The Id is the number of the bonus effect, and you can see which bonus effect corresponds to which Id [here](https://www.moonheartmoon.com/mementomori-masterbook/?lang=ZhTw&mb=DungeonBattleRelicMB).

#### Automatic Shop Purchase

Configuration path: `GameConfig.DungeonBattle`

When encountering a mysterious stall, it will check if there are specified items to buy automatically.

```json5
{
  "GameConfig": {
    "DungeonBattle": {
      "ShopTargetItems": [
        { "ItemType": "EquipmentRarityCrystal","ItemId": 1 } // Life Tree Dew
      ]
    }
  }
}
```

### Wishing Well

Configuration path: `GameConfig.BountyQuestAuto`

You can configure to dispatch only tasks with specified reward items. If not specified, it will dispatch all tasks. If you want to dispatch other tasks, you can enable forced dispatch on the page.

```json5
{
  "GameConfig": {
    "BountyQuestAuto": {
      "TargetItems": [
        {"ItemType": "CharacterTrainingMaterial", "ItemId": 2}, // Potential Bead
        {"ItemType": "CurrencyFree", "ItemId": 1}, // Diamonds
      ]
    } 
  }
}
```

### Gacha

Configuration path: `GameConfig.GachaConfig`

When performing automatic gacha, it will consume items. This configuration can specify which items can be consumed for gacha.

Specific item types and Ids can be found [here](https://www.moonheartmoon.com/mementomori-masterbook/?lang=ZhTw&mb=ItemMB).

```json5
{
      "AutoGachaConsumeUserItems": [
        { "ItemType": "Gold", "ItemId": 1}, // Gold
        {
          "ItemType": "GachaTicket",
          "ItemId": 1 // Holy Angel's Divine Summon Ticket
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 2 // Platinum Summon Ticket
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 4 // Fate Summon Ticket
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 5 // Black Funeral Equipment Summon Ticket
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 6 // Radiant Equipment Summon Ticket
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 7 // Forbidden Equipment Summon Ticket
        },
        { "ItemType": "FriendPoint", "ItemId": 1 } // Friendship Points
      ]
    }
```

### Automatic Tasks, One-Click Execute All

Configuration path: `GameConfig.AutoJob`

Here's an example:

```json5
{
  "AuthOption": {
    "ClientKey": "", // Fill in your client key here
    "DeviceToken": "",
    "AppVersion": "1.4.4",
    "OSVersion": "Android OS 13 / API-33 (TKQ1.220829.002/V14.0.12.0.TLACNXM)",
    "ModelName": "Xiaomi 2203121C",
    "UserId": 0 // Change this 0 to your user ID
  },
  "GameConfig": {
    "AutoJob": {
      "DisableAll": false, // Disable all automatic tasks
      "AutoReinforcementEquipmentOneTime": true, // Enhance equipment once
      "AutoPvp": true, // Auto-arena
      "AutoDungeonBattle": true, // Auto Time-Space Cave
      "AutoUseItems": true, // Auto-use fixed items
      "AutoFreeGacha": true, // Auto free gacha
      "AutoRankUpCharacter": true, // Auto merge characters
      "DailyJobCron": "0 10 4 ? * *", // Daily task execution time, default is 4:10
      "HourlyJobCron": "0 30 0,4,8,12,16,20 ? * *", // Scheduled task execution time, default is 0:30,4:30,8:30,12:30,16:30,20:30
      "PvpJobCron": "0 0 20 ? * *"// Arena task execution time, default is 20:00
    }
  }
}
```

### Rate Limiting

Configuration path: `GameConfig.AutoRequestDelay`

When automatically fighting bosses or sweeping the Infinite Tower, rate limiting may be triggered. You can set the waiting time after each request. The default is 0, which means no limitation. The unit is milliseconds. The example below changes it to 100 milliseconds.

```json5
{
  "AuthOption": {
    "ClientKey": "", // Fill in your client key here
    "DeviceToken": "",
    "AppVersion": "1.4.4",
    "OSVersion": "Android OS 13 / API-33 (TKQ1.220829.002/V14.0.12.0.TLACNXM)",
    "ModelName": "Xiaomi 2203121C",
    "UserId": 0 // Change this 0 to your user ID
  },
  "GameConfig": {
    "AutoRequestDelay": 100
  }
}
```

### Changing Port Number for Multiple Instances

Modify the port number in `appsettings.user.json`, for example, change the port to 5700:

```json5
{
  "AuthOption": {
    "ClientKey": "", // Fill in your client key here
    "DeviceToken": "",
    "AppVersion": "1.4.4",
    "OSVersion": "Android OS 13 / API-33 (TKQ1.220829.002/V14.0.12.0.TLACNXM)",
    "ModelName": "Xiaomi 2203121C",
    "UserId": 0 // Change this 0 to your user ID
  },
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5700"
      }
    }
  }
}
```
