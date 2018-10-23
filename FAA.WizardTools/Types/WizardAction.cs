using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardAction : AWizardObject
    {
        private const string eventsPositionMark = "{ActionEvents}";

        private const string actionCardFileName = "ActionCard.dwc";

        public string ActionType
        {
            get
            {
                return actionType;
            }
            set
            {
                actionType = value;
                switch (actionType)
                {
                    case "0":
                    case "ikhInformation":
                        ActionTypeName = "Previous";
                        break;
                    case "1":
                    case "ikhWarning":
                        ActionTypeName = "Next";
                        break;
                    case "2":
                    case "ikhError":
                        ActionTypeName = "Cancel";
                        break;
                    case "3":
                    case "ikhNoIcon":
                        ActionTypeName = "Finish";
                        break;
                    default:
                        //ActionTypeName = actionType; // !! Иногда прилетает глючный текст МД
                        break;
                }
            }
        }
        private string actionType;
        public string ActionTypeName;

        public WizardEventArray Events;

        public WizardAction()
        {
            Events = new WizardEventArray();
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
                        case WSConstants.Fields.ActionType:
                            this.ActionType = value;
                            break;
                        case WSConstants.Fields.Events:
                            this.Events.PowerLoad(value, workInnerData, dataIndex);
                            workInnerData[dataIndex] = eventsPositionMark;
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

                int eventsIndex = innerData.IndexOf(eventsPositionMark);
                innerData.RemoveAt(eventsIndex);
                innerData.InsertRange(eventsIndex, Events.RawData);
                innerData[eventsIndex] = string.Format("{0} = {1}", WSConstants.Fields.Events, innerData[eventsIndex]);
            }
        }

        public override void LoadFromFolder(string folderPath)
        {
            this.objectName = WSConstants.Objects.Action;
            this.objectEnding = WSConstants.Markup.End;

            string actionFolder = Path.Combine(folderPath, ActionTypeName);
            if (Directory.Exists(actionFolder))
            {
                string actionCardFilePath = Path.Combine(actionFolder, actionCardFileName);
                workInnerData = File.ReadAllLines(actionCardFilePath).ToList();

                Events.LoadFromFolder(actionFolder);
            }
            else
            {
                Events = null;
            }
        }

        public override void SaveToFolder(string folderPath)
        {
            string actionFolder = Path.Combine(folderPath, ActionTypeName);
            Directory.CreateDirectory(actionFolder);

            string actionCardFilePath = Path.Combine(actionFolder, actionCardFileName);
            File.WriteAllLines(actionCardFilePath, workInnerData);

            Events.SaveToFolder(actionFolder);
        }
    }
}
