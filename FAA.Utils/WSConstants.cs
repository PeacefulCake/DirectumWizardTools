using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.Utils
{
    public class WSConstants
    {
        public const string CR = "\r\n";

        // Текстовая разметка
        public class Markup
        {
            static Markup()
            {
                ValueSeparatorArr = new string[] { ValueSeparator };
            }

            public const string Item = "item";
            public const string End = "end";
            public const string ArrayStart = "<";
            public const string ArrayEnd = ">";
            public const string EmptyArray = "<>";
            public const string ValueSeparator = " = ";

            public const string ListStart = "(";
            public const string ListEnd = ")";
            public static string[] ValueSeparatorArr;
        }

        // "Простые" поля
        public class Fields
        {
            public const string ID = "ID";
            public const string Code = "Code";
            public const string Title = "Title";
            public const string Events = "Events";

            public const string ParamName = "ParamName";
            public const string Required = "Required";
            public const string IsNull = "IsNull";
            public const string Value = "Value";

            public const string PickValues = "ValueList.Strings";
        }

        public class Objects
        {
            public const string Wizard = "object TSBWizard";

            public const string StepList = "object TSBWizardStepList";
            public const string Step = "object TSBFormWizardStep";
            public const string FinalStep = "object TSBFinalWizardStep";
            public const string ActionList = "object TSBWizardActionList";
            public const string Action = "object TSBWizardAction";
            public const string StepFormElementList = "object TSBWizardStepFormElementList";
            public const string StepFormElement = "object TSBWizardStepFormElement";

            // Параметры
            public const string ParamList = "object TSBWizardParamList";
            public class Params
            {
                public const string Reference = "object TSBReferenceRecordInfoWizardParam";
                public const string Pick = "object TSBPickWizardParam";
                public const string String = "object TSBStringWizardParam";
                public const string Document = "object TSBEDocumentInfoWizardParam";
                public const string Text = "object TSBTextWizardParam";
                public const string Boolean = "object TSBBooleanWizardParam";
                public const string Integer = "object TSBIntegerWizardParam";
                public const string Date = "object TSBDateWizardParam";
                public const string DateTime = "object TSBDateTimeWizardParam";
                public const string Numeric = "object TSBNumericWizardParam";
                public const string User = "object TSBUserWizardParam";
                public const string Task = "object TSBTaskInfoWizardParam";
                public const string ReferenceList = "object TSBReferenceRecordInfoListWizardParam";
                public const string UserList = "object TSBUserListWizardParam";
                public const string DocumentList = "object TSBEDocumentInfoListWizardParam";
                public const string Folder = "object TSBFolderInfoWizardParam";
                public const string File = "object TSBFileNameWizardParam";
                public const string Contents = "object TSBContentsWizardParam";
            }
        }

        public class EventTypes
        {
            public const string WizardBeforeSelection = "wetWizardBeforeSelection";
            public const string WizardStart = "wetWizardStart";
            public const string WizardFinish = "wetWizardFinish";
            public const string StepStart = "wetStepStart";
            public const string StepFinish = "wetStepFinish";
            public const string ActionExecute = "wetActionExecute";
        }
    }
}