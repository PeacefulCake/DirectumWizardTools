using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardStepFormElement : AWizardObject
    {
        public WizardString ParamName;

        public WizardStepFormElementList FormElements;

        private const string paramNameMark = "{ParamName}";
        public bool IsParamAssigned { get; set; }

        public WizardStepFormElement()
        {
            ParamName = new WizardString();
            IsParamAssigned = false;
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
                            this.ParamName.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = paramNameMark;
                            IsParamAssigned = true;
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
                if (IsParamAssigned)
                {
                    innerData = new List<string>(workInnerData);
                    int nameIndex = innerData.IndexOf(paramNameMark);
                    innerData.RemoveAt(nameIndex);
                    innerData.InsertRange(nameIndex, ParamName.RawData);
                    innerData[nameIndex] = string.Format("ParamName = {0}", innerData[nameIndex]);
                }

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
