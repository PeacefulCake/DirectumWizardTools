﻿using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    class WizardParam : AWizardObject
    {
        public WizardString Name;
        public WizardString Title;

        private const string paramNameMark = "{ParamName}";
        private const string paramTitleMark = "{ParamTitle}";

        public WizardParam()
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
                        case WSConstants.Fields.ParamName:
                            this.Name.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = paramNameMark;
                            break;
                        case WSConstants.Fields.Title:
                            this.Title.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = paramTitleMark;
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

                int nameIndex = innerData.IndexOf(paramNameMark);
                innerData.RemoveAt(nameIndex);
                innerData.InsertRange(nameIndex, Name.RawData);
                innerData[nameIndex] = string.Format("{0} = {1}", WSConstants.Fields.ParamName, innerData[nameIndex]);

                int titleIndex = innerData.IndexOf(paramTitleMark);
                innerData.RemoveAt(titleIndex);
                innerData.InsertRange(titleIndex, Title.RawData);
                innerData[titleIndex] = string.Format("{0} = {1}", WSConstants.Fields.Title, innerData[titleIndex]);
            }
        }

        public WizardParam CreateCopy(string newName)
        {
            WizardParam res = new WizardParam();
            res.LoadFromDataList(RawData);
            res.Name.DecodedValue = newName;
            return res;
        }

        public override void LoadFromFolder(string folderPath) // В этом случае - это полный путь
        {
            //string paramName = Path.GetFileNameWithoutExtension(folderPath);
            this.LoadFromDataList(File.ReadAllLines(folderPath).ToList());
            // TODO : Немного логичнее, но чуть менее удобно будет поступить как с шагами - параметр = папка, внутри ParamCard. Тогда специфичные параметры можно будет гибче сохранять...
        }

        public override void SaveToFolder(string folderPath)
        {
            string paramFilePath = Path.Combine(folderPath, Name.DecodedValue + ".dwc");
            File.WriteAllLines(paramFilePath, RawData);
        }
    }
}
