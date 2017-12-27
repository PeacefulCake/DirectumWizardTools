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
            List<string> resultList = new List<string>(text.Split(new string[] { Const.CR }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.TrimStart()));
            return resultList;
        }

        public static List<string> PickObject(List<string> sList)
        {
            throw new NotImplementedException();
        }

        public static List<string> PickArray(List<string> sList)
        {
            throw new NotImplementedException();
        }

        public static List<string> PickArrayElement(List<string> sList)
        {
            throw new NotImplementedException();
        }

        public static void GetFieldPair(string line, out string name, out string value)
        {
            name = line.Split(Const.SeparatorArr, StringSplitOptions.None).First();
            value = line.Split(Const.SeparatorArr, StringSplitOptions.None).Last();
        }
    }
}
