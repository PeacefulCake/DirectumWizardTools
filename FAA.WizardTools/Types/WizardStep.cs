using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardStep : AWizardObject
    {
        public WizardString Name;
        public WizardString Title;

        public WizardStepFormElementList FormElements;

        private const string stepNameMark = "{StepName}";
        private const string stepTitleMark = "{StepTitle}";
        private const string formElementsMark = "{FormElements}";

        public WizardStep()
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
                        case WSConstants.Fields.StepName:
                            this.Name.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = stepNameMark;
                            break;
                        case WSConstants.Fields.Title:
                            this.Title.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = stepTitleMark;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (line)
                    {
                        case WSConstants.Objects.StepFormElementList:
                            FormElements = new WizardStepFormElementList();
                            FormElements.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                            workInnerData.Insert(dataIndex, formElementsMark);
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

                int nameIndex = innerData.IndexOf(stepNameMark);
                innerData.RemoveAt(nameIndex);
                innerData.InsertRange(nameIndex, Name.RawData);
                innerData[nameIndex] = string.Format("StepName = {0}", innerData[nameIndex]);

                int titleIndex = innerData.IndexOf(stepTitleMark);
                innerData.RemoveAt(titleIndex);
                innerData.InsertRange(titleIndex, Title.RawData);
                innerData[titleIndex] = string.Format("Title = {0}", innerData[titleIndex]);

                int formElementsIndex = innerData.IndexOf(formElementsMark);
                innerData.RemoveAt(formElementsIndex);
                innerData.InsertRange(formElementsIndex, FormElements.RawData);
            }
        }

        public WizardStep CreateCopy(string newName)
        {
            WizardStep res = new WizardStep();
            res.LoadFromDataList(RawData);
            res.Name.DecodedValue = newName;
            return res;
        }
    }
}
