using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class Wizard : AWizardObject
    {
        private const string paramsPositionMark = "{WizardParams}";
        private const string stepsPositionMark = "{WizardSteps}";

        private WizardParamList Params;
        private WizardStepList Steps;

        public override void ExtractUsableData()
        {
            int dataIndex = 0;
            while (dataIndex < workInnerData.Count - 1)
            {
                string line = workInnerData[dataIndex];
                switch (line)
                {
                    case WSConstants.Objects.ParamList:
                        Params = new WizardParamList();
                        Params.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                        workInnerData.Insert(dataIndex, paramsPositionMark);
                        break;
                    case WSConstants.Objects.StepList:
                        Steps = new WizardStepList();
                        Steps.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                        workInnerData.Insert(dataIndex, stepsPositionMark);
                        break;
                    default:
                        dataIndex++;
                        break;
                }
            }
        }

        public override void UpdateInnerDataList()
        {
            innerData = new List<string>(workInnerData);

            int stepsIndex = innerData.IndexOf(stepsPositionMark);
            innerData.RemoveAt(stepsIndex);
            innerData.InsertRange(stepsIndex, Steps.RawData);

            int paramsIndex = innerData.IndexOf(paramsPositionMark);
            innerData.RemoveAt(paramsIndex);
            innerData.InsertRange(paramsIndex, Params.RawData);
        }
    }
}
