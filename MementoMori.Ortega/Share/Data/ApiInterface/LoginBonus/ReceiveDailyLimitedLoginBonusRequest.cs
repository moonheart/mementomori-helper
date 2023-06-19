using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
    [OrtegaApi("loginBonus/receiveDailyLimitedLoginBonus", true, false)]
    [MessagePackObject(true)]
    public class ReceiveDailyLimitedLoginBonusRequest : ApiRequestBase
    {
        public long LimitedLoginBonusId { get; set; }

        public int ReceiveDate { get; set; }

        public ReceiveDailyLimitedLoginBonusRequest()
        {
        }
    }
}