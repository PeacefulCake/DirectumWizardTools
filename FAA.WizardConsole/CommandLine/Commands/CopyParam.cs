using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardConsole.CommandLine.Commands
{
    class CopyParam : BaseCommand
    {
        public CopyParam() : base ()
        {
            name = "paramcopy";
            commandFormat = "<original_param> <new_param>";
            help = "Скопировать параметр МД";
            helpDetails = "\t<original_param> - Имя параметра, который нужно скопировать\n\t<new_param> - Имя нового параметра";
        }

        public override void Execute(string[] commandLine)
        {
            if (!WizardInstanceManager.Loaded)
            {
                Console.WriteLine("МД не загружен");
                return;
            }

            string originalParamName = commandLine[1];
            string newParamName = commandLine[2];

            WizardInstanceManager.GetWizard.Params.CopyParam(originalParamName, newParamName);

            Console.WriteLine("Параметр успешно скопирован.");
        }
    }
}
