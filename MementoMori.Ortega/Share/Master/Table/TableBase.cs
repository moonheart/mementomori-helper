using MementoMori.Ortega.Share.Master.Interfaces;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Table
{
	public abstract class TableBase<TM> : ITable where TM : MasterBookBase
	{
		public TM GetById(long id)
		{
			return _datas?.FirstOrDefault(d => d.Id == id);
		}

		public string GetMasterBookName()
		{
			return typeof(TM).Name;
		}

		public TM[] GetArray()
		{
			return _datas;
		}

		public int Count()
		{
			return _datas.Length;
		}

		public bool Load()
		{
			var filePath = GetMasterDataPath(typeof(TM).Name);
			using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				_datas = MessagePackSerializer.Deserialize<TM[]>(fileStream);
			}

			return true;
			// string name;
			// string text;
			// int num4;
			// string[] array2;
			// for (;;)
			// {
			// 	int num = 0;
			// 	name = typeof(Type).Name;
			// 	if (!File.Exists(text))
			// 	{
			// 		goto IL_156;
			// 	}
			// 	FileStream fileStream = new FileStream(text, (FileMode)0, (FileAccess)((uint)1));
			// 	byte[] array = new byte[fileStream.Length];
			// 	int length = array.Length;
			// 	int num2 = 0;
			// 	int num3 = fileStream.Read(array, num2, length);
			// 	ReadOnlyMemory readOnlyMemory;
			// 	this._datas = readOnlyMemory;
			// 	string text2 = "loaded from cache. MB:" + name + " Path:" + text;
			// 	num4 = 0;
			// 	int num5 = 0;
			// 	text2.FieldGetter(num5, num4, text);
			// 	if (num != 0)
			// 	{
			// 		goto IL_193;
			// 	}
			// 	array2 = new string[6];
			// 	if ("an error occurred during deserialization. MB:" != 0 && "an error occurred during deserialization. MB:" == 0)
			// 	{
			// 		goto IL_193;
			// 	}
			// 	array2[0] = "an error occurred during deserialization. MB:";
			// 	if (name == 0 || "an error occurred during deserialization. MB:" != 0)
			// 	{
			// 		array2[1] = name;
			// 		if (" Path:" == 0 || " Path:" != 0)
			// 		{
			// 			array2[2] = " Path:";
			// 			if (text == 0 || " Path:" != 0)
			// 			{
			// 				array2[3] = text;
			// 				if (" " == 0 || " " != 0)
			// 				{
			// 					array2[4] = " ";
			// 					if (" " == 0 || " " != 0)
			// 					{
			// 						break;
			// 					}
			// 				}
			// 			}
			// 		}
			// 	}
			// }
			// array2[5] = " ";
			// string text3 = string.Concat(array2);
			// int num6 = 0;
			// text3.FieldGetter(num6, num4, text);
			// IL_156:
			// string text4 = "TableBase file not exists. MB:" + name + " Path:" + text;
			// int num7 = 0;
			// text4.FieldGetter(num7, " Path:", text);
			// throw new NullReferenceException();
			// IL_193:
			// throw new NullReferenceException();
			// throw new NotImplementedException();
		}

        public virtual bool Load(byte[] binaryData)
        {
            _datas = MessagePackSerializer.Deserialize<TM[]>(binaryData);;
            return true;
        }

		protected static string GetMasterDataPath(string masterBookName)
		{
			return "./Master/" + masterBookName;
			// throw new NotImplementedException();
		}

		protected TableBase()
		{
		}

		// [FieldOffset(Offset = "0x0")]
		protected TM[] _datas;
	}
}
