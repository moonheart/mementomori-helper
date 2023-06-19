// using System;
// using System.Collections.Generic;
// using Cpp2IlInjected;
// using Ortega.Common.Manager;
// using Ortega.Common.Utils;
// using Ortega.Share;
// using Ortega.Share.Data.DtoInfo;
// using Ortega.Share.Enums;
// using Ortega.Share.Extensions;
// using Ortega.Share.Master.Data;
// using Ortega.Share.Master.Table;
// using Ortega.Share.Utils;
//
// namespace Ortega.Common.Data
// {
// 	public class CharacterTouchActionData
// 	{
// 		public void SetupForMyPage(long characterId)
// 		{
// 			this._characterId = characterId;
// 			this.CreateTouchActionListForMyPage();
// 		}
//
// 		public void SetupForCharacterDetail(long characterId)
// 		{
// 			this._characterId = characterId;
// 			this.CreateTouchActionListForCharacterDetail();
// 		}
//
// 		public void CheckBirthdayAndRankUp()
// 		{
// 			throw new NotImplementedException();
// 		}
//
// 		public long GetId()
// 		{
// 			throw new NotImplementedException();
// 		}
//
// 		public long GetVoiceId()
// 		{
// 			throw new NotImplementedException();
// 		}
//
// 		public void NextAction()
// 		{
// 			throw new NotImplementedException();
//
// 		}
//
// 		private void CreateTouchActionListForMyPage()
// 		{
// 			throw new NotImplementedException();
//
// 		}
//
// 		private void CreateTouchActionListForCharacterDetail()
// 		{
// 			throw new NotImplementedException();
//
// 		}
//
// 		private bool IsValidComeback()
// 		{
// 			throw new NotImplementedException();
// 		}
//
// 		private bool IsValidBirthday()
// 		{
// 			throw new NotImplementedException();
// 		}
//
// 		public CharacterTouchActionData()
// 		{
// 			throw new NotImplementedException();
// 		}
//
// 		// Note: this type is marked as 'beforefieldinit'.
// 		static CharacterTouchActionData()
// 		{
// 			TimeSpan timeSpan;
// 			CharacterTouchActionData.ComebackTimeSpan = timeSpan;
// 		}
//
// 		private static readonly TimeSpan ComebackTimeSpan;
//
// 		[FieldOffset(Offset = "0x10")]
// 		private long _characterId;
//
// 		[FieldOffset(Offset = "0x18")]
// 		private List<long> _touchActionIds;
//
// 		[FieldOffset(Offset = "0x20")]
// 		private int _touchActionIndex;
//
// 		[FieldOffset(Offset = "0x24")]
// 		private bool _isBirthday;
//
// 		[FieldOffset(Offset = "0x25")]
// 		private bool _isComeback;
//
// 		[FieldOffset(Offset = "0x28")]
// 		private CharacterVoiceUnlockCheckData _characterVoiceUnlockCheckData;
// 	}
// }
