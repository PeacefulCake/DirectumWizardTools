﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardToolsOverpowered.Types
{
    interface IStringable
    {
        string ToStructuredString(int indentLevel);
        void LoadFromStringList(List<String> data);
    }
}