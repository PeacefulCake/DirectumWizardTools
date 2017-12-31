using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardTools.Types
{
    class TSBWizardStepFormElement : IStringable
    {
        WizardString Caption;
        TFormElementType ElementType;

        WizardString ParamName;
        bool Required;

        TFormElementProperty ElementProperty;        
        WizardString Hint;

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