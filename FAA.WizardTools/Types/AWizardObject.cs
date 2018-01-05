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
        protected string objectName;

        public bool PasedSuccessfully;

        protected List<string> rawData;
        protected List<string> innerData;
        protected int innerIndents;

        abstract public void UpdateInnerDataList();
        abstract public void ExtractUsableData();

        public void LoadFromDataList(List<string> data)
        {
            if (data == null || data.Count < 3)
            {
                throw new ArgumentNullException("data");
            }

            rawData = data;
            innerIndents = rawData[1].IndentsCount();

            innerData = rawData.GetRange(1, rawData.Count - 2).Select(s => s.Substring(innerIndents)).ToList();

            objectName = rawData[0].TrimStart();

            ExtractUsableData();
        }

        public void UpdateRawData()
        {
            UpdateInnerDataList();

            List<string> newRawData = new List<string>();
            newRawData.Add(rawData.First());
            newRawData.AddRange(innerData.Select(s => s.AddIndents(innerIndents)));

            newRawData.Add(rawData.Last());

            rawData = newRawData;
        }

        public string ExportToString()
        {
            UpdateRawData();

            return string.Join(WSConstants.CR, rawData);
        }
    }
}
