using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    class WizardParamList : AWizardObject
    {
        private List<WizardParam> paramList;

        public WizardParamList()
        {
            paramList = new List<WizardParam>();
        }

        public override void ExtractUsableData()
        {
            WizardParam param;
            int dataIndex = 0;
            while (dataIndex < workInnerData.Count)
            {
                param = new WizardParam();
                param.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                paramList.Add(param);
            }
        }

        public override void UpdateInnerDataList()
        {
            innerData = new List<string>();

            foreach (WizardParam param in paramList)
            {
                innerData.AddRange(param.RawData);
            }
        }
    }
}
