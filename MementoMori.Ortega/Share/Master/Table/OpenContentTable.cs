using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class OpenContentTable : TableBase<OpenContentMB>
    {
        public List<OpenContentMB> GetListByOpenContentType(OpenContentType type)
        {
            return _datas.Where(d => d.OpenContentType == type).ToList();
        }

        public OpenContentMB GetByOpenCommandType(OpenCommandType type)
        {
            return _datas.FirstOrDefault(d => d.OpenCommandType == type);
        }

        public OpenContentMB GetByOpenContentTypeAndCommandType(OpenContentType contentType, OpenCommandType commandType)
        {
            return _datas.FirstOrDefault(d => d.OpenContentType == contentType && d.OpenCommandType == commandType);
        }

        public OpenContentMB GetByTutorialId(long tutorialId)
        {
            return _datas.FirstOrDefault(d => d.TutorialId == tutorialId);
        }
    }
}