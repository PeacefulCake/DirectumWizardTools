using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardEventArray
    {
        private List<WizardEvent> events;

        private const int innerIndents = 2;

        public WizardEventArray()
        {
            this.events = new List<WizardEvent>();
        }

        public List<string> RawData
        {
            get
            {
                List<string> result = new List<string>();
                if (events.Count == 0)
                {
                    result.Add(WSConstants.Markup.EmptyArray);
                }
                else
                {
                    result.Add(WSConstants.Markup.ArrayStart);
                    foreach (var wEvent in events)
                    {
                        result.AddRange(wEvent.RawData.Select(s => s.AddIndents(innerIndents)));
                    }
                    result[result.Count - 1] = string.Format("{0}{1}", result[result.Count - 1], WSConstants.Markup.ArrayEnd);
                }

                return result;
            }
        }

        public void LoadFromDataList(List<String> data)
        {
            WizardEvent wEvent;
            int dataIndex = 0;
            while (dataIndex < data.Count)
            {
                wEvent = new WizardEvent();
                wEvent.LoadFromDataList(StringUtils.PickObject(data, dataIndex));
                events.Add(wEvent);
            }
        }

        public void PowerLoad(string value, List<string> data, int index)
        {
            if (value == WSConstants.Markup.ArrayStart)
            {
                this.LoadFromDataList(StringUtils.PickArray(data, index + 1).Select(s => s.Substring(innerIndents)).ToList());
            }
        }

        public void LoadFromFolder(string folderPath)
        {
            throw new NotImplementedException();
            // TODO : тут есть контроль порядка, если что-то нашлось в количестве больше одного
            List<string> fileList = Directory.GetFiles(folderPath, "*.isbl", SearchOption.TopDirectoryOnly).ToList();
        }

        public void SaveToFolder(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
