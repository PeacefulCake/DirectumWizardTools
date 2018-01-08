using FAA.WizardConsole.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string userLine;
            do //действия при кожде exit - спросить хочу ли я, спрашивать ридкеем, пока не появится символ известный
            {
                Console.Write(@"WT>");
                userLine = Console.ReadLine();
                CommandsProcessor.ExecuteLine(userLine);

            } while (!CommandsProcessor.exitFlag);
            Console.WriteLine("Выход из приложения. Всего доброго!");

            //Console.ReadKey();
        }
    }
}
