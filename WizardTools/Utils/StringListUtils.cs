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
    }
}
