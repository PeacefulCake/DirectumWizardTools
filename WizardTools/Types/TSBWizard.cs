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

            sb.Append(Const.End);
            return sb.ToString();
        }

        public void LoadFromStringList(List<string> data)
        {
            // TODO: нихера так низзя, нужно каждый раз равнивать Count, ибо есть уменьшение
            foreach (int dataIndex in Enumerable.Range(0, data.Count))
            {
                string line = data[dataIndex];
                string trimmedLine = line.TrimStart();
                string name, value;

                if (StringListUtils.GetFieldPair(trimmedLine, out name, out value))
                {
                    switch (name)
                    {
                        case Const.FieldID:
                            this.ID = Convert.ToInt32(value);
                            break;
                        case Const.FieldCode:
                            this.Code.EncodedValue = value;
                            break;
                        case Const.FieldTitle:
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
                }
                else
                {
                    switch (trimmedLine)
                    {
                        case Const.StepListHeader:
                            break;
                        case Const.ParamListHeader:
                            break;
                        default:
                            break;
                    }
                }
            }


            throw new NotImplementedException();
        }

        public void LoadFromString(string dataString)
        {
            this.LoadFromStringList(StringListUtils.GetListFromText(dataString));
        }
    }
}
