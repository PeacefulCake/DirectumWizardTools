using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardEvent : AWizardObject
    {
        private const string eventISBLCodeMark = "{EventISBLCode}";
        private const string eventTypeMark = "{EventType}";

        string EventType;
        WizardString ISBLText;

        public WizardEvent()
        {
            ISBLText = new WizardString();
            objectName = WSConstants.Markup.Item;
            objectEnding = WSConstants.Markup.End;
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
                        case WSConstants.Fields.ISBLText:
                            this.ISBLText.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData.RemoveAt(dataIndex);
                            dataIndex--;
                            break;
                        case WSConstants.Fields.EventType:
                            this.EventType = value;
                            workInnerData.RemoveAt(dataIndex);
                            dataIndex--;
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
            innerData = new List<string>();

            if (ISBLText.EncodedValue != "")
            {
                innerData.AddRange(ISBLText.RawData);
                innerData[0] = string.Format("{0} = {1}", WSConstants.Fields.ISBLText, innerData[0]);
            }

            innerData.Add(string.Format("{0} = {1}", WSConstants.Fields.EventType, EventType));
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