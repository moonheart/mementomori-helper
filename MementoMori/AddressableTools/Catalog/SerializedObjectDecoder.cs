using System;
using System.IO;
using System.Text;

namespace MementoMori.AddressableTools.Catalog
{
    internal static class SerializedObjectDecoder
    {
        internal enum ObjectType
        {
            AsciiString,
            UnicodeString,
            UInt16,
            UInt32,
            Int32,
            Hash128,
            Type,
            JsonObject
        }

        internal static object Decode(BinaryReader br)
        {
            var type = (ObjectType) br.ReadByte();

            switch (type)
            {
                case ObjectType.AsciiString:
                    {
                        var str = ReadString4(br);
                        return str;
                    }

                case ObjectType.UnicodeString:
                    {
                        var str = ReadString4Unicode(br);
                        return str;
                    }

                case ObjectType.UInt16:
                    {
                        return br.ReadUInt16();
                    }

                case ObjectType.UInt32:
                    {
                        return br.ReadUInt32();
                    }

                case ObjectType.Int32:
                    {
                        return br.ReadInt32();
                    }

                case ObjectType.Hash128:
                    {
                        // read as string for now
                        var str = ReadString1(br);
                        return str;
                    }

                case ObjectType.Type:
                    {
                        var str = ReadString1(br);
                        return Type.GetTypeFromCLSID(new Guid(str));
                    }

                case ObjectType.JsonObject:
                    {
                        var assemblyName = ReadString1(br);
                        var className = ReadString1(br);
                        var jsonText = ReadString4Unicode(br);
                        var jsonObj = new ClassJsonObject(assemblyName, className, jsonText);
                        return jsonObj;
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        private static string ReadString1(BinaryReader br)
        {
            int length = br.ReadByte();
            var str = Encoding.ASCII.GetString(br.ReadBytes(length));
            return str;
        }

        private static string ReadString4(BinaryReader br)
        {
            var length = br.ReadInt32();
            var str = Encoding.ASCII.GetString(br.ReadBytes(length));
            return str;
        }

        private static string ReadString4Unicode(BinaryReader br)
        {
            var length = br.ReadInt32();
            var str = Encoding.Unicode.GetString(br.ReadBytes(length));
            return str;
        }
    }
}