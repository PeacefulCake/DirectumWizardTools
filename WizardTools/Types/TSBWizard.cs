using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardTools.Types
{
    class TSBWizard
    {
        public WizardString Code;
        public int ID;
        public WizardString Title;

        TSBWizardEvent[] Events;

        TSBWizardStepList StepList;

        TSBWizardParamList ParamList;

        public string ToStructuredString()
        {
            string innerIndent = new string(' ', 2);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("object TSBWizard");
            sb.AppendFormat("{0}Code = {1}{2}", innerIndent, Code.ToStructuredString(1), Const.CR);
            sb.AppendFormat("{0}ID = {1}{2}", innerIndent, ID.ToString(), Const.CR);
            sb.AppendFormat("{0}Title = {1}{2}", innerIndent, Title.ToStructuredString(1), Const.CR);

            sb.Append("end");
            return sb.ToString();
        }
    }
}
