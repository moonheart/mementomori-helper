using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Master
{
    [MessagePackObject(true)]
    public class MasterBookInfo
    {
        public string Hash { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public MasterBookInfo()
        {
        }
    }
}