using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Extensions;
using WizardTools.Utils;

namespace WizardTools.Types
{
    class TSBWizard
    {
        public WizardString Code;
        public int ID;
        public WizardString Title;

        public TSBWizardEvent[] Events;

        public TSBWizardStepList StepList;

        public TSBWizardParamList ParamList;

        public string ToStructuredString()
        {
            int currentIndenLevel = 1;
            string currentIndent = new string(' ', currentIndenLevel * 2);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Const.WizardHeader);
            sb.AppendFormat("{0}Code = {1}{2}", currentIndent, Code.ToStructuredString(currentIndenLevel), Const.CR);
            sb.AppendFormat("{0}ID = {1}{2}", currentIndent, ID.ToString(), Const.CR);
            sb.AppendFormat("{0}Title = {1}{2}", currentIndent, Title.ToStructuredString(currentIndenLevel), Const.CR);

            sb.AppendFormat("{0}Events = {1}{2}", currentIndent, Events.ToStructuredString(currentIndenLevel), Const.CR);

            sb.Append(Const.End);
            return sb.ToString();
        }
    }
}
