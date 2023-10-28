using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.MagicOnion;

public class MagicOnionLocalRaidNotificaiton: IMagicOnionLocalRaidNotificaiton
{
    public void OnError(ErrorCode errorCode)
    {
        throw new NotImplementedException();
    }

    public void OnReadyBattle()
    {
        throw new NotImplementedException();
    }

    public void OnStartBattle()
    {
        throw new NotImplementedException();
    }

    public void OnDisbandRoom()
    {
        throw new NotImplementedException();
    }

    public void OnRefused()
    {
        throw new NotImplementedException();
    }

    public void OnInvited(OnInviteResponse response)
    {
        throw new NotImplementedException();
    }

    public void OnJoinRoom()
    {
        throw new NotImplementedException();
    }
}