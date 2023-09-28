using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("朗読")]
	public class RecitationMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("CharacterDteailVoiceMBのID")]
		public long CharacterDetailVoiceId { get; }

		[PropertyOrder(2)]
		[Description("キャラクターID")]
		public long CharacterId { get; }

		[Nest(false, 0)]
		[PropertyOrder(3)]
		[Description("朗読設定_JP")]
		public IReadOnlyList<RecitationSettingData> RecitationSettingDatasJP { get; }

		[Nest(false, 0)]
		[PropertyOrder(4)]
		[Description("朗読設定_US")]
		public IReadOnlyList<RecitationSettingData> RecitationSettingDatasUS { get; }

		[PropertyOrder(5)]
		[Description("表示開始日時")]
		public string StartTimeFixJST { get; }

		[SerializationConstructor]
		public RecitationMB(long id, bool? isIgnore, string memo, long characterId, IReadOnlyList<RecitationSettingData> recitationSettingDatasJP, IReadOnlyList<RecitationSettingData> recitationSettingDatasUS, string StartTimeFixJst, long characterDetailVoiceId)
			: base(id, isIgnore, memo)
		{
			CharacterId = characterId;
            RecitationSettingDatasJP = recitationSettingDatasJP;
            RecitationSettingDatasUS = recitationSettingDatasUS;
            StartTimeFixJST = StartTimeFixJst;
            CharacterDetailVoiceId = characterDetailVoiceId;
		}

		public RecitationMB():base(0L, null, null)
		{
			
		}

		public IReadOnlyList<RecitationSettingData> GetRecitationSettingDatas(LanguageType voiceLanguageType)
        {
            throw new NotImplementedException();
			// if (voiceLanguageType != LanguageType.jaJP && voiceLanguageType == LanguageType.enUS)
			// {
			// 	return this.<RecitationSettingDatasUS>k__BackingField;
			// }
			// return this.<RecitationSettingDatasJP>k__BackingField;
		}
	}
}
