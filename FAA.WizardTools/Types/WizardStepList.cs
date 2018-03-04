using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardStepList : AWizardObject
    {
        private const string stepsFolderName = "Steps";
        private const string wizardStepsOrderFileName = "StepsOrder.dwi";

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

        public override void LoadFromFolder(string folderPath)
        {
            objectName = WSConstants.Objects.StepList;
            objectEnding = WSConstants.Markup.End;

            string stepsFolder = Path.Combine(folderPath, stepsFolderName);
            string stepsOrderFilePath = Path.Combine(stepsFolder, wizardStepsOrderFileName);

            List<string> folderList = Directory.GetDirectories(stepsFolder).ToList();

            List<string> stepsOrder = File.ReadAllLines(stepsOrderFilePath).ToList();

            Dictionary<string, WizardStep> namedSteps = new Dictionary<string, WizardStep>();
            // Загрузить шаги из папки
            foreach (var fPath in folderList)
            {
                var di = new DirectoryInfo(fPath);
                string newStepName = di.Name;
                WizardStep createdStep = new WizardStep();
                createdStep.LoadFromFolder(fPath);
                namedSteps.Add(newStepName, createdStep);
            }

            // 1. Искать по новому имени затем по старому (для переименованных)
            WizardStep step; string dictName;
            foreach (string stepName in stepsOrder)
            {
                dictName = stepName;
                if (!namedSteps.TryGetValue(stepName, out step))
                {
                    // Не лучшая идея одновременно переименовать и сделать копию, возьмется полуслучайно, на свой страх и риск кароч
                    var stepd = namedSteps.Where(p => p.Value.Name.DecodedValue == stepName).FirstOrDefault();
                    dictName = stepd.Key;
                    step = stepd.Value;
                }
                if (step != null)
                {
                    step.Name.DecodedValue = dictName;
                    stepList.Add(step);
                    namedSteps.Remove(dictName);
                }
            }

            // 2. Второй заход, ищет по старому имени для того чтобы вставить параметры в порядке хоть немного похожем на первоначальный
            foreach (string stepName in stepsOrder)
            {
                List<string> remStepNames = new List<string>();
                foreach (var stepd in namedSteps.Where(p => p.Value.Name.DecodedValue == stepName))
                {
                    step = stepd.Value;
                    dictName = stepd.Key;
                    step.Name.DecodedValue = dictName;
                    stepList.Add(step);
                    remStepNames.Add(dictName);
                }
                foreach (string dn in remStepNames)
                {
                    namedSteps.Remove(dn);
                }

            }

            // 3. Добавить оставшиеся (ну совсем новые) параметры в самый конец
            foreach (var stepd in namedSteps)
            {
                step = stepd.Value;
                dictName = stepd.Key;
                step.Name.DecodedValue = dictName;
                stepList.Add(step);
            }
        }

        public override void SaveToFolder(string folderPath)
        {
            string stepsFolder = Path.Combine(folderPath, stepsFolderName);
            Directory.CreateDirectory(stepsFolder);

            string stepsOrderFilePath = Path.Combine(stepsFolder, wizardStepsOrderFileName);
            File.WriteAllLines(stepsOrderFilePath, stepList.Select(p => p.Name.DecodedValue).ToList());

            foreach (var step in stepList)
            {
                step.SaveToFolder(stepsFolder);
            }
        }
    }
}
