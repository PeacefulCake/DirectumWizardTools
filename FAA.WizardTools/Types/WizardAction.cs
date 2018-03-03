using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardAction : AWizardObject
    {
        private const string eventsPositionMark = "{StepEvents}";

        public string ActionType; // 0 - Previous, 1 - Next, 2 - Cancel, 3 - Finish
        public WizardEventArray Events;

        public WizardAction()
        {
            Events = new WizardEventArray();
        }

        public override void ExtractUsableData()
        {
            int dataIndex = 0;
            while (dataIndex < workInnerData.Count)
            {
                string line = workInnerData[dataIndex];
                string name, value;
                if (StringUtils.GetFieldPair(line, out name, out value))
                {
                    switch (name)
                    {
                        case WSConstants.Fields.ActionType:
                            this.ActionType = value;
                            break;
                        case WSConstants.Fields.Events:
                            this.Events.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = eventsPositionMark;
                            break;
                        default:
                            break;
                    }
                }
                dataIndex++;
            }
        }

        public override void UpdateInnerDataList()
        {
            {
                innerData = new List<string>(workInnerData);

                int eventsIndex = innerData.IndexOf(eventsPositionMark);
                innerData.RemoveAt(eventsIndex);
                innerData.InsertRange(eventsIndex, Events.RawData);
                innerData[eventsIndex] = string.Format("{0} = {1}", WSConstants.Fields.Events, innerData[eventsIndex]);
            }
        }

        public override void LoadFromFolder(string folderPath)
        {
            throw new NotImplementedException();
        }

        public override void SaveToFolder(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
