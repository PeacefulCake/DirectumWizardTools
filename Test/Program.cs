using FAA.Utils;
using FAA.WizardTools.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string wholeWizard = File.ReadAllText(@"D:\DevProjects\WorkFriendly\WizardTools\Test\TestWizardData\Wizard22.txt");

            Wizard w = new Wizard();

            w.LoadFromDataList(StringUtils.GetListFromText(wholeWizard));

            w.SaveToFolder(@"D:\DevProjects\WorkFriendly\WizardTools\Test\TestWizardData\Wizard22\");

            //Console.Write(w.ExportToString());


            Console.ReadKey();
        }
    }
}
