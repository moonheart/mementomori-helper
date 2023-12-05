using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.MessageMatching;
using EleCho.GoCqHttpSdk.Post;
using MementoMori.BotServer.Options;
using MementoMori.Option;

namespace MementoMori.BotServer.Plugins;

[InjectSingleton]
public class AdminPlugin : CqMessageMatchPostPlugin
{
    private readonly IWritableOptions<BotOptions> _botOptions;
    private readonly SessionAccessor _sessionAccessor;

    public AdminPlugin(SessionAccessor sessionAccessor, IWritableOptions<BotOptions> botOptions)
    {
        _botOptions = botOptions;
        _sessionAccessor = sessionAccessor;
    }

    [CqMessageMatch(@"^/(?<cmd>(添加|移除))(?<current>当前)?群\s*(?<groupIdStr>\d+)?$")]
    public async Task AddGroup(CqGroupMessagePostContext context, string cmd, string current, string groupIdStr)
    {
        if (!IsSenderAdmin(context)) return;

        var groupId = string.IsNullOrEmpty(current) ? long.Parse(groupIdStr) : context.GroupId;

        _botOptions.Update(x =>
        {
            if (cmd == "添加")
            {
                if (!x.OpenedGroups.Contains(groupId))
                {
                    x.OpenedGroups.Add(groupId);
                }
            }
            else if (cmd == "移除")
            {
                if (x.OpenedGroups.Contains(groupId))
                {
                    x.OpenedGroups.Remove(groupId);
                }
            }
        });
        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage("操作成功"));
    }


    private bool IsSenderAdmin(CqGroupMessagePostContext context)
    {
        return _botOptions.Value.AdminIds.Contains(context.Sender.UserId);
    }
}