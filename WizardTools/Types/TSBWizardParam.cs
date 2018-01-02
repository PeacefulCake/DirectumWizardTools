using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Utils;

namespace WizardTools.Types
{
    abstract class TSBWizardParam : IStringable
    {
        protected WizardString ParamName;
        protected WizardString Title;
        protected bool Required;
        protected bool IsNull; // False, когда параметр заполне по умолчанию, или отсутствует вместе со значением

        public abstract void AppendTypeBasedLines(StringBuilder sb, int indentLevel);
        protected string ObjectHeader;

        public TSBWizardParam()
        {
            ParamName = new WizardString();
            Title = new WizardString();
            Required = false;
            IsNull = true;
        }

        virtual public void LoadFromStringList(List<String> data)
        {
            int dataIndex = 1;
            while (dataIndex < data.Count - 1)
            {
                string line = data[dataIndex];
                string trimmedLine = line.TrimStart();

                if (StringListUtils.GetFieldPair(trimmedLine, out string name, out string value))
                {
                    switch (name)
                    {
                        case Const.FieldParamName:
                            if (value == "") this.ParamName.LoadFromStringList(StringListUtils.PickWizardString(data, dataIndex + 1));
                            this.ParamName.EncodedValue = value;
                            break;
                        case Const.FieldTitle:
                            if (value == "") this.Title.LoadFromStringList(StringListUtils.PickWizardString(data, dataIndex + 1));
                            this.Title.EncodedValue = value;
                            break;
                        case Const.FieldRequired:
                            this.Required = true;
                            break;
                        case Const.FieldIsNull:
                            this.IsNull = false;
                            break;
                        default:
                            break;
                    }
                }
                dataIndex++;
            }
        }

        virtual public string ToStructuredString(int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);
            string innerIndent = new string(' ', (indentLevel+1) * 2);

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}{2}", currentIndent, ObjectHeader, Const.CR);
            sb.AppendFormat("{0}ParamName = {1}{2}", innerIndent, ParamName.ToStructuredString(indentLevel + 1), Const.CR);
            sb.AppendFormat("{0}Title = {1}{2}", innerIndent, Title.ToStructuredString(indentLevel + 1), Const.CR);
            if (Required)
            {
                sb.AppendFormat("{0}Required = True{1}", innerIndent, Const.CR);
            }
            if (!IsNull)
            {
                sb.AppendFormat("{0}IsNull = False{1}", innerIndent, Const.CR);
            }
            AppendTypeBasedLines(sb, indentLevel + 1);
            
            sb.AppendFormat("{0}{1}", currentIndent, Const.End);
            return sb.ToString();
        }
    }
    
    class TSBBooleanWizardParam : TSBWizardParam
    {
        bool Value;

        public TSBBooleanWizardParam() : base()
        {
            ObjectHeader = Const.BooleanParamHeader;
            Value = false;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            if (!IsNull)
            {
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToString(), Const.CR);
            }
        }
    }

    class TSBStringWizardParam : TSBWizardParam
    {
        WizardString Value;

        public TSBStringWizardParam() : base()
        {
            ObjectHeader = Const.StringParamHeader;
            Value = null;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            if (!IsNull)
            {
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToStructuredString(indentLevel), Const.CR);
            }
        }

        public override void LoadFromStringList(List<string> data)
        {
            base.LoadFromStringList(data);

            int dataIndex = 1;
            while (dataIndex < data.Count - 1)
            {
                string line = data[dataIndex];
                string trimmedLine = line.TrimStart();

                if (StringListUtils.GetFieldPair(trimmedLine, out string name, out string value))
                {
                    switch (name)
                    {
                        case Const.FieldValue:
                            this.Value = new WizardString();
                            if (value == "") this.Value.LoadFromStringList(StringListUtils.PickWizardString(data, dataIndex + 1));
                            else this.Value.EncodedValue = value;
                            break;
                        default:
                            break;
                    }
                }
                dataIndex++;
            }
        }
    }

    class TSBTextWizardParam : TSBWizardParam
    {
        WizardString Value;

        public TSBTextWizardParam() : base()
        {
            ObjectHeader = Const.TextParamHeader;
            Value = null;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            if (!IsNull)
            {
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToStructuredString(indentLevel), Const.CR);
            }
        }

        public override void LoadFromStringList(List<string> data)
        {
            base.LoadFromStringList(data);

            int dataIndex = 1;
            while (dataIndex < data.Count - 1)
            {
                string line = data[dataIndex];
                string trimmedLine = line.TrimStart();

                if (StringListUtils.GetFieldPair(trimmedLine, out string name, out string value))
                {
                    switch (name)
                    {
                        case Const.FieldValue:
                            this.Value = new WizardString();
                            if (value == "") this.Value.LoadFromStringList(StringListUtils.PickWizardString(data, dataIndex + 1));
                            else this.Value.EncodedValue = value;
                            break;
                        default:
                            break;
                    }
                }
                dataIndex++;
            }
        }
    }

    class TSBIntegerWizardParam : TSBWizardParam
    {
        int Value;

        public TSBIntegerWizardParam() : base()
        {
            ObjectHeader = Const.IntegerParamHeader;
            Value = 0;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            if (!IsNull)
            {
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToString(), Const.CR);
            }
        }
    }

    class TSBNumericWizardParam : TSBWizardParam
    {
        double Value;
        string originalValue; // value = Convert.ToDouble(vanillaValue.Replace('.', ','));

        public TSBNumericWizardParam() : base()
        {
            ObjectHeader = Const.NumericParamHeader;
            Value = 0;
            originalValue = null;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            if (!IsNull)
            {
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToString("F18").Replace(',', '.'), Const.CR);
            }
        }
    }

    class TSBDateWizardParam : TSBWizardParam
    {
        DateTime Value; // DateTime dt = DateTime.FromOADate(value);
        double innerValue;
        string originalValue; 

        public TSBDateWizardParam() : base()
        {
            ObjectHeader = Const.DateParamHeader;
            Value = new DateTime();
            innerValue = Value.ToOADate();
            originalValue = null;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            if (!IsNull)
            {
                innerValue = Value.Date.ToOADate();
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, innerValue.ToString("F18").Replace(',', '.'), Const.CR);
            }
        }
        
    }

    class TSBDateTimeWizardParam : TSBWizardParam
    {
        DateTime Value;
        double innerValue;
        string originalValue;

        public TSBDateTimeWizardParam() : base()
        {
            ObjectHeader = Const.DateTimeParamHeader;
            Value = new DateTime();
            innerValue = Value.ToOADate();
            originalValue = null;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);
            
            if (!IsNull)
            {
                innerValue = Value.ToOADate();
                sb.AppendFormat("{0}Value = {1}{2}", currentIndent, innerValue.ToString("F18").Replace(',', '.'), Const.CR);
            }
        }
    }

    class TSBReferenceRecordInfoWizardParam : TSBWizardParam
    {
        int Value;
        WizardString ComponentCode;
        bool ShowOperatingRecordsOnly;

        public TSBReferenceRecordInfoWizardParam() : base()
        {
            ObjectHeader = Const.ReferenceParamHeader;
            Value = 0;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);

            sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToString(), Const.CR);
            sb.AppendFormat("{0}ComponentCode = {1}{2}", currentIndent, ComponentCode.ToStructuredString(indentLevel), Const.CR);
            if (ShowOperatingRecordsOnly)
            {
                sb.AppendFormat("{0}ShowOperatingRecordsOnly = True{1}", currentIndent, Const.CR);
            }
        }
    }

    class TSBEDocumentInfoWizardParam : TSBWizardParam
    {
        int Value;

        public TSBEDocumentInfoWizardParam() : base()
        {
            ObjectHeader = Const.DocumentParamHeader;
            Value = 0;
        }

        public override void AppendTypeBasedLines(StringBuilder sb, int indentLevel)
        {
            string currentIndent = new string(' ', indentLevel * 2);
            sb.AppendFormat("{0}Value = {1}{2}", currentIndent, Value.ToString(), Const.CR);
        }
    }

    /*class TSBPickWizardParam : TSBWizardParam
    {
        WizardString Value;

        List<WizardString> ValueList;
        public const string PickParamStrings = "ValueList.Strings";
        public const string PickParamStringsStart = "(";
        public const string PickParamStringsEnd = ")";
    }*/

    // UserParam
}