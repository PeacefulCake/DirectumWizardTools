using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardStepList : AWizardObject
    {
        private List<WizardStep> stepList;

        public WizardStepList()
        {
            stepList = new List<WizardStep>();
        }

        public override void ExtractUsableData()
        {
            WizardStep step;
            int dataIndex = 0;
            while (dataIndex < workInnerData.Count)
            {
                step = new WizardStep();
                step.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                stepList.Add(step);
            }
        }

        public override void UpdateInnerDataList()
        {
            innerData = new List<string>();

            foreach (WizardStep step in stepList)
            {
                innerData.AddRange(step.RawData);
            }
        }

        public bool CopyStep(string step, string newStepName)
        {
            WizardStep originalStep = stepList.Where(p => p.Name.DecodedValue == step).FirstOrDefault();
            if (originalStep == null) return false;
            WizardStep newStep = stepList.Where(p => p.Name.DecodedValue == newStepName).FirstOrDefault();
            if (newStep != null) return false;
            newStep = originalStep.CreateCopy(newStepName);
            stepList.Add(newStep);
            return true;
        }

        public WizardStep GetStep(string stepName)
        {
            return stepList.Where(s => s.Name.DecodedValue == stepName).FirstOrDefault();
        }
    }
}
