using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Utils;

namespace WizardTools.Types
{
    class TSBWizardEvent : IStringable
    {
        public WizardString ISBLText;
        public TWizardEventType EventType;

        public TSBWizardEvent()
        {
            ISBLText = new WizardString();
        }

        public void LoadFromStringList(IList<string> data)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            string headersIndent = new string(' ', indentLevel * 2);
            string innerIndent = new string(' ', (indentLevel + 1) * 2);

            sb.AppendFormat("{0}{1}{2}", headersIndent, Const.Item, Const.CR);

            sb.AppendFormat("{0}ISBLText = {1}{2}", innerIndent, ISBLText.ToStructuredString(indentLevel + 1), Const.CR);
            sb.AppendFormat("{0}EventType = {1}{2}", innerIndent, EventType.ToString(), Const.CR);

            sb.AppendFormat("{0}{1}", headersIndent, Const.End);
            return sb.ToString();
        }

        public static TWizardEventType GetEventTypeByName(string eventName)
        {
            switch (eventName)
            {
                case Const.EventTypes.WizardBeforeSelection:
                    return TWizardEventType.wetWizardBeforeSelection;
                case Const.EventTypes.WizardStart:
                    return TWizardEventType.wetWizardStart;
                case Const.EventTypes.WizardFinish:
                    return TWizardEventType.wetWizardFinish;
                case Const.EventTypes.StepStart:
                    return TWizardEventType.wetStepStart;
                case Const.EventTypes.StepFinish:
                    return TWizardEventType.wetStepFinish;
                case Const.EventTypes.ActionExecute:
                    return TWizardEventType.wetActionExecute;
                default: return TWizardEventType.wetActionExecute;
            }

        }
    }
}