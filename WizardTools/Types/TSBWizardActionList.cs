using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Utils;

namespace WizardTools.Types
{
    class TSBWizardActionList : IStringable
    {
        List<TSBWizardAction> Actions;

        public TSBWizardActionList()
        {
            Actions = new List<TSBWizardAction>();
        }

        public void LoadFromStringList(List<String> data)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            string currentIndent = new string(' ', indentLevel * 2);
            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.ActionListHeader, Const.CR);

            foreach (var item in Actions)
            {
                sb.AppendLine(item.ToStructuredString(indentLevel + 1));
            }

            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.End, Const.CR);
            return sb.ToString();
        }
    }
}