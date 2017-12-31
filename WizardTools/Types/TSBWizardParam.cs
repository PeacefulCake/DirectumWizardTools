using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardTools.Types
{
    class TSBWizardParam : IStringable
    {
        WizardString ParamName;
        WizardString Title;
        bool Required;
        bool IsNull; // False, когда параметр заполне по умолчанию, или отсутствует вместе со значением

        virtual public void LoadFromStringList(List<String> data)
        {
            throw new NotImplementedException();
        }

        virtual public string ToStructuredString(int indentLevel)
        {
            throw new NotImplementedException();
        }
    }

    class TSBBooleanWizardParam : TSBWizardParam
    {
        bool Value;

        // при сохранении вызвать base, а затем если не IsNull, то и свое значение
    }

    class TSBStringWizardParam : TSBWizardParam
    {
        WizardString Value;
    }

    class TSBTextWizardParam : TSBWizardParam
    {
        WizardString Value;
    }

    class TSBIntegerWizardParam : TSBWizardParam
    {
        int Value;
    }

    class TSBNumericWizardParam : TSBWizardParam
    {
        double Value;
    }

    class TSBDateWizardParam : TSBWizardParam
    {
        DateTime Value;

        // Тут сделать обрезку времени
    }

    class TSBDateTimeWizardParam : TSBWizardParam
    {
        DateTime Value;
    }

    class TSBDateTimeWiTSBReferenceRecordInfoWizardParamzardParam : TSBWizardParam
    {
        int Value;
    }

    class TSBEDocumentInfoWizardParam : TSBWizardParam
    {
        int Value;
    }

    class TSBPickWizardParam : TSBWizardParam
    {
        WizardString Value;

        List<WizardString> ValueList;
        /*public const string PickParamStrings = "ValueList.Strings";
        public const string PickParamStringsStart = "(";
        public const string PickParamStringsEnd = ")";*/
    }

    // UserParam
}