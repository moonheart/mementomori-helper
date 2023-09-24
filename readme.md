# MementoMori 游戏助手

施工中

![](images/intro1.png)
![](images/intro2.png)
![](images/intro3.png)
![](images/intro4.png)
![](images/intro5.png)
![](images/intro6.png)
![](images/intro7.png)
![](images/intro8.png)

## Todos

<!-- prettier-ignore -->
<table>
  <tbody>
  <tr >
      <td>
	      
- [ ] 主页
    - [x] 用户登陆
    - [x] 领取每日登陆奖励
    - [x] 领取每日 VIP 礼物
    - [x] 一键发送/接收友情点
    - [x] 一键领取礼物箱
    - [x] 一键领取任务奖励
    - [x] 一键领取徽章奖励
    - [x]  一键使用固定物品
    - [x] 每四小时执行 (3:30,7:30,11:30,15:30,19:30,23:30)
        - 收取自动战斗奖励
        - 祈愿之泉派遣+收取
        - 公会讨伐
        - 友情点收取			
        - 任务奖励收取
        - 自动抽卡 (配置请看 [下面的抽卡配置](#抽卡))
  	    - 使用固定物品
- [ ] 交换商店
    - [x] 普通物品购买
    - [x] 符石购买
- [ ] 角色
	- [x] 角色列表
	- [x] 角色属性
	- [x] 角色装备详细
	- [ ] 升级
	- [ ] 突破
    - [ ] 编组

</td>
<td>

- [ ] 储物箱
	- [x] 自动使用物品
	- [x] 自动精炼SABC装备为魔装、继承到D级别装备
	- [x] 神装自动继承到D装
	- [x] 装备自动打磨
	- [ ] 手动使用物品
- [ ] 冒险
    - [x] 领取自动战斗奖励
    - [x] 一键高速战斗
    - [x] 首领一键扫荡
    - [x] 自动刷关
- [ ] 试炼
    - [x] 无穷之塔一键扫荡
    - [x] 无穷之塔自动刷关
    - [ ] 幻影神殿一键挑战
    - [x] 古竞技场一键挑战5次
    - [x] 祈愿之泉一键全部领取
    - [x] 祈愿之泉一键远征
    - [x] 时空洞窟一键自动执行
- [ ] 抽卡
    - [x] 每日免费/金币抽卡
    - [x] 卡池列表
    - [x] 手动抽卡
- [ ] 公会
    - [x] 公会签到
    - [x] 公会讨伐战自动扫荡

</td>
</tr>

  </tbody>
</table>




## 使用

进入到发布页面：https://github.com/moonheart/mementomori-helper/releases, 然后下载 `publish-win-x64.zip` 解压。

要运行的话,你需要配置好你的账号信息. 然后就可以运行 `MementoMori.WebUI.exe` 了, 找到类似 `Now listening on: http://0.0.0.0:5290` 的日志, 打开这个地址就可以了.

进入网页之后, 先点击一次登录, 之后就可以随意操作了.

## 配置

配置文件是 `appsettings.user.json`, 你可以把 `appsettings.json` 中的配置复制到 `appsettings.user.json` 然后修改, 就可以覆盖默认配置了.

### 帐号配置

#### 方法1
在 Android 手机上登录一次帐号, 然后获取配置文件 `/data/data/jp.boi.mementomori.android/shared_prefs/jp.boi.mementomori.android.v2.playerprefs.xml`,
重命名为 `account.xml` 放到项目目录.

#### 方法2
在 Windows 上用 DMM 登录一次游戏, 然后找到注册表 `\HKEY_CURRENT_USER\Software\BankOfInnovation\MementoMori`, 拿到 UserId 和 Clientkey
- UserId: xxxxxx_Userid_hxxxxxx
- ClientKey: xxxxxx_ClientKey_hxxxxxx

双击名称, 会显示二进制数据, 把右侧的文本抄下来, ClientKey 共 32 个字母, 注意不要抄错, 不包含引号.

> # 警告! ClientKey 相当于密码, 不要泄露给别人, 不要发到群里.

然后填写到 `appsettings.user.json` 文件中, AuthOption.ClientKey 和 AuthOption.UserId.

### 时空洞窟

配置路径 `GameConfig.DungeonBattleRelicSort`

时空洞窟自动执行的时候, 会需要选择各种加成效果, 这个配置用来设置优先选择哪些加成效果, 靠前的会优先选择.

Id 是加成效果的编号, 具体哪个加成效果对应哪个 Id 可以在 [这里看](https://www.moonheartmoon.com/mementomori-masterbook/?lang=ZhTw&mb=DungeonBattleRelicMB) 

### 抽卡

配置路径 `GameConfig.GachaConfig`

在执行自动抽卡的时候, 会消耗道具, 这个配置可以指定能够消耗哪些道具来抽卡.

具体的道具类型和Id可以在 [这里看](https://www.moonheartmoon.com/mementomori-masterbook/?lang=ZhTw&mb=ItemMB) 

``` json
{
      "AutoGachaConsumeUserItems": [
        { "ItemType": "Gold", "ItemId": 1}, // 金幣
        {
          "ItemType": "GachaTicket",
          "ItemId": 1 // 聖天使的神諭召喚券
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 2 //白金召喚券
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 4 // 命運召喚券
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 5 // 黑葬武具召喚券
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 6 // 天光武具召喚券
        },
        {
          "ItemType": "GachaTicket",
          "ItemId": 7 // 禁忌武具召喚券
        },
        { "ItemType": "FriendPoint", "ItemId": 1 } // 好友點數
      ]
    }
```
