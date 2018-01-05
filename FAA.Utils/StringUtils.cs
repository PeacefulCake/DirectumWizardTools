using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.Utils
{
    public static class StringUtils
    {
        public static int IndentsCount(this string str)
        {
            return str.Length - str.TrimStart().Length;
        }

        public static string AddIndents(this string str, int indentsCount)
        {
            return string.Format("{0}{1}", new string(' ', indentsCount), str);
        }

        public static List<string> GetListFromText(string text)
        {
            List<string> resultList = new List<string>(text.Split(new string[] { WSConstants.CR }, StringSplitOptions.RemoveEmptyEntries)/*.Select(s => s.TrimStart())*/);
            return resultList;
        }

        public static List<string> PickWizardString(List<string> sList, int startIndex)
        {
            int linesCount = 0;
            string current = sList[startIndex];

            int indentsCount = current.Length - current.TrimStart().Length;
            int currentIndentsCount;
            do
            {
                linesCount++;
                current = sList[startIndex + linesCount - 1];
                currentIndentsCount = current.Length - current.TrimStart().Length;

            } while (sList.Count > linesCount + startIndex && currentIndentsCount == indentsCount);
            if (currentIndentsCount != indentsCount) linesCount--; // Ибо такой спотык будет на ненужной строке
            List<string> result = sList.GetRange(startIndex, linesCount);
            sList.RemoveRange(startIndex, linesCount);
            return result;
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
            return PickItem(sList, startIndex, WSConstants.Markup.End);
        }

        public static List<string> PickArray(List<string> sList, int startIndex)
        {
            var result = PickItem(sList, startIndex, WSConstants.Markup.End + WSConstants.Markup.ArrayEnd);
            // Грохнуть певую строчку, уйдет и крышка и ненужное имя массива
            result.RemoveAt(startIndex);
            // Из последней строки убрать последний символ - закрывающую крышку
            string lastStr = result[result.Count - 1];
            result[result.Count - 1] = lastStr.Substring(0, lastStr.Length - 1);
            return result;
        }

        public static List<string> PickArrayElement(List<string> sList, int startIndex)
        {
            // TODO : Ждем или массив уже без крышки, или смриться что будет выход за счет условия выхода за рамки индекса
            return PickItem(sList, startIndex, WSConstants.Markup.End);
        }

        public static bool GetFieldPair(string line, out string name, out string value)
        {
            if(line.IndexOf(WSConstants.Markup.ValueSeparator) > -1)
            {
                name = line.Split(WSConstants.Markup.ValueSeparatorArr, StringSplitOptions.None).First();
                value = line.Split(WSConstants.Markup.ValueSeparatorArr, StringSplitOptions.None).Last();
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
