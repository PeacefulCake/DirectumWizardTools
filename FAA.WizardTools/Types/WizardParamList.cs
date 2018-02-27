using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardParamList : AWizardObject
    {
        private const string paramsFolderName = "Params";
        private const string wizardParamsOrderFileName = "ParamsOrder.xml";

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

        public bool CopyParam(string param, string newParamName)
        {
            WizardParam originalParam = paramList.Where(p => p.Name.DecodedValue == param).FirstOrDefault();
            if (originalParam == null) return false;
            WizardParam newParam = paramList.Where(p => p.Name.DecodedValue == newParamName).FirstOrDefault();
            if (newParam != null) return false;
            newParam = originalParam.CreateCopy(newParamName);
            paramList.Add(newParam);
            return true;
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
