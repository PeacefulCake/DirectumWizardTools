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

            Dictionary<string, WizardParam> namedParams = new Dictionary<string, WizardParam>();
            // Загрузить параметры из папки
            foreach (var filePath in fileList)
            {
                if (paramsOrderFilePath != filePath)
                {
                    string newParamName = Path.GetFileNameWithoutExtension(filePath);
                    WizardParam createdParam = new WizardParam();
                    createdParam.LoadFromFolder(filePath);
                    namedParams.Add(newParamName, createdParam); 
                }
            }

            // 1. Искать по новому имени затем по старому (для переименованных)
            WizardParam param; string dictName;
            foreach (string paramName in paramsOrder)
            {
                dictName = paramName;
                if (!namedParams.TryGetValue(paramName, out param))
                {
                    // Не лучшая идея одновременно переименовать и сделать копию, возьмется полуслучайно, на свой страх и риск кароч
                    var paramd = namedParams.Where(p => p.Value.Name.DecodedValue == paramName).FirstOrDefault();
                    dictName = paramd.Key;
                    param = paramd.Value;
                }
                if (param != null)
                {
                    param.Name.DecodedValue = dictName;
                    paramList.Add(param);
                    namedParams.Remove(dictName);
                }
            }

            // 2. Второй заход, ищет по старому имени для того чтобы вставить параметры в порядке хоть немного похожем на первоначальный
            foreach (string paramName in paramsOrder)
            {
                List<string> remParamNames = new List<string>();
                foreach (var paramd in namedParams.Where(p => p.Value.Name.DecodedValue == paramName))
                {
                    param = paramd.Value;
                    dictName = paramd.Key;
                    param.Name.DecodedValue = dictName;
                    paramList.Add(param);
                    remParamNames.Add(dictName);
                }
                foreach (string dn in remParamNames)
                {
                    namedParams.Remove(dn);
                }

            }

            // 3. Добавить оставшиеся (ну совсем новые) параметры в самый конец
            foreach (var paramd in namedParams)
            {
                param = paramd.Value;
                dictName = paramd.Key;
                param.Name.DecodedValue = dictName;
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
