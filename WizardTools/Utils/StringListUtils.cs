using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardTools.Utils
{
    public static class StringListUtils
    {
        public static List<string> GetListFromText(string text)
        {
            List<string> resultList = new List<string>(text.Split(new string[] { Const.CR }, StringSplitOptions.RemoveEmptyEntries)/*.Select(s => s.TrimStart())*/);
            return resultList;
        }

        public static List<string> PickItem(List<string> sList, int startIndex, string endString)
        {
            int linesCount = 0;
            string current = sList[startIndex];
            endString = string.Format("{0}{1}", new string(' ', current.Length - current.TrimStart().Length), endString);

            do
            {
                linesCount++;
                current = sList[startIndex + linesCount - 1];
            } while (sList.Count > linesCount + startIndex && current != endString);

            List<string> result = sList.GetRange(startIndex, linesCount);
            sList.RemoveRange(startIndex, linesCount);
            return result;
        }

        public static List<string> PickObject(List<string> sList, int startIndex)
        {
            return PickItem(sList, startIndex, Const.End);
        }

        public static List<string> PickArray(List<string> sList, int startIndex)
        {
            var result = PickItem(sList, startIndex, Const.End + Const.ArrayEnd);
            // Грохнуть певую строчку, уйдет и крышки и ненужное имя массива
            result.RemoveAt(startIndex);
            // Из последней строки убрать последний символ - закрывающую крышку
            string lastStr = result[result.Count - 1];
            result[result.Count - 1] = lastStr.Substring(0, lastStr.Length - 1);
            return result;
        }

        public static List<string> PickArrayElement(List<string> sList, int startIndex)
        {
            // TODO : Ждем или массив уже без крышки, или смриться что будет выход за счет условия выхода за рамки индекса
            return PickItem(sList, startIndex, Const.End);
        }

        public static bool GetFieldPair(string line, out string name, out string value)
        {
            if(line.IndexOf(Const.Separator) > -1)
            {
                name = line.Split(Const.SeparatorArr, StringSplitOptions.None).First();
                value = line.Split(Const.SeparatorArr, StringSplitOptions.None).Last();
                return true;
            }
            else
            {
                name = null;
                value = null;
                return false;
            }
        }
    }
}
