using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardToolsOverpowered.Types
{
    class TSBWizardAction : IStringable
    {
        public WizardString ActionName;
        public WizardString Title;

        public TSBWizardEvent[] Events;

        public TWizardActionType ActionType;

        public bool Enabled;

        public void LoadFromStringList(List<String> data)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString(int indentLevel)
        {
            throw new NotImplementedException();
        }
    }
}