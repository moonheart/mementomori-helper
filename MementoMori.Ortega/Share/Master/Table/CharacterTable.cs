using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class CharacterTable : TableBase<CharacterMB>
    {
        public bool GetCharacterName(long id, out string name1, out string name2)
        {
            name1 = "";
            name2 = "";
            var characterMb = this.GetById(id);
            if (characterMb == null)
            {
                return false;
            }

            characterMb.GetCharacterName(out name1, out name2);
            return true;
        }

        public string GetCharacterName(long id)
        {
            GetCharacterName(id, out var name1, out var name2);
            return string.IsNullOrEmpty(name2) ? name1 : $"【{name2}】{name1}";
        }
    }
}