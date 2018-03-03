using FAA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardActionList : AWizardObject
    {
        private List<WizardAction> actions;

        public WizardActionList()
        {
            actions = new List<WizardAction>();
        }

        public override void ExtractUsableData()
        {
            WizardAction action;
            int dataIndex = 0;
            while (dataIndex < workInnerData.Count)
            {
                action = new WizardAction();
                action.LoadFromDataList(StringUtils.PickObject(workInnerData, dataIndex));
                actions.Add(action);
            }
        }

        public override void UpdateInnerDataList()
        {
            innerData = new List<string>();

            foreach (WizardAction action in actions)
            {
                innerData.AddRange(action.RawData);
            }
        }

        public override void LoadFromFolder(string folderPath)
        {
            throw new NotImplementedException();
        }

        public override void SaveToFolder(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
