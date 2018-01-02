using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAA.WizardEncoding;

namespace WizardTools.Utils
{
    public class Const
    {
        static Const()
        {
            SeparatorArr = new string[] { Const.Separator };
        }

        public const string CR = "\r\n";

        // Текстовая разметка
        public const string Item = "item";
        public const string End = "end";
        public const string ArrayStart = "<";
        public const string ArrayEnd = ">";
        public const string EmptyArray = "<>";
        public const string Separator = " = ";
        public static string[] SeparatorArr;

        // "Простые" поля
        public const string FieldID = "ID";
        public const string FieldCode = "Code";
        public const string FieldTitle = "Title";
        public const string FieldEvents = "Events";

        public const string FieldParamName = "ParamName";
        public const string FieldRequired = "Required";
        public const string FieldIsNull = "IsNull";
        public const string FieldValue = "Value";

        public const string WizardHeader = "object TSBWizard";

        public const string NameValueDelimiter = " = ";
        public const string StepListHeader = "object TSBWizardStepList";
        public const string StepHeader = "object TSBFormWizardStep";
        public const string FinalStepHeader = "object TSBFinalWizardStep";
        public const string ActionListHeader = "object TSBWizardActionList";
        public const string ActionHeader = "object TSBWizardAction";
        public const string StepFormElementListHeader = "object TSBWizardStepFormElementList";
        public const string StepFormElementHeader = "object TSBWizardStepFormElement";
        // Параметры
        public const string ParamListHeader = "object TSBWizardParamList";
        public const string ReferenceParamHeader = "object TSBReferenceRecordInfoWizardParam";
        public const string PickParamHeader = "object TSBPickWizardParam";
            public const string PickParamStrings = "ValueList.Strings";
            public const string PickParamStringsStart = "(";
            public const string PickParamStringsEnd = ")";
        public const string StringParamHeader = "object TSBStringWizardParam";
        public const string DocumentParamHeader = "object TSBEDocumentInfoWizardParam";
        public const string TextParamHeader = "object TSBTextWizardParam";
        public const string BooleanParamHeader = "object TSBBooleanWizardParam";
        public const string IntegerParamHeader = "object TSBIntegerWizardParam";
        public const string DateParamHeader = "object TSBDateWizardParam";
        public const string DateTimeParamHeader = "object TSBDateTimeWizardParam";
        public const string NumericParamHeader = "object TSBNumericWizardParam";
        public const string UserParamHeader = "object TSBUserWizardParam";
        public const string TaskParamHeader = "object TSBTaskInfoWizardParam";
        public const string ReferenceListParamHeader = "object TSBReferenceRecordInfoListWizardParam";
        public const string UserListParamHeader = "object TSBUserListWizardParam";
        public const string DocumentListParamHeader = "object TSBEDocumentInfoListWizardParam";
        public const string FolderParamHeader = "object TSBFolderInfoWizardParam";
        public const string FileParamHeader = "object TSBFileNameWizardParam";
        public const string ContentsParamHeader = "object TSBContentsWizardParam";


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