using FAA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Types
{
    public class WizardEventArray
    {
        private List<WizardEvent> events;
        private List<string> eventsOrder;

        private const int innerIndents = 2;

        public WizardEventArray()
        {
            this.events = new List<WizardEvent>();
            eventsOrder = new List<string>();
            eventsOrder.Add("wetWizardBeforeSelection");
            eventsOrder.Add("wetWizardStart");
            eventsOrder.Add("wetWizardFinish");

            eventsOrder.Add("wetStepStart");
            eventsOrder.Add("wetStepFinish");

            eventsOrder.Add("wetActionExecute");
        }

        public List<string> RawData
        {
            get
            {
                List<string> result = new List<string>();
                if (events.Count == 0)
                {
                    result.Add(WSConstants.Markup.EmptyArray);
                }
                else
                {
                    result.Add(WSConstants.Markup.ArrayStart);
                    foreach (var wEvent in events)
                    {
                        result.AddRange(wEvent.RawData.Select(s => s.AddIndents(innerIndents)));
                    }
                    result[result.Count - 1] = string.Format("{0}{1}", result[result.Count - 1], WSConstants.Markup.ArrayEnd);
                }

                return result;
            }
        }

        public void LoadFromDataList(List<String> data)
        {
            WizardEvent wEvent;
            int dataIndex = 0;
            while (dataIndex < data.Count)
            {
                wEvent = new WizardEvent();
                wEvent.LoadFromDataList(StringUtils.PickObject(data, dataIndex));
                events.Add(wEvent);
            }
        }

        public void PowerLoad(string value, List<string> data, int index)
        {
            if (value == WSConstants.Markup.ArrayStart)
            {
                this.LoadFromDataList(StringUtils.PickArray(data, index + 1).Select(s => s.Substring(innerIndents)).ToList());
            }
        }

        public void LoadFromFolder(string folderPath)
        {
            List<string> fileList = Directory.GetFiles(folderPath, "*.isbl", SearchOption.TopDirectoryOnly).ToList();
            Dictionary<string, WizardEvent> namedEvents = new Dictionary<string, WizardEvent>();

            foreach (var file in fileList)
            {
                string eventType = Path.GetFileNameWithoutExtension(file);
                WizardEvent wEvent = new WizardEvent();
                wEvent.EventType = eventType;
                wEvent.LoadFromFolder(folderPath);
                namedEvents.Add(eventType, wEvent);
            }

            WizardEvent wizardEvent;
            foreach (string eventType in eventsOrder)
            {
                if (namedEvents.TryGetValue(eventType, out wizardEvent))
                {
                    events.Add(wizardEvent);
                }
            }
        }

        public void SaveToFolder(string folderPath)
        {
            foreach (var wEvent in events)
            {
                wEvent.SaveToFolder(folderPath);
            }
        }
    }
}
