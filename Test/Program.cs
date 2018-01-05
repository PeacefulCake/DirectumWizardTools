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
            //Console.WriteLine(StringUtils.AddIndents("123", 5));


            string wholeWizard = File.ReadAllText(@"D:\DevProjects\WorkFriendly\WizardTools\Test\TestWizardData\Wizard22.txt");

            Wizard w = new Wizard();

            w.LoadFromDataList(StringUtils.GetListFromText(wholeWizard));

            Console.Write(w.ExportToString());


            Console.ReadKey();
        }
    }
}
