﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardTools.Types
{
    class TSBWizardActionList : IStringable
    {
        TSBWizardAction[] Actions;

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