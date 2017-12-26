using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardTools.Types
{
    class TSBWizardAction : IStringable
    {
        public WizardString ActionName;
        public WizardString Title;

        public TSBWizardEvent[] Events;

        public TWizardActionType ActionType;

        public bool Enabled;

        public void LoadFromStringList(IList<string> data)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString(int indentLevel)
        {
            throw new NotImplementedException();
        }
    }
}