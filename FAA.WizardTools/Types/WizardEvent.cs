using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardEvent : AWizardObject
    {
        string EventType;
        WizardString ISBLText;

        public WizardEvent()
        {
            ISBLText = new WizardString();
        }

        public override void ExtractUsableData()
        {
            throw new NotImplementedException();
        }

        public override void LoadFromFolder(string folderPath)
        {
            throw new NotImplementedException();
        }

        public override void SaveToFolder(string folderPath)
        {
            throw new NotImplementedException();
        }

        public override void UpdateInnerDataList()
        {
            throw new NotImplementedException();
        }
    }
}