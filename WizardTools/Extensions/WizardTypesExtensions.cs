using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Types;
using WizardTools.Utils;

namespace WizardTools.Extensions
{
    static class WizardTypesExtensions
    {
        public static string ToStructuredString(this TSBWizardEvent[] events, int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Const.ArrayStart);

            foreach (var item in events)
            {
                sb.Append(item.ToStructuredString(indentLevel + 1));
            }

            sb.Append(Const.ArrayEnd);
            return sb.ToString();
        }


        public static string ToStructuredString(this TSBWizardParam[] paramss, int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Const.ParamListHeader);

            foreach (var item in paramss)
            {
                sb.Append(item.ToStructuredString(indentLevel + 1));
            }

            sb.Append(Const.End);
            return sb.ToString();
        }

        public static string ToStructuredString(this TSBWizardAction[] actions, int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Const.ActionListHeader);

            foreach (var item in actions)
            {
                sb.Append(item.ToStructuredString(indentLevel + 1));
            }

            sb.Append(Const.End);
            return sb.ToString();
        }

        public static string ToStructuredString(this TSBWizardStep[] steps, int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Const.StepListHeader);

            foreach (var item in steps)
            {
                sb.Append(item.ToStructuredString(indentLevel + 1));
            }

            sb.Append(Const.End);
            return sb.ToString();
        }
    }
}
