using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public abstract class AWizardObject
    {
        public string ObjectName {
            get
            {
                return objectName;
            }
        }
        protected string objectName = "";

        public string ObjectEnding
        {
            get
            {
                return objectEnding;
            }
        }
        protected string objectEnding = "";

        protected bool loaded = false;
        public bool Loaded
        {
            get { return loaded; }
        }

        public List<string> RawData {
            get
            {
                UpdateRawData();
                return rawData;
            }
        }
        protected List<string> rawData;
        protected List<string> innerData;
        protected List<string> workInnerData;
        protected const int innerIndents = 2;

        abstract public void UpdateInnerDataList();
        abstract public void ExtractUsableData();
        //abstract public bool CheckData(); // Метод для рекурсивной проверки
        // TODO: проверить насколько вообще нужно считать количество отступов

        public void LoadFromDataList(List<string> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            rawData = data;
            if (data.Count >= 3)
            {
                int trueInnerIndents = rawData[1].IndentsCount();
                if (trueInnerIndents != innerIndents)
                {
                    throw new Exception("С отступами что-то не так");
                }

                innerData = rawData.GetRange(1, rawData.Count - 2).Select(s => s.Substring(innerIndents)).ToList();
                workInnerData = new List<string>(innerData);
            }
            else
            {
                innerData = new List<string>();
                workInnerData = new List<string>();
            }

            objectName = rawData.First().TrimStart();
            objectEnding = rawData.Last().TrimStart();

            ExtractUsableData();

            loaded = true;
        }

        public void UpdateRawData()
        {
            UpdateInnerDataList();

            List<string> newRawData = new List<string>();
            newRawData.Add(objectName);
            newRawData.AddRange(innerData.Select(s => s.AddIndents(innerIndents)));
            newRawData.Add(objectEnding);

            rawData = newRawData;
        }

        public string ExportToString()
        {
            return string.Join(WSConstants.CR, RawData);
        }

        abstract public void LoadFromFolder(string folderPath);
        abstract public void SaveToFolder(string folderPath);
    }
}
