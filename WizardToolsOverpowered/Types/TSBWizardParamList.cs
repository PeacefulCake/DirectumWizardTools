using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardToolsOverpowered.Utils;

namespace WizardToolsOverpowered.Types
{
    class TSBWizardParamList : IStringable
    {
        List<TSBWizardParam> Params;

        public TSBWizardParamList()
        {
            Params = new List<TSBWizardParam>();
        }

        public void LoadFromStringList(List<String> data)
        {
            int dataIndex = 1; // Начать со второй строки, где имя текущего объекта, и закончить до end
            while (dataIndex < data.Count - 1)
            {
                string line = data[dataIndex];
                string trimmedLine = line.TrimStart();

                var paramLines = StringListUtils.PickObject(data, dataIndex);
                var param = GetParamObjectByName(trimmedLine);
                if (param != null) // TODO : Условия быть не должно
                {
                    param.LoadFromStringList(paramLines);

                    Params.Add(param);
                }
            }
        }

        public string ToStructuredString(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();
            string currentIndent = new string(' ', indentLevel * 2);
            sb.AppendFormat("{0}{1}{2}", currentIndent, Const.ParamListHeader, Const.CR);

            foreach (var item in Params)
            {
                sb.AppendLine(item.ToStructuredString(indentLevel + 1));
            }

            sb.AppendFormat("{0}{1}", currentIndent, Const.End);
            return sb.ToString();
        }

        TSBWizardParam GetParamObjectByName(string objectName)
        {
            switch (objectName)
            {
                case Const.ReferenceParamHeader:
                    return new TSBReferenceRecordInfoWizardParam();
                //case Const.PickParamHeader:
                //    return new TSBPickWizardParam();
                case Const.StringParamHeader:
                    return new TSBStringWizardParam();
                case Const.DocumentParamHeader:
                    return new TSBEDocumentInfoWizardParam();
                case Const.TextParamHeader:
                    return new TSBTextWizardParam();
                case Const.BooleanParamHeader:
                    return new TSBBooleanWizardParam();
                case Const.IntegerParamHeader:
                    return new TSBIntegerWizardParam();
                case Const.DateParamHeader:
                    return new TSBDateWizardParam();
                case Const.DateTimeParamHeader:
                    return new TSBDateTimeWizardParam();
                case Const.NumericParamHeader:
                    return new TSBNumericWizardParam();
                case Const.UserParamHeader:
                //    return new TSBUserWizardParam();
                //case Const.TaskParamHeader:
                //    return new TSBTaskInfoWizardParam();
                //case Const.ReferenceListParamHeader:
                //    return new TSBReferenceRecordInfoListWizardParam();
                //case Const.UserListParamHeader:
                //    return new TSBUserListWizardParam();
                //case Const.DocumentListParamHeader:
                //    return new TSBEDocumentInfoListWizardParam();
                //case Const.FolderParamHeader:
                //    return new TSBFolderInfoWizardParam();
                //case Const.FileParamHeader:
                //    return new TSBFileNameWizardParam();
                //case Const.ContentsParamHeader:
                //    return new TSBContentsWizardParam();
                default:
                    return null; // TODO!!
                    throw new FormatException("Неожиданный тип параметра: " + objectName);
            }
        }
    }
}