using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    class WizardParam : AWizardObject
    {
        public WizardString Name;
        public WizardString Title;

        private const string paramNameMark = "{ParamName}";
        private const string paramTitleMark = "{ParamTitle}";

        public WizardParam()
        {
            Name = new WizardString();
            Title = new WizardString();
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
                        case WSConstants.Fields.ParamName:
                            this.Name.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = paramNameMark;
                            break;
                        case WSConstants.Fields.Title:
                            this.Title.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = paramTitleMark;
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

                int nameIndex = innerData.IndexOf(paramNameMark);
                innerData.RemoveAt(nameIndex);
                innerData.InsertRange(nameIndex, Name.RawData);
                innerData[nameIndex] = string.Format("ParamName = {0}", innerData[nameIndex]);

                int titleIndex = innerData.IndexOf(paramTitleMark);
                innerData.RemoveAt(titleIndex);
                innerData.InsertRange(titleIndex, Title.RawData);
                innerData[titleIndex] = string.Format("Title = {0}", innerData[titleIndex]);
            }
        }

        public WizardParam CreateCopy(string newName)
        {
            WizardParam res = new WizardParam();
            res.LoadFromDataList(RawData);
            res.Name.DecodedValue = newName;
            return res;
        }
    }
}
