using FAA.Utils;
using System;
using System.IO;
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

        private const string wizardCardFileName = "WizardCard.dwc";

        public WizardParamList Params;
        public WizardStepList Steps;

        public WizardString Code;

        public Wizard()
        {
            Params = new WizardParamList();
            Steps = new WizardStepList();
            Code = new WizardString();
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
                        case WSConstants.Fields.Code:
                            this.Code.PowerLoad(value, workInnerData, dataIndex);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (line)
                    {
                        case WSConstants.Objects.ParamList:
                            Params.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                            workInnerData.Insert(dataIndex, paramsPositionMark);
                            break;
                        case WSConstants.Objects.StepList:
                            Steps.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                            workInnerData.Insert(dataIndex, stepsPositionMark);
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
            innerData = new List<string>(workInnerData);

            int stepsIndex = innerData.IndexOf(stepsPositionMark);
            innerData.RemoveAt(stepsIndex);
            innerData.InsertRange(stepsIndex, Steps.RawData);

            int paramsIndex = innerData.IndexOf(paramsPositionMark);
            innerData.RemoveAt(paramsIndex);
            innerData.InsertRange(paramsIndex, Params.RawData);
        }

        public override void LoadFromFolder(string folderPath)
        {
            throw new NotImplementedException();
            var di = new DirectoryInfo(folderPath);
            Code.DecodedValue = di.Name;

            string cardFilePath = Path.Combine(folderPath, wizardCardFileName);
            workInnerData = File.ReadAllLines(cardFilePath).ToList();

            Params.LoadFromFolder(folderPath);
            Steps.LoadFromFolder(folderPath);
        }

        public override void SaveToFolder(string folderPath)
        {
            string wizardFolder = Path.Combine(folderPath, Code.DecodedValue);
            if(Directory.Exists(wizardFolder)) Directory.Delete(wizardFolder, true);
            Directory.CreateDirectory(wizardFolder);

            // TODO: xml с реквизитами, когда-нибудь... а надо ли?
            string cardFilePath = Path.Combine(wizardFolder, wizardCardFileName);
            File.WriteAllLines(cardFilePath, workInnerData);

            Params.SaveToFolder(wizardFolder);
            Steps.SaveToFolder(wizardFolder);
        }
    }
}
