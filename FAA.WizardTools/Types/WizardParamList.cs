using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardParamList : AWizardObject
    {
        private const string paramsFolderName = "Params";
        private const string wizardParamsOrderFileName = "ParamsOrder.dwi";

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
            objectName = WSConstants.Objects.ParamList;
            objectEnding = WSConstants.Markup.End;

            string paramsFolder = Path.Combine(folderPath, paramsFolderName);
            string paramsOrderFilePath = Path.Combine(paramsFolder, wizardParamsOrderFileName);

            List<string> fileList = Directory.GetFiles(paramsFolder).ToList();

            List<string> paramsOrder = File.ReadAllLines(paramsOrderFilePath).ToList();

            // Добавить в конец списка новые параметры
            foreach (var filePath in fileList)
            {
                string fileName = Path.GetFileName(filePath);
                string fileNameWE = Path.GetFileNameWithoutExtension(filePath);
                if (wizardParamsOrderFileName != fileName && !paramsOrder.Contains(fileNameWE))
                {
                    paramsOrder.Add(fileNameWE);
                }
            }

            // Загрузить все параметры
            WizardParam param;
            foreach (var paramName in paramsOrder)
            {
                string paramFilePath = Path.Combine(paramsFolder, paramName);
                param = new WizardParam();
                param.LoadFromFolder(paramFilePath);
                paramList.Add(param);
            }
        }

        public override void SaveToFolder(string folderPath)
        {
            string paramsFolder = Path.Combine(folderPath, paramsFolderName);
            Directory.CreateDirectory(paramsFolder);

            string paramsOrderFilePath = Path.Combine(paramsFolder, wizardParamsOrderFileName);
            File.WriteAllLines(paramsOrderFilePath, paramList.Select(p => p.Name.DecodedValue).ToList());

            foreach (var param in paramList)
            {
                param.SaveToFolder(paramsFolder);
            }
        }
    }
}
