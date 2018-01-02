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
            //sb.AppendLine(Const.ArrayStart);
            sb.Append(Const.ArrayStart);
            if (events.Length > 0)
            {
                foreach (var item in events)
                {
                    sb.AppendLine(item.ToStructuredString(indentLevel + 1));
                }
            }

            sb.Append(Const.ArrayEnd);
            return sb.ToString();
        }
    }
}
