using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardToolsOverpowered.Types
{
    class TSBWizardStep : IStringable
    {
        WizardString StepName;
        WizardString Title;

        public TSBWizardEvent[] Events;

        WizardString Description;

        TSBWizardActionList ActionList;

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