// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Runtime.CompilerServices;
// using Cpp2IlInjected;
// using MementoMori.Ortega.Share.Data.Auth;
// using MementoMori.Ortega.Share.Enums;
// using Ortega.Common.Data;
// using Ortega.Common.MasterData;
// using Ortega.Common.Utils;
// using Ortega.Common.ViewController;
// using Ortega.Share.Extensions;
// using Ortega.Share.Utils;
// using UnityEngine;
//
// namespace Ortega.Common.Manager
// {
// 	public class GameManager
// 	{
// 		public static string AssetFullUrl { get; set; }
//
// 		public string ApiHost{ get; set; }
//
// 		public string AssetCatalogFixedUriFormat{ get; set; }
//
// 		public string AssetVersion{ get; set; }
//
// 		public AppAssetVersionInfo AppAssetVersionInfo{ get; set; }
//
// 		public string BaseApiUri{ get; set; }
//
// 		public string AuthApiUri{ get; set; }
//
// 		public string NoticeBannerImageUriFormat{ get; set; }
//
// 		public OrtegaEnvironment Environment
// 		{
// 			get;
// 			private set;
// 		}
//
// 		public LanguageType CurrentLanguageType
// 		{
// 			get;
// 			private set;
// 		} = (LanguageType)((ulong)1L);
//
// 		public LanguageType CurrentVoiceLanguageType
// 		{
// 			get;
// 			private set;
// 		} = (LanguageType)((ulong)1L);
//
// 		public ResolutionType CurrentResolutionType
// 		{
// 			get;
// 			private set;
// 		} = (ResolutionType)((ulong)3L);
//
// 		public bool IsNewStartUp
// 		{
// 			get;
// 			set;
// 		}
//
// 		public string MagicOnionHost{ get; set; }
//
// 		public int MagicOnionPort
// 		{
// 			get;
// 			set;
// 		}
//
// 		public string MasterUriFormat{ get; set; }
//
// 		public string MasterVersion{ get; set; }
//
// 		public long MyPageCharacterId
// 		{
// 			get;
// 			private set;
// 		}
//
// 		public long MyPageNextCharacterId
// 		{
// 			get;
// 			private set;
// 		}
//
// 		public string AdvertisingId{ get; set; }
//
// 		public Dictionary<long, CharacterTouchActionData> MyPageTouchActionDatas{ get; set; }
//
// 		public string UUID{ get; set; }
//
// 		public IReadOnlyList<MaintenanceDebugUserInfo> MaintenanceDebugUserInfos{ get; set; }
//
// 		public IReadOnlyList<MaintenanceInfo> MaintenanceInfos{ get; set; }
//
// 		public IReadOnlyList<ManagementNewUserInfo> ManagementNewUserInfos{ get; set; }
//
// 		public IReadOnlyList<WorldInfo> WorldInfos{ get; set; }
//
// 		public void Init()
// 		{
// 			GameManager.<AssetFullUrl>k__BackingField = string.Empty;
// 			string empty = string.Empty;
// 			this.<MasterUriFormat>k__BackingField = empty;
// 			string empty2 = string.Empty;
// 			this.<NoticeBannerImageUriFormat>k__BackingField = empty2;
// 			string empty3 = string.Empty;
// 			this.<MasterVersion>k__BackingField = empty3;
// 			string empty4 = string.Empty;
// 			this.<AssetVersion>k__BackingField = empty4;
// 			string empty5 = string.Empty;
// 			this.<MagicOnionHost>k__BackingField = empty5;
// 			int num = 0;
// 			this.<MagicOnionPort>k__BackingField = num;
// 			string empty6 = string.Empty;
// 			this.<AdvertisingId>k__BackingField = empty6;
// 			this.<UUID>k__BackingField = typeof(string).TypeHandle;
// 			this.<MyPageCharacterId>k__BackingField = (long)num;
// 			this.<MyPageNextCharacterId>k__BackingField = (long)num;
// 			Dictionary<long, CharacterTouchActionData> dictionary = new Dictionary();
// 			this.<MyPageTouchActionDatas>k__BackingField = dictionary;
// 			this.<IsNewStartUp>k__BackingField = true;
// 			this.InitVolume();
// 			ulong num2;
// 			bool staticValue = SingletonMonoBehaviour.Instance.GetStaticValue<bool>("GameSettingTapEffect", num2 != 0UL);
// 			this.<IsTapEffect>k__BackingField = staticValue;
// 			ulong num3;
// 			bool staticValue2 = SingletonMonoBehaviour.Instance.GetStaticValue<bool>("GameSettingCurrency", num3 != 0UL);
// 			this.<IsCurrencyConsumptionConfirmation>k__BackingField = staticValue2;
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 			FpsType fpsType;
// 			this.<FpsType>k__BackingField = fpsType;
// 			PlayerPrefsManager instance2 = SingletonMonoBehaviour.Instance;
// 			int num4 = 0;
// 			bool staticValue3 = instance2.GetStaticValue<bool>("GameSettingLiveMode", num4 != 0);
// 			FpsType fpsType2 = this.<FpsType>k__BackingField;
// 			this.<IsLiveMode>k__BackingField = staticValue3;
// 			if (fpsType2 == FpsType.Fps30 || fpsType2 == FpsType.Fps30 || fpsType2 == FpsType.Fps45)
// 			{
// 				uint num5;
// 				Application.targetFrameRate = (int)num5;
// 			}
// 			Dictionary<AppNotificationType, bool> dictionary2 = new Dictionary();
// 			this._appNotificationDict = dictionary2;
// 			AppNotificationType[] valueArray = EnumUtil.GetValueArray<AppNotificationType>();
// 			if (num < valueArray.Length)
// 			{
// 				AppNotificationType appNotificationType = valueArray[num];
// 				PlayerPrefsManager instance3 = SingletonMonoBehaviour.Instance;
// 				Dictionary<AppNotificationType, bool> appNotificationDict = this._appNotificationDict;
// 				num++;
// 			}
// 			this.<CurrentLanguageType>k__BackingField = (LanguageType)((ulong)1L);
// 			this.<CurrentVoiceLanguageType>k__BackingField = (LanguageType)((ulong)1L);
// 			PlayerPrefsManager instance4 = SingletonMonoBehaviour.Instance;
// 			ResolutionType resolutionType;
// 			this.<CurrentResolutionType>k__BackingField = resolutionType;
// 		}
//
// 		public void ChangeResolution()
// 		{
// 			ResolutionType[] valueArray = EnumUtil.GetValueArray<ResolutionType>();
// 			int num = 0;
// 			int num2 = Math.Max(num2, num);
// 			int x = ClientConst.Common.ResoultionSizes[num2].m_X;
// 		}
//
// 		public void SetDefaultEnvironment()
// 		{
// 			OrtegaEnvironment connectionType = ClientConst.App.ConnectionType;
// 			int num = 0;
// 			this.<Environment>k__BackingField = connectionType;
// 			string empty = string.Empty;
// 			this.<BaseApiUri>k__BackingField = empty;
// 			string empty2 = string.Empty;
// 			this.<AuthApiUri>k__BackingField = empty2;
// 			Dictionary<OrtegaEnvironment, string> gameApiUrlDictionary = ClientConst.App.GameApiUrlDictionary;
// 			OrtegaEnvironment ortegaEnvironment = this.<Environment>k__BackingField;
// 			if (gameApiUrlDictionary.TryGetValue(ortegaEnvironment, num))
// 			{
// 				this.<BaseApiUri>k__BackingField = num;
// 			}
// 			Dictionary<OrtegaEnvironment, string> authApiUrlDictionary = ClientConst.App.AuthApiUrlDictionary;
// 			OrtegaEnvironment ortegaEnvironment2 = this.<Environment>k__BackingField;
// 			if (authApiUrlDictionary.TryGetValue(ortegaEnvironment2, num))
// 			{
// 				this.<AuthApiUri>k__BackingField = num;
// 			}
// 			OrtegaEnvironment ortegaEnvironment3 = this.<Environment>k__BackingField;
// 			string text = string.Format("Changed Environment : {0}", ortegaEnvironment3);
// 		}
//
// 		public void ChangeEnvironment(OrtegaEnvironment environment)
// 		{
// 			int num = 0;
// 			this.<Environment>k__BackingField = environment;
// 			string empty = string.Empty;
// 			this.<BaseApiUri>k__BackingField = empty;
// 			string empty2 = string.Empty;
// 			this.<AuthApiUri>k__BackingField = empty2;
// 			Dictionary<OrtegaEnvironment, string> gameApiUrlDictionary = ClientConst.App.GameApiUrlDictionary;
// 			OrtegaEnvironment ortegaEnvironment = this.<Environment>k__BackingField;
// 			if (gameApiUrlDictionary.TryGetValue(ortegaEnvironment, num))
// 			{
// 				this.<BaseApiUri>k__BackingField = num;
// 			}
// 			Dictionary<OrtegaEnvironment, string> authApiUrlDictionary = ClientConst.App.AuthApiUrlDictionary;
// 			OrtegaEnvironment ortegaEnvironment2 = this.<Environment>k__BackingField;
// 			if (authApiUrlDictionary.TryGetValue(ortegaEnvironment2, num))
// 			{
// 				this.<AuthApiUri>k__BackingField = num;
// 			}
// 			OrtegaEnvironment ortegaEnvironment3 = this.<Environment>k__BackingField;
// 			string text = string.Format("Changed Environment : {0}", ortegaEnvironment3);
// 		}
//
// 		public void CreateCacheFolders()
// 		{
// 			string text = Path.Combine(Application.persistentDataPath, "Master");
// 			if (!Directory.Exists(text))
// 			{
// 				DirectoryInfo directoryInfo = Directory.CreateDirectory(text);
// 			}
// 			string text2 = Path.Combine(Application.persistentDataPath, "AssetBundle");
// 			if (!Directory.Exists(text2))
// 			{
// 				DirectoryInfo directoryInfo2 = Directory.CreateDirectory(text2);
// 				return;
// 			}
// 		}
//
// 		public string GetCountryCode()
// 		{
// 			return CountryUtil.GetCountryCode();
// 		}
//
// 		public string GetAssetBundleCachePath()
// 		{
// 			return Path.Combine(Application.persistentDataPath, "AssetBundle");
// 		}
//
// 		public string GetRemoteAssetCatalogPath()
// 		{
// 			return Path.Combine(Application.persistentDataPath, "RemoteAssetCatalog");
// 		}
//
// 		public string GetMasterCachePath()
// 		{
// 			return Path.Combine(Application.persistentDataPath, "Master");
// 		}
//
// 		public float SetTimeScale(float timeScale)
// 		{
// 			Time.timeScale = timeScale;
// 			float timeScale2 = Time.timeScale;
// 			string text = string.Format("GameManager -> TimeScale is changed : {0}", "GameManager -> TimeScale is changed : {0}");
// 			return timeScale;
// 		}
//
// 		public void SetSleepEnable(bool enable)
// 		{
// 			Screen.sleepTimeout = (enable ? 1 : 0);
// 			string text = string.Format("GameManager -> Sleep is changed : {0}", "GameManager -> Sleep is changed : {0}");
// 		}
//
// 		public Ortega.Share.Enums.DeviceType GetDeviceType()
// 		{
// 			return Ortega.Share.Enums.DeviceType.DmmGames;
// 		}
//
// 		public bool IsFirstStart()
// 		{
// 			return SingletonMonoBehaviour.Instance.GetStaticValue<bool>("FirstStart", true);
// 		}
//
// 		public void DisableFirstStart()
// 		{
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 			int num = 0;
// 			bool flag = instance.SetStaticValue<bool>("FirstStart", num != 0);
// 		}
//
// 		public void ChangeFavoriteCharacter(List<long> characterIds)
// 		{
// 			ulong num;
// 			do
// 			{
// 				Dictionary<long, CharacterTouchActionData> dictionary = this.<MyPageTouchActionDatas>k__BackingField;
// 				Dictionary<long, CharacterTouchActionData> dictionary2 = new Dictionary();
// 				this.<MyPageTouchActionDatas>k__BackingField = dictionary2;
// 				bool flag;
// 				if (flag)
// 				{
// 					bool flag2;
// 					while (!flag2)
// 					{
// 					}
// 					Dictionary<long, CharacterTouchActionData> dictionary3 = this.<MyPageTouchActionDatas>k__BackingField;
// 				}
// 			}
// 			while (num != (ulong)0L);
// 		}
//
// 		public void SetMyPageCharacterId(long characterId, List<long> favoriteUserCharacterIds)
// 		{
// 			int num = 0;
// 			this.<MyPageCharacterId>k__BackingField = characterId;
// 			this.<MyPageNextCharacterId>k__BackingField = (long)num;
// 			int num2 = favoriteUserCharacterIds.IndexOf(characterId);
// 			num2++;
// 			long num3 = favoriteUserCharacterIds[num2];
// 			this.<MyPageNextCharacterId>k__BackingField = num3;
// 		}
//
// 		public void OpenStore()
// 		{
// 			Application.OpenURL(LocalTextResourceTable.Get("[DmmStoreUrl]"));
// 		}
//
// 		public void Quit()
// 		{
// 			Application.Quit();
// 		}
//
// 		private string GetUUID()
// 		{
// 		}
//
// 		public float BgmVolume
// 		{
// 			get
// 			{
// 				int num = 0;
// 				return this.GetVolume((VolumeType)num);
// 			}
// 		}
//
// 		public float AmbVolume
// 		{
// 			get
// 			{
// 				int num = 0;
// 				return this.GetVolume((VolumeType)num);
// 			}
// 		}
//
// 		public float SeVolume
// 		{
// 			get
// 			{
// 				return this.GetVolume(VolumeType.Se);
// 			}
// 		}
//
// 		public float JingleVolume
// 		{
// 			get
// 			{
// 				return this.GetVolume(VolumeType.Se);
// 			}
// 		}
//
// 		public float VoiceVolume
// 		{
// 			get
// 			{
// 				return this.GetVolume(VolumeType.Voice);
// 			}
// 		}
//
// 		private void InitVolume()
// 		{
// 			Dictionary<VolumeType, int> dictionary = new Dictionary();
// 			this._volumeSettings = dictionary;
// 			Dictionary<VolumeType, string>.KeyCollection keys = ClientConst.Sound.VolumePlayerPrefsKey.Keys;
// 			throw new NullReferenceException();
// 		}
//
// 		public void SetVolumeSetting(VolumeType volumeType, int value)
// 		{
// 			Dictionary<VolumeType, string> volumePlayerPrefsKey = ClientConst.Sound.VolumePlayerPrefsKey;
// 			bool flag;
// 			if (flag)
// 			{
// 				PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 				Dictionary<VolumeType, int> volumeSettings = this._volumeSettings;
// 				SoundManager instance2 = SingletonMonoBehaviour.Instance;
// 			}
// 		}
//
// 		public int GetVolumeSetting(VolumeType volumeType)
// 		{
// 			Dictionary<VolumeType, int> volumeSettings = this._volumeSettings;
// 			int num;
// 			return num;
// 		}
//
// 		public float GetVolume(VolumeType volumeType)
// 		{
// 			Dictionary<VolumeType, bool> muteSettings = this._muteSettings;
// 			bool flag;
// 			if (!flag)
// 			{
// 				Dictionary<VolumeType, int> volumeSettings = this._volumeSettings;
// 				float num;
// 				return num;
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public void SetMute(VolumeType volumeType, bool value)
// 		{
// 			Dictionary<VolumeType, string> mutePlayerPrefsKey = ClientConst.Sound.MutePlayerPrefsKey;
// 			bool flag;
// 			if (flag)
// 			{
// 				PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 				Dictionary<VolumeType, bool> muteSettings = this._muteSettings;
// 				SoundManager instance2 = SingletonMonoBehaviour.Instance;
// 			}
// 		}
//
// 		public bool IsMute(VolumeType volumeType)
// 		{
// 			Dictionary<VolumeType, bool> muteSettings = this._muteSettings;
// 			bool flag;
// 			return flag;
// 		}
//
// 		public bool IsTapEffect
// 		{
// 			get;
// 			private set;
// 		}
//
// 		public bool IsCurrencyConsumptionConfirmation
// 		{
// 			get;
// 			private set;
// 		}
//
// 		public FpsType FpsType
// 		{
// 			get;
// 			private set;
// 		}
//
// 		public bool IsLiveMode
// 		{
// 			get;
// 			private set;
// 		}
//
// 		private void InitSystem()
// 		{
// 			ulong num;
// 			bool staticValue = SingletonMonoBehaviour.Instance.GetStaticValue<bool>("GameSettingTapEffect", num != 0UL);
// 			this.<IsTapEffect>k__BackingField = staticValue;
// 			ulong num2;
// 			bool staticValue2 = SingletonMonoBehaviour.Instance.GetStaticValue<bool>("GameSettingCurrency", num2 != 0UL);
// 			this.<IsCurrencyConsumptionConfirmation>k__BackingField = staticValue2;
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 			FpsType fpsType;
// 			this.<FpsType>k__BackingField = fpsType;
// 			PlayerPrefsManager instance2 = SingletonMonoBehaviour.Instance;
// 			int num3 = 0;
// 			bool staticValue3 = instance2.GetStaticValue<bool>("GameSettingLiveMode", num3 != 0);
// 			FpsType fpsType2 = this.<FpsType>k__BackingField;
// 			this.<IsLiveMode>k__BackingField = staticValue3;
// 			if (fpsType2 == FpsType.Fps30)
// 			{
// 				Application.targetFrameRate = 30;
// 				return;
// 			}
// 			if (fpsType2 == FpsType.Fps30)
// 			{
// 				Application.targetFrameRate = 45;
// 				return;
// 			}
// 			if (fpsType2 == FpsType.Fps45)
// 			{
// 				Application.targetFrameRate = 60;
// 				return;
// 			}
// 		}
//
// 		public void ResetSystem()
// 		{
// 			this.<IsTapEffect>k__BackingField = true;
// 			bool flag = SingletonMonoBehaviour.Instance.SetStaticValue<bool>("GameSettingTapEffect", true);
// 			CommonTouchParticleViewController touchParticleViewController = StaticAccess.TouchParticleViewController;
// 			bool flag2 = this.<IsTapEffect>k__BackingField;
// 			touchParticleViewController.SetActive(flag2);
// 			this.<IsCurrencyConsumptionConfirmation>k__BackingField = true;
// 			bool flag3 = SingletonMonoBehaviour.Instance.SetStaticValue<bool>("GameSettingCurrency", true);
// 			this.<FpsType>k__BackingField = (FpsType)((ulong)2L);
// 			FpsType fpsType = SingletonMonoBehaviour.Instance.SetStaticValue<FpsType>("GameSettingFps", (FpsType)((uint)2));
// 			Application.targetFrameRate = 60;
// 			this.<IsLiveMode>k__BackingField = false;
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 			int num = 0;
// 			bool flag4 = instance.SetStaticValue<bool>("GameSettingLiveMode", num != 0);
// 		}
//
// 		public void SetTapEffect(bool isOn)
// 		{
// 			this.<IsTapEffect>k__BackingField = isOn;
// 			bool flag = SingletonMonoBehaviour.Instance.SetStaticValue<bool>("GameSettingTapEffect", isOn);
// 			CommonTouchParticleViewController touchParticleViewController = StaticAccess.TouchParticleViewController;
// 			bool flag2 = this.<IsTapEffect>k__BackingField;
// 			touchParticleViewController.SetActive(flag2);
// 		}
//
// 		public void SetCurrencyConsumptionConfirmation(bool isOn)
// 		{
// 			this.<IsCurrencyConsumptionConfirmation>k__BackingField = isOn;
// 			bool flag = SingletonMonoBehaviour.Instance.SetStaticValue<bool>("GameSettingCurrency", isOn);
// 		}
//
// 		public void SetFps(FpsType fpsType)
// 		{
// 			this.<FpsType>k__BackingField = fpsType;
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 			if (fpsType == FpsType.Fps30)
// 			{
// 				Application.targetFrameRate = 30;
// 				return;
// 			}
// 			if (fpsType != FpsType.Fps30)
// 			{
// 				if (fpsType == FpsType.Fps45)
// 				{
// 				}
// 				return;
// 			}
// 			Application.targetFrameRate = 45;
// 		}
//
// 		private void SetFrameRate(FpsType fpsType)
// 		{
// 			if (fpsType == FpsType.Fps30)
// 			{
// 				uint num;
// 				Application.targetFrameRate = (int)num;
// 				return;
// 			}
// 			if (fpsType == FpsType.Fps30)
// 			{
// 				uint num2;
// 				Application.targetFrameRate = (int)num2;
// 				return;
// 			}
// 			if (fpsType == FpsType.Fps45)
// 			{
// 				return;
// 			}
// 		}
//
// 		public void SetLiveMode(bool isOn)
// 		{
// 			this.<IsLiveMode>k__BackingField = isOn;
// 			bool flag = SingletonMonoBehaviour.Instance.SetStaticValue<bool>("GameSettingLiveMode", isOn);
// 		}
//
// 		private void InitLanguage()
// 		{
// 			this.<CurrentLanguageType>k__BackingField = (LanguageType)((ulong)1L);
// 			this.<CurrentVoiceLanguageType>k__BackingField = (LanguageType)((ulong)1L);
// 		}
//
// 		public void SetLanguageType(LanguageType languageType)
// 		{
// 			throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
// 		}
//
// 		public void SetVoiceLanguageType(LanguageType languageType)
// 		{
// 			throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
// 		}
//
// 		private void InitNotification()
// 		{
// 			Dictionary<AppNotificationType, bool> dictionary = new Dictionary();
// 			this._appNotificationDict = dictionary;
// 			AppNotificationType[] valueArray = EnumUtil.GetValueArray<AppNotificationType>();
// 			int num = 0;
// 			if (num < valueArray.Length)
// 			{
// 				AppNotificationType appNotificationType = valueArray[num];
// 				PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 				Dictionary<AppNotificationType, bool> appNotificationDict = this._appNotificationDict;
// 				num++;
// 			}
// 		}
//
// 		public void ResetNotificationSettings()
// 		{
// 			Dictionary<AppNotificationType, bool> dictionary = new Dictionary();
// 			this._appNotificationDict = dictionary;
// 			AppNotificationType[] valueArray = EnumUtil.GetValueArray<AppNotificationType>();
// 			int num = 0;
// 			if (num < valueArray.Length)
// 			{
// 				AppNotificationType appNotificationType = valueArray[num];
// 				PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 				Dictionary<AppNotificationType, bool> appNotificationDict = this._appNotificationDict;
// 				num++;
// 			}
// 		}
//
// 		public bool GetNotificationValue(AppNotificationType appNotificationType)
// 		{
// 			Dictionary<AppNotificationType, bool> appNotificationDict = this._appNotificationDict;
// 			bool flag;
// 			return flag;
// 		}
//
// 		public void SetNotificationValue(AppNotificationType appNotificationType, bool isOn)
// 		{
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 			Dictionary<AppNotificationType, bool> appNotificationDict = this._appNotificationDict;
// 		}
//
// 		public List<ManagementNewUserInfo> GetOpenManagementNewUserInfosByManagementNewUserType(DateTime nowUtcDateTime, ManagementNewUserType managementNewUserType)
// 		{
// 			List<ManagementNewUserInfo> list;
// 			int num;
// 			do
// 			{
// 				list = new List();
// 				if (this.<ManagementNewUserInfos>k__BackingField.IsNullOrEmpty<ManagementNewUserInfo>())
// 				{
// 					break;
// 				}
// 				IReadOnlyList<ManagementNewUserInfo> readOnlyList = this.<ManagementNewUserInfos>k__BackingField;
// 				num = 0;
// 				DateTime dateTime;
// 				if (dateTime != 0)
// 				{
// 					uint num2;
// 					if (num < (int)num2)
// 					{
// 						num += num;
// 						if (num == (int)num2)
// 						{
// 							goto IL_51;
// 						}
// 						num++;
// 					}
// 					if (!(dateTime <= dateTime) || !(dateTime <= dateTime))
// 					{
// 						continue;
// 					}
// 					IL_51:
// 					list += list;
// 				}
// 				if ("{il2cpp array field local12->}" != (ulong)0L)
// 				{
// 				}
// 			}
// 			while (num != 0);
// 			return list;
// 		}
//
// 		public List<MaintenanceInfo> GetOpenMaintenanceInfos(DateTime nowUtcDateTime)
// 		{
// 			List<MaintenanceInfo> list;
// 			int num;
// 			do
// 			{
// 				list = new List();
// 				if (this.<MaintenanceInfos>k__BackingField.IsNullOrEmpty<MaintenanceInfo>())
// 				{
// 					break;
// 				}
// 				IReadOnlyList<MaintenanceInfo> readOnlyList = this.<MaintenanceInfos>k__BackingField;
// 				num = 0;
// 				DateTime dateTime;
// 				if (dateTime != 0)
// 				{
// 					uint num2;
// 					if (num < (int)num2)
// 					{
// 						num += num;
// 						if (num == (int)num2)
// 						{
// 							goto IL_47;
// 						}
// 						num++;
// 					}
// 					if (!(dateTime <= dateTime) || !(dateTime <= dateTime))
// 					{
// 						continue;
// 					}
// 					IL_47:
// 					list += list;
// 				}
// 				if ("{il2cpp array field local10->}" != (ulong)0L)
// 				{
// 				}
// 			}
// 			while (num != 0);
// 			return list;
// 		}
//
// 		public WorldInfo GetWorldInfoById(long id)
// 		{
// 			int num;
// 			do
// 			{
// 				bool flag = this.<WorldInfos>k__BackingField.IsNullOrEmpty<WorldInfo>();
// 				if (flag)
// 				{
// 					break;
// 				}
// 				IReadOnlyList<WorldInfo> readOnlyList = this.<WorldInfos>k__BackingField;
// 				num = 0;
// 				if (flag)
// 				{
// 					uint num2;
// 					if (num >= (int)num2)
// 					{
// 						goto IL_2D;
// 					}
// 					num += num;
// 					if (num != (int)num2)
// 					{
// 						num++;
// 						goto IL_2D;
// 					}
// 					IL_30:
// 					int num3;
// 					num3 += num3;
// 					goto IL_37;
// 					IL_2D:
// 					num3 = 0;
// 					goto IL_30;
// 				}
// 				IL_37:
// 				if ("{il2cpp array field local7->}" != (ulong)0L)
// 				{
// 				}
// 			}
// 			while (num != 0);
// 			throw new NullReferenceException();
// 		}
//
// 		public WorldInfo GetWorldInfoByPlayerId()
// 		{
// 			long playerId = SingletonMonoBehaviour.Instance.PlayerId;
// 			long num = SingletonMonoBehaviour.Instance.TimeServerId;
// 			num = Ortega.Share.Utils.WorldUtil.GetWorldIdByPlayerId(playerId, num);
// 			return this.GetWorldInfoById(num);
// 		}
//
// 		public bool IsDebugUserByUserIdAndPlayerId(long userId, long playerId)
// 		{
// 			int num;
// 			do
// 			{
// 				bool flag = this.<MaintenanceDebugUserInfos>k__BackingField.IsNullOrEmpty<MaintenanceDebugUserInfo>();
// 				if (flag)
// 				{
// 					break;
// 				}
// 				IReadOnlyList<MaintenanceDebugUserInfo> readOnlyList = this.<MaintenanceDebugUserInfos>k__BackingField;
// 				num = 0;
// 				if (flag)
// 				{
// 					uint num2;
// 					if (num >= (int)num2)
// 					{
// 						goto IL_2D;
// 					}
// 					num += num;
// 					if (num != (int)num2)
// 					{
// 						num++;
// 						goto IL_2D;
// 					}
// 					IL_30:
// 					int num3;
// 					num3 += num3;
// 					goto IL_37;
// 					IL_2D:
// 					num3 = 0;
// 					goto IL_30;
// 				}
// 				IL_37:
// 				if ("{il2cpp array field local8->}" != (ulong)0L)
// 				{
// 				}
// 			}
// 			while (num != 0);
// 			throw new NullReferenceException();
// 		}
//
// 		private void InitResoutionType()
// 		{
// 			ResolutionType staticValue = SingletonMonoBehaviour.Instance.GetStaticValue<ResolutionType>("ResolutionType", (ResolutionType)((uint)3));
// 			this.<CurrentResolutionType>k__BackingField = staticValue;
// 		}
//
// 		public void SetResolutionType(ResolutionType resolutionType)
// 		{
// 			PlayerPrefsManager instance = SingletonMonoBehaviour.Instance;
// 		}
//
// 		public GameManager()
// 		{
// 		}
//
// 		[FieldOffset(Offset = "0xD0")]
// 		private Dictionary<VolumeType, bool> _muteSettings;
//
// 		[FieldOffset(Offset = "0xD8")]
// 		private Dictionary<VolumeType, int> _volumeSettings;
//
// 		[FieldOffset(Offset = "0xF0")]
// 		private Dictionary<AppNotificationType, bool> _appNotificationDict;
// 	}
// }
