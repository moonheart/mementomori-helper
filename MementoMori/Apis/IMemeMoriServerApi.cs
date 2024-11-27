using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace MementoMori.Apis
{
    public interface IMemeMoriServerApi
    {
        [Post("/api/battlelog/report")]
        Task ReportBattleLog(List<BattleLogReq> logs);
    }

    public class BattleLogReq
    {
        public string Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public BattleType Type { get; set; }
        public int TypeInfo { get; set; }
        public string Content { get; set; }
    }
}