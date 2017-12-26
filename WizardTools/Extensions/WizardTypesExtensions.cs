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
            sb.Append(Const.ArrayStart);
            sb.Append(Const.CR);

            foreach (var item in events)
            {
                sb.Append(item.ToStructuredString(indentLevel + 1));
            }

            sb.Append(Const.ArrayEnd);
            return sb.ToString();
        }
    }
}
