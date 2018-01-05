using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardToolsOverpowered.Utils;

namespace WizardToolsOverpowered.Types
{
    class TSBWizardStepFormElementList : IStringable
    {
        List<TSBWizardStepFormElement> FormElements;

        public TSBWizardStepFormElementList()
        {
            FormElements = new List<TSBWizardStepFormElement>();
        }

        public void LoadFromStringList(List<String> data)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            string currentIndent = new string(' ', indentLevel * 2);
            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.StepFormElementListHeader, Const.CR);

            foreach (var item in FormElements)
            {
                sb.AppendLine(item.ToStructuredString(indentLevel + 1));
            }

            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.End, Const.CR);
            return sb.ToString();
        }
    }
}