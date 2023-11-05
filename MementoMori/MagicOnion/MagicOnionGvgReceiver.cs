using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Gvg;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.MagicOnion;

public class MagicOnionGvgReceiver: IMagicOnionGvgReceiver, IMagicOnionErrorReceiver
{
    private readonly OrtegaMagicOnionClient _client;
    private readonly Action<string> _log;

    public List<CastleInfo> CastleInfos { get; private set; } = new List<CastleInfo>();
    public bool IsCastleInfoUpdated { get; private set; }
    public OnUpdateCastlePartyResponse OnUpdateCastlePartyResponse { get; private set; }
    public OnUpdateDeployCharacterResponse OnUpdateDeployCharacterResponse { get; private set; }
    public bool IsDeployCharacterUpdated { get; set; }

    public MagicOnionGvgReceiver(OrtegaMagicOnionClient client, Action<string> log)
    {
        _client = client;
        _log = log;
    }

    public void OnAuthenticateSuccess()
    {
    }

    public void OnEndCastleBattle(OnEndCastleBattleResponse response)
    {
    }

    public void OnOpenBattleDialog(OnOpenBattleDialogResponse response)
    {
    }

    public void OnUpdateCastleParty(OnUpdateCastlePartyResponse response)
    {
        // OnUpdateCastlePartyResponse = response;
    }

    public void OnUpdateMap(OnUpdateMapResponse response)
    {
        CastleInfos = response.CastleInfos;
        IsCastleInfoUpdated = true;
    }

    public void OnUpdateDeployCharacter(OnUpdateDeployCharacterResponse response)
    {
        OnUpdateDeployCharacterResponse = response;
        IsDeployCharacterUpdated = true;
    }

    public void OnAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response)
    {
    }

    public void OnError(ErrorCode errorCode)
    {
    }
}