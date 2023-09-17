# MementoMori 游戏助手

施工中

![](images/intro1.png)
![](images/intro2.png)
![](images/intro3.png)

## Todos

- [ ] 主页
    - [x] 用户登陆
    - [x] 领取每日登陆奖励
    - [x] 领取每日 VIP 礼物
    - [x] 一键发送/接收友情点
    - [x] 一键领取礼物箱
    - [x] 一键领取任务奖励
    - [x] 一键领取徽章奖励
    - [x]  一键使用固定物品
    - [x] 每四小时执行
        - 收取自动战斗奖励
        - 祈愿之泉派遣+收取
        - 公会讨伐
        - 友情点收取			
        - 任务奖励收取
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


## 使用

### 方法1
在 Android 手机上登录一次帐号, 然后获取配置文件 `/data/data/jp.boi.mementomori.android/shared_prefs/jp.boi.mementomori.android.v2.playerprefs.xml`,
重命名为 `account.xml` 放到项目目录.

### 方法2
在 Windows 上用 DMM 登录一次游戏, 然后找到注册表 `\HKEY_CURRENT_USER\Software\BankOfInnovation\MementoMori`, 拿到 UserId 和 Clientkey
- UserId: xxxxxx_Userid_hxxxxxx
- ClientKey: xxxxxx_ClientKey_hxxxxxx

双击名称, 会显示二进制数据, 把右侧的文本抄下来, 不包含引号.

然后填写到 `appsettings.user.json` 文件中, AuthOption.ClientKey 和 AuthOption.UserId.