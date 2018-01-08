﻿using FAA.Utils;
using FAA.WizardTools.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole
{
    public static class WizardInstanceManager
    {
        private static Wizard wizard;
        public static Wizard GetWizard
        {
            get { return wizard;  }
        }

        public static bool Loaded {
            get {
                return wizard.Loaded;
            }
        }
        public static bool ChangesSaved { get; set; }
        static WizardInstanceManager()
        {
            wizard = new Wizard();
            ChangesSaved = true;
        }

        public static bool LoadFromString(string wizardData)
        {
            var data = StringUtils.GetListFromText(wizardData);
            if (data.First() != WSConstants.Objects.Wizard || data.Last() != WSConstants.Markup.End)
                return false;

            wizard.LoadFromDataList(data);
            WizardInstanceManager.ChangesSaved = true;
            return true;
        }
    }
}
