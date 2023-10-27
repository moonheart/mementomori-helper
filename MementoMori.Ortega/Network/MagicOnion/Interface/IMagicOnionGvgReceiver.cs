using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	public interface IMagicOnionGvgReceiver
	{
		void OnAuthenticateSuccess();

		void OnEndCastleBattle(OnEndCastleBattleResponse response);

		void OnOpenBattleDialog(OnOpenBattleDialogResponse response);

		void OnUpdateCastleParty(OnUpdateCastlePartyResponse response);

		void OnUpdateMap(OnUpdateMapResponse response);

		void OnUpdateDeployCharacter(OnUpdateDeployCharacterResponse response);

		void OnAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response);
	}
}
