using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardTools.Extensions;
using WizardTools.Utils;

namespace WizardTools.Types
{
    class TSBWizard
    {
        public WizardString Code;
        public int ID;
        public WizardString Title;

        public TSBWizardEvent[] Events;

        public TSBWizardStepList StepList;

        public TSBWizardParamList ParamList;

        public TSBWizard()
        {
            Code = new WizardString();
            ID = 0;
            Title = new WizardString();
            Events = new TSBWizardEvent[0];
            StepList = new TSBWizardStepList();
            ParamList = new TSBWizardParamList();
        }

        public string ToStructuredString()
        {
            int currentIndenLevel = 1;
            string currentIndent = new string(' ', currentIndenLevel * 2);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Const.WizardHeader);
            sb.AppendFormat("{0}Code = {1}{2}", currentIndent, Code.ToStructuredString(currentIndenLevel), Const.CR);
            sb.AppendFormat("{0}ID = {1}{2}", currentIndent, ID.ToString(), Const.CR);
            sb.AppendFormat("{0}Title = {1}{2}", currentIndent, Title.ToStructuredString(currentIndenLevel), Const.CR);

            sb.AppendFormat("{0}Events = {1}{2}", currentIndent, Events.ToStructuredString(currentIndenLevel), Const.CR);

            //sb.AppendLine(StepList.ToStructuredString(currentIndenLevel));

            sb.AppendLine(ParamList.ToStructuredString(currentIndenLevel));

            sb.Append(Const.End);
            return sb.ToString();
        }

        public void LoadFromStringList(List<string> data)
        {
            int dataIndex = 1;
            while(dataIndex < data.Count - 1)
            {
                string line = data[dataIndex];
                string trimmedLine = line.TrimStart();

                if (StringListUtils.GetFieldPair(trimmedLine, out string name, out string value))
                {
                    switch (name)
                    {
                        case Const.FieldID:
                            this.ID = Convert.ToInt32(value);
                            break;
                        case Const.FieldCode:
                            if (value == "") this.Code.LoadFromStringList(StringListUtils.PickWizardString(data, dataIndex + 1));
                            this.Code.EncodedValue = value;
                            break;
                        case Const.FieldTitle:
                            if (value == "") this.Title.LoadFromStringList(StringListUtils.PickWizardString(data, dataIndex + 1));
                            this.Title.EncodedValue = value;
                            break;
                        case Const.FieldEvents:
                            if (value != Const.EmptyArray)
                            {
                                var events = StringListUtils.PickArray(data, dataIndex);
                            }
                            break;
                        default:
                            throw new FormatException("Неожиданное имя свойства: " + name);
                    }
                    dataIndex++;
                }
                else
                {
                    switch (trimmedLine)
                    {
                        case Const.StepListHeader:
                            var tmpSteps = StringListUtils.PickObject(data, dataIndex);
                            break;
                        case Const.ParamListHeader:
                            var tmpParams = StringListUtils.PickObject(data, dataIndex);
                            this.ParamList.LoadFromStringList(tmpParams);
                            break;
                        default:
                            throw new FormatException("Неожиданный объект: " + trimmedLine);
                            break;
                    }
                }
            }


            //throw new NotImplementedException();
        }

        public void LoadFromString(string dataString)
        {
            this.LoadFromStringList(StringListUtils.GetListFromText(dataString));
        }
    }
}
