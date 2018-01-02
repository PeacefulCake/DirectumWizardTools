using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Utils;

namespace WizardTools.Types
{
    class TSBWizardStepList : IStringable
    {
        List<TSBWizardStep> Steps;

        public TSBWizardStepList()
        {
            Steps = new List<TSBWizardStep>();
        }

        public void LoadFromStringList(List<String> data)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            string currentIndent = new string(' ', indentLevel * 2);
            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.StepListHeader, Const.CR);

            foreach (var item in Steps)
            {
                sb.AppendLine(item.ToStructuredString(indentLevel + 1));
            }

            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.End, Const.CR);
            return sb.ToString();
        }
    }
}