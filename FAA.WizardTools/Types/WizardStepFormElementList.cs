﻿using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardStepFormElementList : AWizardObject
    {
        private const string stepFormFileName = "StepForm.dwc";

        private List<WizardStepFormElement> formElementsList;

        public WizardStepFormElementList()
        {
            formElementsList = new List<WizardStepFormElement>();
        }

        public override void ExtractUsableData()
        {
            WizardStepFormElement formElement;
            int dataIndex = 0;
            while (dataIndex < workInnerData.Count)
            {
                formElement = new WizardStepFormElement();
                formElement.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                formElementsList.Add(formElement);
            }
        }

        public override void UpdateInnerDataList()
        {
            innerData = new List<string>();

            foreach (WizardStepFormElement formElement in formElementsList)
            {
                innerData.AddRange(formElement.RawData);
            }
        }

        public List<WizardStepFormElement> GetAskableParams()
        {
            return formElementsList.Where(fe => fe.IsParamAssigned).ToList();
        }

        public override void LoadFromFolder(string folderPath)
        {
            string cardFilePath = Path.Combine(folderPath, stepFormFileName);
            this.LoadFromDataList(File.ReadAllLines(cardFilePath).ToList());
        }

        public override void SaveToFolder(string folderPath)
        {
            string cardFilePath = Path.Combine(folderPath, stepFormFileName);
            File.WriteAllLines(cardFilePath, RawData);
        }
    }
}
